using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCHaracterCOllison : MonoBehaviour
{
    public BoxCollider2D character;
    public BoxCollider2D characterBlocker;
    void Start()
    {
        Physics2D.IgnoreCollision(character,characterBlocker,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
