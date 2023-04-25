using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private PlayerMovement _player;
    private float _speed;
    
    [SerializeField] private float projectileSpeed = 10f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _speed = _player.transform.localScale.x * projectileSpeed;
        transform.localScale = new Vector2(Mathf.Sign(_speed), 1f);
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(_speed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_collider.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazard")))
            Destroy(this.gameObject);
    }
}
