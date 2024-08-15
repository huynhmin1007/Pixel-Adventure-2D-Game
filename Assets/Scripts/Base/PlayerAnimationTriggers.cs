using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private PlayerCharacter player => GetComponentInParent<PlayerCharacter>();

        private void AnimationTrigger() => player.AnimationTrigger();

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.AttackCheckRadius);

            foreach (Collider2D hit in colliders)
            {
                EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

                if (enemy != null)
                {
                    enemy.Damage();
                }
            }
        }

        private void AnimationEndCounterTrigger() => player.AnimationEndCounterTrigger();
    }
}
