using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class SwordSkill : Skill
    {
        public SwordType swordType = SwordType.Regular;

        [Header("Bounce info")]
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceSpeed;

        [Header("Piecer info")]
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity;

        [Header("Spin info")]
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float maxTravelDistance = 3;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1;

        [Header("Skill info")]
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        [SerializeField] private float freezeTimeDuration;
        [SerializeField] private float returnSpeed;

        private Vector2 finalDirection;

        [Header("Aim dots")]
        [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBeetwenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        private GameObject[] dots;

        protected override void Start()
        {
            base.Start();

            GenerateDots();
            SetupGravity();
        }

        private void SetupGravity()
        {
            switch (swordType)
            {
                case SwordType.Bounce:
                    swordGravity = bounceGravity;
                    break;

                case SwordType.Pierce:
                    swordGravity = pierceGravity;
                    break;

                case SwordType.Spin:
                    swordGravity = spinGravity;
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Vector2 direction = AimDirection();
                finalDirection = new Vector2(direction.normalized.x * launchForce.x,
                    direction.normalized.y * launchForce.y);
            }

            if (Input.GetKey(KeyCode.Tab))
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
                }
            }
        }

        public void CreateSword()
        {
            GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

            switch (swordType)
            {
                case SwordType.Bounce:
                    newSwordScript.SetupBounce(true, bounceAmount, bounceSpeed);
                    break;

                case SwordType.Pierce:
                    newSwordScript.SetupPiecer(pierceAmount);
                    break;

                case SwordType.Spin:
                    newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                    break;
            }

            newSwordScript.SetupSword(finalDirection, swordGravity, player, freezeTimeDuration, returnSpeed);

            player.AssignNewSword(newSword);

            DotsActive(false);
        }

        public Vector2 AimDirection()
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - playerPosition;

            return direction;
        }

        public void DotsActive(bool isActive)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].SetActive(isActive);
            }
        }

        private void GenerateDots()
        {
            dots = new GameObject[numberOfDots];
            for (int i = 0; i < numberOfDots; i++)
            {
                dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            var direction = AimDirection();
            Vector2 position = (Vector2)player.transform.position +
                new Vector2(
                    direction.normalized.x * launchForce.x,
                    direction.normalized.y * launchForce.y) * t + .5f
                    * (Physics2D.gravity * swordGravity) * (t * t);

            return position;
        }
    }

    public enum SwordType
    {
        Regular, Bounce, Pierce, Spin
    }
}
