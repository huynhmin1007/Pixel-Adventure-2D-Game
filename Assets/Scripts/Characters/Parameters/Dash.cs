using UnityEngine;

namespace Assets.Scripts.Characters.Parameters
{
    [System.Serializable]
    public class Dash
    {
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;

        public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
        public float DashDuration { get => dashDuration; set => dashDuration = value; }
    }
}
