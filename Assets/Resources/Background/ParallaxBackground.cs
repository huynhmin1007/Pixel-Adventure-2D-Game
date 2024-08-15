using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private bool applyHorizontal;
    [SerializeField] private float horizontalEffect;

    [SerializeField] private bool applyVertical;
    [SerializeField] private float verticalEffect;

    private float length, height;
    private Vector2 startPosition;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        startPosition = transform.position;
    }

    void Update()
    {
        float distanceToMoveX = 0f;
        float distanceToMoveY = 0f;

        if (applyHorizontal)
        {
            float distanceMovedX = cam.transform.position.x * (1 - horizontalEffect);
            distanceToMoveX = cam.transform.position.x * horizontalEffect;

            if (distanceMovedX > startPosition.x + length)
                startPosition.x += length;
            else if (distanceMovedX < startPosition.x - length)
                startPosition.x -= length;
        }

        if (applyVertical)
        {
            float distanceMovedY = cam.transform.position.y * (1 - verticalEffect);
            distanceToMoveY = cam.transform.position.y * verticalEffect;

            if (distanceMovedY > startPosition.y + height)
                startPosition.y += height;
            else if (distanceMovedY < startPosition.y - height)
                startPosition.y -= height;
        }

        transform.position = new Vector3(startPosition.x + distanceToMoveX, startPosition.y + distanceToMoveY);
    }
}
