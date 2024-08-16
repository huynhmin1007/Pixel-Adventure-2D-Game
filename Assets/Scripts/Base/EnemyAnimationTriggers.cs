using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class EnemyAnimationTriggers : MonoBehaviour
    {
        private EnemyCharacter enemy => GetComponentInParent<EnemyCharacter>();
        private Collider2D hitbox => enemy.Hitbox;
        public LayerMask playerLayer;

        private void AnimationTrigger() => enemy.AnimationTrigger();

        private void AttackTrigger()
        {
            if (hitbox != null)
            {
                Collider2D hit = Physics2D.OverlapBox(hitbox.bounds.center, hitbox.bounds.size, 0, playerLayer);
                if (hit != null && hit.CompareTag("Player"))
                {
                    PlayerCharacter player = PlayerManager.instance.player;
                    if (player != null)
                    {
                        player.Damage();
                    }
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
        private void OpenWarningWindow() => enemy.OpenWarningAttackWindow();
    }
}
