using Assets.Scripts.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkderStrikeController : MonoBehaviour
{

    [SerializeField] private CharacterStats targetStats;
    [SerializeField] private float speed;
    private int damage;


    private Animator animator;
    private bool triggered;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Setup(int _damage, CharacterStats _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }
    private void Update()
    {
        if (!targetStats) return;

        if(triggered)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;

        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {
            animator.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            triggered = true;
            targetStats.TakeDamage(1);
            animator.SetTrigger("Hit");
            Destroy(gameObject, .4f);
        }
    }
    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Character>() != null)
    //    {
    //        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    //        EnemyStats enemyTarget = collision.GetComponent<EnemyStats>();
    //        playerStats.DoMagicalDamage(enemyTarget);
    //    }
    //}
}
