using Assets.Scripts.Characters.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Character character;
    private RectTransform myTransform;
    private CharacterStats myStats;

    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        character = GetComponentInParent<Character>();

        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();
        character.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }

    private void FlipUI() => myTransform.Rotate(0, 180, 0);


    private void OnDisable()
    {
        character.onFlipped -= FlipUI;
        myStats.onHealthChanged -= UpdateHealthUI;
    }

}
