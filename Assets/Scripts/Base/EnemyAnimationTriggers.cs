using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class EnemyAnimationTriggers : MonoBehaviour
    {
        private EnemyCharacter enemy => GetComponentInParent<EnemyCharacter>();

        private void AnimationTrigger() => enemy.AnimationTrigger();

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.AttackCheck.position, enemy.AttackCheckRadius);

            foreach (Collider2D hit in colliders)
            {
                PlayerCharacter player = hit.GetComponent<PlayerCharacter>();

                if (player != null)
                {
                    player.Damage();
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
        private void OpenWarningWindow() => enemy.OpenWarningAttackWindow();
    }
}
