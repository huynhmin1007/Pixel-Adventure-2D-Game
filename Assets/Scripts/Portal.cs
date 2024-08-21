using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
   private HashSet<GameObject> portalObject = new HashSet<GameObject>();

    [SerializeField] private Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (portalObject.Contains(collision.gameObject)) { return; }

        if(destination.TryGetComponent(out Portal destinationPortal))
        {
            destinationPortal.portalObject.Add(collision.gameObject);
        }

        collision.transform.position = destination.position;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        portalObject.Remove(collision.gameObject);
    }
}
