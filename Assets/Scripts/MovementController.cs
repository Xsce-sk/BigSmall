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
    public Transform meleePos;

    [Header("Debug")]
    [SerializeField] private Rigidbody2D m_RigidBody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Aimer m_Aimer;
    [SerializeField] private float m_HitboxRange;


    public void Awake()
    {
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Aimer = GetComponentInChildren<Aimer>();
        if(meleePos != null)
            m_HitboxRange = meleePos.localPosition.x;
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
            {
                m_SpriteRenderer.flipX = false;
                meleePos.localPosition = new Vector3(m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
            }
            else if (Input.GetAxis(horizontalAxis) < 0)
            {
                m_SpriteRenderer.flipX = true;
                meleePos.localPosition = new Vector3(-m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
            }
        }
        
    }

    void UpdateAnimationController()
    {
        m_Animator.SetFloat("Velocity", m_RigidBody2D.velocity.magnitude);
    }
}
