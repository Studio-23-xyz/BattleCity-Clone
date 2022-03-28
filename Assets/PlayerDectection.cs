using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class PlayerDectection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided enter with player");
            GetComponent<EnemyTank>().HitPlayer = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided enter with player");
            GetComponent<EnemyTank>().HitPlayer = false;
        }
    }
}
