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
                Collider2D[] hitColliders = new Collider2D[10];
                int hitCount = Physics2D.OverlapCollider(hitbox, new ContactFilter2D(), hitColliders);

                for (int i = 0; i < hitCount; i++)
                {
                    Collider2D hit = hitColliders[i];
                    if (hit != null)
                    {
                        EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();
                        if (enemy != null)
                        {
                            EnemyStats _target = hit.GetComponent<EnemyStats>();
                            player.stats.DoDamage(_target);
                        }
                    }
                }
            }
        }

        private void AnimationEndCounterTrigger() => player.AnimationEndCounterTrigger();

        private void ThrowSword()
        {
            player.swordSkill.UseSkill();
        }
    }
}
