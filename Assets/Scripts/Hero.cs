using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundCheck _groundCheck;
    
    private int _money;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    
    private static readonly int IsGroundKey = Animator.StringToHash("IsGround");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int VelocityY = Animator.StringToHash("VelocityY");
    
    private bool IsGrounded => _groundCheck.isGround;

    private void Awake()
    {
        _money = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _moveSpeed, _rigidbody.velocity.y);
        Jump();
        SetAnimatorParameters();
        UpdateSpriteFlip();



    }

    private void UpdateSpriteFlip()
    {
        _spriteRenderer.flipX = _rigidbody.velocity.x < 0;
    }
    private void SetAnimatorParameters()
    {
        _animator.SetBool(IsRunning, _direction.x != 0);
        _animator.SetFloat(VelocityY, _rigidbody.velocity.y);
        _animator.SetBool(IsGroundKey, IsGrounded);
    }

    private void Jump()
    {
        bool isJumping = _direction.y > 0;
        if (isJumping && IsGrounded)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void AddMoney(int addmoney)
    {
        _money += addmoney;

    }
}
