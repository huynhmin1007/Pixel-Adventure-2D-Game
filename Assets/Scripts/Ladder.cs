using Assets.Scripts.Characters.Player;
using Autodesk.Fbx;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public PlayerCharacter player;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        player = PlayerManager.instance.player;
    }
    void Update()
    {
        Debug.Log(isClimbing);
        if (isLadder && Mathf.Abs(player.XInput) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            player.Rb.gravityScale = 0f;
            player.Rb.velocity = new Vector2(rb.velocity.x, player.XInput * speed);
        }
        else
        {
            player.Rb.gravityScale = 4f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chain"))
        {
            isLadder = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chain"))
        {
            isLadder = false;
        }
    }
}
