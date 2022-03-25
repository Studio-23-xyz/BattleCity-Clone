using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{

    public LayerMask layer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison new");
        Debug.Log(collision.gameObject.layer + "layername");

        if (collision.gameObject.layer == layer)
        {
            this.gameObject.transform.parent.GetComponent<PlayerTank>().collison = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layer)
        {
            this.gameObject.transform.parent.GetComponent<PlayerTank>().collison = false;
        }
    }
}
