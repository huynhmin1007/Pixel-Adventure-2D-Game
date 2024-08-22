using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFall = false;
    public Transform restore;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") &&!isFall)
        {
            rb.isKinematic = false;
            isFall = true;
            Invoke("Restore", 5f);
        }
    }

    private void Restore()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularDrag = 0;
        transform.position = restore.position;
        isFall = false;

    }
}
