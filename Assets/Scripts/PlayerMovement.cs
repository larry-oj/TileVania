using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private CapsuleCollider2D _collider;
    private BoxCollider2D _feetCollider;
    private float _defaultGravity;
    private bool _isAlive;

    [Header("Player Movement Settings")]
    [SerializeField] private float maximumVelocity = 5;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float climbSpeed = 5f;
    
    [Header("Player Shooting Settings")]
    [SerializeField] private GameObject projectile;

    private void Start()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
        _collider = this.GetComponent<CapsuleCollider2D>();
        _feetCollider = this.GetComponent<BoxCollider2D>();
        _defaultGravity = _rigidBody.gravityScale;
        _isAlive = true;
    }
    
    private void Update()
    {
        if (!_isAlive) return;
        
        Run();
        FlipSprite();
        ChangeAnimation();
        ClimbLadder();
        Die();
    }
    
    private void OnMove(InputValue value)
    {
        if (!_isAlive) return;
        
        _moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!_isAlive) return;
        
        if (!value.isPressed) return;
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        _rigidBody.velocity += new Vector2(0f, jumpSpeed);
    }
    
    private void OnFire(InputValue value)
    {
        if (!_isAlive) return;
        
        if (!value.isPressed) return;

        Instantiate(projectile, transform.position, Quaternion.identity);
    }
    
    private void Run()
    {
        var playerVelocity = new Vector2(
            _moveInput.x * maximumVelocity, 
            _rigidBody.velocity.y);
        
        _rigidBody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        // if player is moving on x direction, flip his sprite
        // we need Epsilon, because having x speed at 0 will always flip model to facing left
        if (CheckPlayerHorizontalSpeed())
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1f);
    }

    private void ChangeAnimation()
    {
        // change IsRunning state
        _animator.SetBool($"IsRunning", CheckPlayerHorizontalSpeed());
    }

    private void ClimbLadder()
    {
        if (!_collider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _rigidBody.gravityScale = _defaultGravity;
            _animator.SetBool($"IsClimbing", false);
            return;
        }

        var playerVelocity = new Vector2(
            _rigidBody.velocity.x, 
            _moveInput.y * climbSpeed);
        
        _rigidBody.velocity = playerVelocity;
        _rigidBody.gravityScale = 0;

        _animator.SetBool($"IsClimbing", Mathf.Abs(_rigidBody.velocity.y) > Mathf.Epsilon);
    }

    private void Die()
    {
        if (!_collider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard"))) return;
        
        _isAlive = false;
        _animator.SetTrigger($"IsDead");
        _rigidBody.velocity = new Vector2(0f, jumpSpeed);
        
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private bool CheckPlayerHorizontalSpeed()
        => Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;
}
