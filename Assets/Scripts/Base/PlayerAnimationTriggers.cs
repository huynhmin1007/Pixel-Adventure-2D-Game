using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private PlayerCharacter player => GetComponentInParent<PlayerCharacter>();
        private Collider2D hitbox => player.Hitbox;

        private void AnimationTrigger() => player.AnimationTrigger();

        private void AttackTrigger()
        {
            if (hitbox != null)
            {
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(hitbox.bounds.center, hitbox.bounds.size, 0);
                foreach (var hitCollider in hitColliders)
                {
                    EnemyCharacter enemy = hitCollider.GetComponent<EnemyCharacter>();
                    if (enemy != null)
                    {
                        enemy.Damage();
                    }
                }
            }
        }

        private void AnimationEndCounterTrigger() => player.AnimationEndCounterTrigger();
    }
}
