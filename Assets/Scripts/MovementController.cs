using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public string verticalAxis;
    public string horizontalAxis;

    [Header("Debug")]
    [SerializeField] private Rigidbody2D m_RigidBody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Transform m_Transform;
    [SerializeField] int m_SpriteDir;

    public void Awake()
    {
        m_SpriteDir = 1;

        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_Transform = transform;
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
        if (Input.GetAxis(horizontalAxis) < 0)
            m_SpriteDir = -1;
        else if (Input.GetAxis(horizontalAxis) > 0)
            m_SpriteDir = 1;
        m_Transform.localScale = new Vector3(m_SpriteDir, m_Transform.localScale.y, 1);
    }

    void UpdateAnimationController()
    {
        m_Animator.SetFloat("HorizontalAxis", Input.GetAxis(horizontalAxis));
        m_Animator.SetFloat("VerticalAxis", Input.GetAxis(verticalAxis));
        m_Animator.SetFloat("Velocity", m_RigidBody2D.velocity.x + m_RigidBody2D.velocity.y);
    }
}
