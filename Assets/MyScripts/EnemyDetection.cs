using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Vector3 previousPostion;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Enemy"))
        {
            previousPostion = transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("Collided enter enemy");
            GameUtils.Game.Instance.MovePlayer = false;
            transform.position = previousPostion;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided exit enemy");
            GameUtils.Game.Instance.MovePlayer = true;
            


        }
    }
}
