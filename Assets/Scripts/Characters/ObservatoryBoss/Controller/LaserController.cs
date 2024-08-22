using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.ObservatoryBoss.Skills;
using Assets.Scripts.Characters.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.Controller
{
    public class LaserController : MonoBehaviour
    {
        private CircleCollider2D cd;
        private PlayerCharacter player;
        private LaserSkill laserSkill;
        // Boss di chuyen
        private bool canMove;
        private float moveSpeed;

        private bool canGrow;
        private float growSpeed;
        private float maxSize;

        private float ballTimer;
        private float ballTimeCooldown;

        private Vector2 moveDirection;

        private float jump;

        private BossManager bossManager;




        private void Start()
        {
            cd = GetComponent<CircleCollider2D>();
            player = PlayerManager.instance.player;     
        }

        private void Laser()
        {
            canMove = true;

            // Ban laser
            Debug.Log("Run Laser");

            canGrow = false;
            moveDirection = (transform.position).normalized;
            ballTimer = ballTimeCooldown;
        }

        private void Update()
        {
            if (canMove)
            {
                //transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
                //ballTimer -= Time.deltaTime;

                if (ballTimer < 0)
                    FinishLaserSkill();
            }
            else if (IsMaxSize())
            {
                Laser();
            }
            else if (canGrow)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            }
        }

        private void FinishLaserSkill()
        {
            canMove = false;
            Destroy(gameObject);
        }

        public void Setup(LaserSkill _fireBallSkill, bool _canGrow, float _growSpeed, float _maxSize, float _moveSpeed,
            float _ballTimeCooldown, float _jump)
        {
            laserSkill = _fireBallSkill;
            canGrow = _canGrow;
            growSpeed = _growSpeed;
            maxSize = _maxSize;
            moveSpeed = _moveSpeed;
            ballTimeCooldown = _ballTimeCooldown;
            jump = _jump;
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
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    //PlayerStats stat = player.GetComponent<PlayerStats>();
                    //stat.DoDamage();

                    player.DamageEffect();


                }
                FinishLaserSkill();
            }
        }
    }
}
