using Assets.Scripts.Characters.ObservatoryBoss.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.Skills
{
    public class LaserSkill : BossSkill
    {
        public GameObject ballPrefab;

        [SerializeField] private bool canGrow;
        [SerializeField] private float growSpeed;
        [SerializeField] private float maxSize;

        [SerializeField] private float moveSpeed;

        [SerializeField] private float ballTimeCooldown;
        [SerializeField] private float jump;
        //[SerializeField] private Animator lineBallEffect;
        //[SerializeField] private Animator explodeBallEffect;    

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newBall = Instantiate(ballPrefab, hitBox.transform.position, Quaternion.identity);
            Debug.Log(newBall.transform.position);
            LaserController controller = newBall.GetComponent<LaserController>();

            controller.Setup(this, canGrow, growSpeed, maxSize, moveSpeed, ballTimeCooldown, jump);
        }
    }
}
