using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public static MainCharacterController Instance { get; private set; }

    public event EventHandler OnGrounded;
    public event EventHandler OnJumped;
    public event EventHandler<OnScoredEventArgs> OnScored;
    public class OnScoredEventArgs : EventArgs { public int score; }

    private const string GROUND_TAG_NAME = "Ground";
    private const string OBSTACLE_TAG_NAME = "Obstacle";
    private const string SCORE_MARK_TAG_NAME = "ScoreMark";

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpHeight = 5f;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private bool isGrounded = false;
    private int score = 0;

    private void Awake()
    {
        Instance = this;

        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING)
        {
            if (isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigidbody2d.gravityScale));

                    rigidbody2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

                    isGrounded = false;

                    OnJumped?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        switch (collision.collider.tag)
        {
            case GROUND_TAG_NAME:
                if (!isGrounded)
                {
                    if (HasGroundBelow())
                    {
                        isGrounded = true;

                        OnGrounded?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;

            case OBSTACLE_TAG_NAME:
                GameManager.Instance.OverTheGame();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case SCORE_MARK_TAG_NAME:
                score += 1;

                OnScored?.Invoke(this, new OnScoredEventArgs {
                    score = score
                });
                break;
        }
    }

    private bool HasGroundBelow()
    {
        RaycastHit2D hitInfo = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.5f, groundLayerMask);

        return hitInfo.collider != null;
    }

    public void RestoreInitialState()
    {
        score = 0;

        OnScored?.Invoke(this, new OnScoredEventArgs { score = 0 });

        rigidbody2d.velocity = Vector3.zero;

        transform.position = Vector3.zero;

        isGrounded = false;
    }
}
