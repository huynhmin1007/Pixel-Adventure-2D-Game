using Assets.Scripts.Characters.ObservatoryBoss.Skills;
using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.Controller
{
    public class FireBallController : MonoBehaviour
    {   
        
        private CircleCollider2D cd;
        private PlayerCharacter player;
        private FireBallSkill fireBallSkill;
        

        private bool canMove;
        private float moveSpeed;

        private bool canGrow;
        private float growSpeed;
        private float maxSize;

        private float ballTimer;
        private float ballTimeCooldown;

        private Vector2 moveDirection;
        

        private void Start()
        {
            cd = GetComponent<CircleCollider2D>();
            player = PlayerManager.instance.player;

         
        }

        private void FireBall()
        {
            canMove = true;
            canGrow = false;
            moveDirection = (player.transform.position - transform.position).normalized;
            ballTimer = ballTimeCooldown;
        }

        private void Update()
        {
            if (canMove)
            {
                transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
                ballTimer -= Time.deltaTime;

                if (ballTimer < 0)
                    FinishFireBallSkill();
            }
            else if (IsMaxSize())
            {
                FireBall();
            }
            else if (canGrow)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            }
        }

        private void FinishFireBallSkill()
        {
            canMove = false;
            Destroy(gameObject);
        }

        public void Setup(FireBallSkill _fireBallSkill, bool _canGrow, float _growSpeed, float _maxSize, float _moveSpeed,
            float _ballTimeCooldown)
        {
            fireBallSkill = _fireBallSkill;
            canGrow = _canGrow;
            growSpeed = _growSpeed;
            maxSize = _maxSize;
            moveSpeed = _moveSpeed;
            ballTimeCooldown = _ballTimeCooldown;
        }

        private bool IsMaxSize()
        {
            return Mathf.Abs(maxSize - transform.localScale.x) <= 1f
                && Mathf.Abs(maxSize - transform.localScale.y) <= 1f;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (canMove && (collision.gameObject.layer == LayerMask.NameToLayer("Player")
               || collision.gameObject.layer == LayerMask.NameToLayer("Ground")))
            {
                if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    //PlayerStats stat = player.GetComponent<PlayerStats>();
                    //stat.DoDamage();
                    
                    player.DamageEffect();


                }
                FinishFireBallSkill();
            }
        }
    }
}
