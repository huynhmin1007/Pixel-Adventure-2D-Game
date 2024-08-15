using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        public PlayerCharacter player;

        private void Awake()
        {
            if (instance != null)
                Destroy(instance.gameObject);
            else instance = this;
        }
    }
}
