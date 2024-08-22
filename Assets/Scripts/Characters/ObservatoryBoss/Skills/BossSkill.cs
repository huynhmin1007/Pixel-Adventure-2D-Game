using Assets.Scripts.Characters.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.Skills
{
    public class BossSkill : Skill
    {
        public Collider2D hitBox => BossManager.instance.boss.Hitbox;
        public ObservatoryBossCharacter boss => BossManager.instance.boss;

        
    }
}
