using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerCounterAttackState : BaseState
    {
        private readonly PlayerCharacter character;
        private bool isCounterSuccessful;
        private bool endCounter;
        private readonly List<EnemyCharacter> stunnedEnemies;

        public PlayerCounterAttackState(Character characterBase, StateMachine stateMachine, Enum animBoolName)
            : base(characterBase, stateMachine, animBoolName)
        {
            character = (PlayerCharacter)characterBase;
            stunnedEnemies = new List<EnemyCharacter>();
        }

        public override void Enter()
        {
            base.Enter();

            character.ResetVelocity();
            endCounter = false;
            isCounterSuccessful = false;
            stunnedEnemies.Clear();
            character.animator.SetBool(EState.CounterAttackSuccessful.ToString(), isCounterSuccessful);
        }

        public override void Update()
        {
            base.Update();

            if (!endCounter)
            {
                PerformCounterAttack();
            }

            if (endCounter)
            {
                HandleCounterEnd();
            }
        }

        private void PerformCounterAttack()
        {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(character.Hitbox.bounds.center,
                character.Hitbox.bounds.size, 0);

            foreach (var hitCollider in hitColliders)
            {
                EnemyCharacter enemy = hitCollider.GetComponentInParent<EnemyCharacter>();

                if (enemy != null && enemy.CanBeStunned && !stunnedEnemies.Contains(enemy))
                {
                    isCounterSuccessful = true;
                    stunnedEnemies.Add(enemy);
                    character.IsImmune = true;
                }
            }
        }

        private void HandleCounterEnd()
        {
            if (!isCounterSuccessful)
            {
                stateMachine.ChangeState(character.GetState(EState.Idle));
                return;
            }

            character.animator.SetBool(EState.CounterAttackSuccessful.ToString(), isCounterSuccessful);

            if (triggerCalled)
            {
                foreach (var enemy in stunnedEnemies)
                {
                    enemy.Stun();
                }

                if (stunnedEnemies.Count > 0)
                {
                    character.cloneSkill.CreateCloneOnCounterAttack(stunnedEnemies[0].transform);
                }

                character.IsImmune = false;
                stateMachine.ChangeState(character.GetState(EState.Idle));
            }
        }

        public void AnimationEndCounterTrigger()
        {
            endCounter = true;
        }
    }
}
