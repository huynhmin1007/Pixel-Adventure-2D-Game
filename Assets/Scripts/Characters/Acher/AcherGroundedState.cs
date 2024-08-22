using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Acher
{
    public class AcherGroundedState : EnemyGroundedState
    {
        private AcherCharacter acher;
        private Transform player;

        public AcherGroundedState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            acher = (AcherCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            player = PlayerManager.instance.player.transform;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();


        }
    }
}
