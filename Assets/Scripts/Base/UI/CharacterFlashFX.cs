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

        [Header("Ailment colors")]
        [SerializeField] private Color[] chillColor;
        [SerializeField] private Color[] igniteColor;
        [SerializeField] private Color[] shockColor;


        private void Start()
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            originalMaterial = sr.material;
        }

        private IEnumerator FlashFX()
        {
            sr.material = hitMaterial;

            Color currentColor = sr.color;

            sr.color = new Color(0.83f, 0.83f, 0.83f);

            yield return new WaitForSeconds(flashDuration);

            sr.color = currentColor;
           sr.material = originalMaterial;
        }

        private void RedColorBlink()
        {
            Color grayColor = new Color(0.83f, 0.83f, 0.83f);
            sr.color = sr.color == grayColor ? hitMaterial.color : grayColor;
        }

        private void CancelColorChange()
        {
            CancelInvoke();
            sr.color = new Color(0.83f, 0.83f, 0.83f);
        }
        public void ShockFxFor(float _seconds)
        {
            InvokeRepeating("ShockColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }

        public void ChillFxFor(float _seconds)
        {
            InvokeRepeating("ChillColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }

        public void IgniteFxFor(float _seconds)
        {
            InvokeRepeating("IgniteColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }

        private void IgniteColorFx()
        {
            if(sr.color != igniteColor[0])
            {
                sr.color = igniteColor[0];
            }
            else
            {
                sr.color = igniteColor[1];
            }
        }

        private void ShockColorFx()
        {
            if (sr.color != shockColor[0])
            {
                sr.color = shockColor[0];
            }
            else
            {
                sr.color = shockColor[1];
            }
        }

        private void ChillColorFx()
        {
            if (sr.color != chillColor[0])
            {
                sr.color = chillColor[0];
            }
            else
            {
                sr.color = chillColor[1];
            }
        }
    }
}
