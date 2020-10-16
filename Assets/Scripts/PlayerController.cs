using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  https://pixelfrog-store.itch.io/treasure-hunters?download
/// </summary>

public class PlayerController : MonoBehaviour
{
    // Inspector variables
    public float movementSpeed = 1f;
    public float jumpForce = 1f;
    public int life = 3;

    // Component caching
    private Rigidbody2D m_rigidBody = null;
    private SpriteRenderer m_spriteRenderer = null;
    private PlayerAnimationController m_animatorController = null;
    private CameraShake m_cameraShake = null;

    // Component variables
    [HideInInspector]
    public bool m_wasNotGrounded = false;
    [HideInInspector]
    public bool m_isGrounded = true;
    private float m_isLookingRight = 1f;

    private bool m_invulnerable;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animatorController = GetComponent<PlayerAnimationController>();
        m_cameraShake = GetComponentInChildren<CameraShake>();
    }

    private void Start()
    {
        PlayerUIController.UpdateHealth(life);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        bool jumping = m_isGrounded && Input.GetAxisRaw("Jump") != 0f;
        Vector2 newVelocity = m_rigidBody.velocity;

        if (xAxis != 0f && xAxis != m_isLookingRight)
        {
            m_isLookingRight = xAxis;
            m_spriteRenderer.flipX = m_isLookingRight == -1f;
        }

        if (m_isGrounded && jumping)
        {
            newVelocity.y = jumpForce;
            m_isGrounded = false;
        }
        newVelocity.x = movementSpeed * Input.GetAxisRaw("Horizontal");
        m_rigidBody.velocity = newVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        m_wasNotGrounded = !m_isGrounded;
        m_isGrounded = true;
        if (!m_invulnerable && collision.gameObject.CompareTag("Enemy"))
        {
            m_animatorController.DamageAnimation();
            m_cameraShake.Shake();
            m_invulnerable = true;
            life--;
            PlayerUIController.UpdateHealth(life);
            Invoke("RestoreInvulnerability", .25f);
        }
    }

    private void RestoreInvulnerability()
    {
        m_invulnerable = false;
    }
}
