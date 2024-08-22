using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private Character character;
    private RectTransform myTransform;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        character = GetComponentInParent<Character>();

        character.onFlipped += FlipUI;
    }

    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }
}
