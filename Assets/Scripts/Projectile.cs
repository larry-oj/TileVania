using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    [SerializeField] private float projectileSpeed = 10f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(projectileSpeed, 0f);
    }
}
