using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{

    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _jumpForce;

    [SerializeField] private Transform _groundPosLeft;
    [SerializeField] private Transform _groundPosRight;
    [SerializeField] private LayerMask _groundLayer;


    private Rigidbody2D _rb;

    private bool _isGrounded;
    private float _moveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGround();
        Walk();
    }

    public float GetVerticalSpeed() => _rb.velocity.y;
    public float GetHorizontalSpeed() => Input.GetAxis("Horizontal");

    public bool GetIsGrounded() => _isGrounded;

    private void Walk()
    {
        _rb.velocity = new Vector2(_moveInput * _moveSpeed, _rb.velocity.y);
        if (_moveInput < 0f) transform.localScale = new Vector3(-1, 1, 1);
        else if (_moveInput > 0f) transform.localScale = new Vector3(1, 1, 1);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    private bool CheckGround()
    {
        Collider2D[] colliderLeft = Physics2D.OverlapCircleAll(_groundPosLeft.position, 0.1f, _groundLayer);
        Collider2D[] colliderRight = Physics2D.OverlapCircleAll(_groundPosRight.position, 0.1f, _groundLayer);
        return colliderLeft.Length + colliderRight.Length > 0;
    }
}
