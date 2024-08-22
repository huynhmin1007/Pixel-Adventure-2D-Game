using Assets.Scripts.Characters.Enemy;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private EnemyCharacter character;
    private RectTransform myTransform;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        character = GetComponentInParent<EnemyCharacter>();

        character.onFlipped += FlipUI;
    }

    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }
}
