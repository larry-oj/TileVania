using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        FindObjectOfType<GameSession>().IncrementScore(100);
        Destroy(gameObject);
    }
}
