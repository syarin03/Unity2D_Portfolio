using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("Move Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Koyote Time")]
    [SerializeField] private float coyoteTime;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float sizeX;
    private float sizeY;

    private int jumpCounter;
    private float coyoteCounter;

    private float horizontalInput;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        sizeX = transform.localScale.x;
        sizeY = transform.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(sizeX, sizeY, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-sizeX, sizeX, 1);

        anim.SetBool("isRun", horizontalInput != 0);
        anim.SetBool("isGrounded", IsGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && rigid.velocity.y > 0)
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y / 2);

        if (OnWall())
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector3.zero;
        }
        else
        {
            rigid.gravityScale = 7;
            rigid.velocity = new Vector2(horizontalInput * speed, rigid.velocity.y);

            if (IsGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !OnWall() && jumpCounter <= 0) return;

        if (OnWall())
            WallJump();
        else
        {
            if (IsGrounded())
                rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            else
            {
                if (coyoteCounter > 0)
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0)
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        rigid.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}
