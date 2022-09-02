using System;
using Components;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damagejumpForce;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private LayerMask _interactionLayer;
    
    private int _money;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private static readonly int IsGroundKey = Animator.StringToHash("IsGround");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int VelocityY = Animator.StringToHash("VelocityY");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private bool IsGrounded => _groundCheck.isGround;

    private void Awake()
    {
        _money = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
        if (_direction.x < 0)
            _spriteRenderer.flipX = true;
        else if (_direction.x > 0)
            _spriteRenderer.flipX = false;
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _moveSpeed, _rigidbody.velocity.y);
        Jump();
        SetAnimatorParameters();
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

    public void AddMoney(int money)
    {
        _money += money;

    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpForce);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            int size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);
            for (int i = 0; i < size; i++)
            {
               var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
               if (interactable != null)
               {
                   interactable.Interact();
               }
            }
        }
    }
}
