using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private PlayerCharacter player => GetComponentInParent<PlayerCharacter>();
        private Collider2D hitbox => GetComponentInChildren<Collider2D>();

        private void AnimationTrigger() => player.AnimationTrigger();

        private void AttackTrigger()
        {

        }

        private void AnimationEndCounterTrigger() => player.AnimationEndCounterTrigger();
    }
}
