using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Base.UI
{
    public class CharacterFlashFX : MonoBehaviour
    {
        private SpriteRenderer sr;

        [Header("Flash FX")]
        [SerializeField] private float flashDuration;
        [SerializeField] private Material hitMaterial;
        private Material originalMaterial;

        private void Start()
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            originalMaterial = sr.material;
        }

        private IEnumerator FlashFX()
        {
            sr.material = hitMaterial;

            yield return new WaitForSeconds(flashDuration);

            sr.material = originalMaterial;
        }

        private void RedColorBlink()
        {
            Color grayColor = new Color(0.83f, 0.83f, 0.83f);
            sr.color = sr.color == grayColor ? hitMaterial.color : grayColor;
        }

        private void CancelRedBlink()
        {
            CancelInvoke();
            sr.color = new Color(0.83f, 0.83f, 0.83f);
        }
    }
}
