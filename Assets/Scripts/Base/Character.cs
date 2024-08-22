using Assets.Scripts.Base;
using Assets.Scripts.Base.UI;
using Assets.Scripts.Characters.CollisionStrategy;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region Components
    protected Rigidbody2D rb;
    public Animator animator { get; private set; }
    public CharacterFlashFX flashFX { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    #region Movement
    [Header("Movement")]
    [SerializeField] protected float moveSpeed;
    protected float defaultMoveSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected Direction direction = Direction.RIGHT;

    protected float xInput, yInput;
    #endregion

    #region Attack
    [Header("Attack")]
    [SerializeField] protected int comboWindow;
    [SerializeField] protected Vector2[] attackMove;
    [SerializeField] protected Collider2D hitboxAttack;
    [SerializeField] protected float freezeTime;
    #endregion

    [Header("Knockback")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockBackDuration;
    protected bool isKnocked;

    #region States
    protected StateMachine stateMachine;
    protected Dictionary<Enum, BaseState> states;
    protected bool isBusy;

    protected Color defaultColor;

    #endregion

    #region Collision
    [Header("Collision Check")]
    [SerializeField] protected LineCollisionCheck groundCheck;
    [SerializeField] protected LineCollisionCheck wallCheck;
    [SerializeField] protected LayerMask groundLayer;
    #endregion

    protected bool isImmune;
    public CharacterStats stats { get; private set; }

    public System.Action onFlipped;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        flashFX = GetComponent<CharacterFlashFX>();

        defaultMoveSpeed = moveSpeed;
        stateMachine = new StateMachine();
        states = new Dictionary<Enum, BaseState>();

        InitializeStates();
    }

    protected virtual void Start()
    {
        defaultColor = sr.color;
        stateMachine.Initialize(states[EState.Idle]);
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        StateController();
        stateMachine.Update();
    }

    public IEnumerator BusyFor(float seconds)
    {
        IsBusy = true;
        yield return new WaitForSeconds(seconds);

        IsBusy = false;
    }

    protected abstract void InitializeStates();

    public virtual void AnimationTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public BaseState GetState(Enum name)
    {
        return states[name];
    }

    public virtual void Flip()
    {
        Direction = Direction.Flip();
        wallCheck.Direction = Direction;
        transform.Rotate(0, 180, 0);
        onFlipped();
    }

    public void SetVelocity(float x, float y, bool flip = false)
    {
        if (isKnocked) return;

        if (flip && ((Direction == Direction.LEFT && x > 0) || (Direction == Direction.RIGHT && x < 0)))
            Flip();

        Rb.velocity = new Vector2(x, y);
    }

    public void ResetVelocity()
    {
        if (isKnocked) return;

        XInput = 0;
        YInput = 0;
        Rb.velocity = new Vector2(0, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        wallCheck.Draw();
        groundCheck.Draw();
    }

    public virtual void DamageEffect()
    {
        flashFX.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        Rb.velocity = new Vector2(knockbackDirection.x * -Direction.XValue(), knockbackDirection.y);

        yield return new WaitForSeconds(knockBackDuration);

        isKnocked = false;
    }

    public void Transparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else sr.color = defaultColor;
    }

    public virtual void Dead()
    {

    }

    protected abstract void StateController();

    public Direction Direction { get => direction; set => direction = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    private bool IsNearlyZero(float value, float threshold = 0.001f)
    {
        return Mathf.Abs(value) < threshold;
    }

    public virtual void FrozenTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            animator.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeTimeFor(float seconds)
    {
        FrozenTime(true);

        yield return new WaitForSeconds(seconds);

        FrozenTime(false);
    }

    public float XVelocity => IsNearlyZero(Rb.velocity.x) ? 0 : Rb.velocity.x;
    public float YVelocity => IsNearlyZero(Rb.velocity.y) ? 0 : Rb.velocity.y;

    public float XInput { get => xInput; set => xInput = value; }
    public float YInput { get => yInput; set => yInput = value; }
    public int ComboWindow { get => comboWindow; set => comboWindow = value; }
    public bool IsBusy { get => isBusy; set => isBusy = value; }
    public Vector2[] AttackMove { get => attackMove; set => attackMove = value; }
    public bool IsImmune { get => isImmune; set => isImmune = value; }

    public RaycastHit2D IsGrounded() => groundCheck.Check(groundLayer);
    public RaycastHit2D IsWallDetected() => wallCheck.Check(groundLayer);
    public Collider2D Hitbox { get => hitboxAttack; set => hitboxAttack = value; }
    public float FreezeTime { get => freezeTime; set => freezeTime = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
}