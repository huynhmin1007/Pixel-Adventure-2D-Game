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
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(playerLayer);
                filter.useLayerMask = true;

                Collider2D[] hitColliders = new Collider2D[10];
                int hitCount = Physics2D.OverlapCollider(hitbox, filter, hitColliders);

                for (int i = 0; i < hitCount; i++)
                {
                    Collider2D hit = hitColliders[i];
                    if (hit != null && hit.CompareTag("Player"))
                    {
                        PlayerCharacter player = PlayerManager.instance.player;
                        if (player != null)
                        {
                            PlayerStats target = hit.GetComponent<PlayerStats>();
                            enemy.stats.DoDamage(target);
                        }
                    }
                }
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
        private void OpenWarningWindow() => enemy.OpenWarningAttackWindow();
    }
}
