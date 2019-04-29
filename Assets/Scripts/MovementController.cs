using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public string verticalAxis;
    public string horizontalAxis;
    public bool turnWithGun;

    [Header("Debug")]
    [SerializeField] private Rigidbody2D m_RigidBody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Aimer m_Aimer;

    public void Awake()
    {
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Aimer = GetComponentInChildren<Aimer>();
    }

    public void Update()
    {
        m_RigidBody2D.velocity = new Vector3(Input.GetAxis(horizontalAxis) * moveSpeed,
                                             Input.GetAxis(verticalAxis) * moveSpeed,
                                             0f);

        FlipSprite();
        UpdateAnimationController();
    }

    void FlipSprite()
    {
        if (turnWithGun)
        {
            m_SpriteRenderer.flipX = m_Aimer.IsGunFacingLeft();
        }
        else
        {
            if (Input.GetAxis(horizontalAxis) > 0)
                m_SpriteRenderer.flipX = false;
            else if (Input.GetAxis(horizontalAxis) < 0)
                m_SpriteRenderer.flipX = true;
        }
        
    }

    void UpdateAnimationController()
    {
        m_Animator.SetFloat("Velocity", m_RigidBody2D.velocity.magnitude);
    }
}
