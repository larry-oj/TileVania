using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField] private BoxCollider2D feetCollider;
    [SerializeField] private float movementSpeed = 1f;

    private void Start()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        movementSpeed = -movementSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
