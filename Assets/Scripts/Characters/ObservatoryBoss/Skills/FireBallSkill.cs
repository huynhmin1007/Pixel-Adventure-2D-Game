using Assets.Scripts.Characters.ObservatoryBoss.Controller;
using Assets.Scripts.Characters.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.Skills
{
    public class FireBallSkill : BossSkill
    {
        public GameObject ballPrefab;

        [SerializeField] private bool canGrow;
        [SerializeField] private float growSpeed;
        [SerializeField] private float maxSize;

        [SerializeField] private float moveSpeed;

        [SerializeField]  private float ballTimeCooldown;

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newBall = Instantiate(ballPrefab, hitBox.transform.position, Quaternion.identity);
            FireBallController controller = newBall.GetComponent<FireBallController>();

            controller.Setup(this, canGrow, growSpeed, maxSize, moveSpeed, ballTimeCooldown);
        }
    }
}
