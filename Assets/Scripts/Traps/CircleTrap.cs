using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrap : MonoBehaviour
{
    private float speedRotation = 30f;
    private float speedMoving = 10f;

    public Transform start;
    public Transform end;
    private Vector3 target;

    private void Start()
    {
        target = start.position;    
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedMoving * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if(transform.position == start.position)
            {
                target = end.position;
            }
            else
            {
                target = start.position;
            }
        }
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0,0,speedRotation);
    }
}
