using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemyManager : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float attackRange;
    public string attackAnimation;
    public GameObject Smalls;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_TransformSmalls;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_TransformSmalls = Smalls.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToSmalls = m_TransformSmalls.position - m_Transform.position;

        if (directionToSmalls.magnitude <= attackRange)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Animator.Play(attackAnimation);
            //do actual attack
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
        {
            m_Rigidbody2D.velocity = directionToSmalls.normalized * moveSpeed;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if (m_Rigidbody2D.velocity.x > 0)
        {
            m_SpriteRenderer.flipX = false;
        }
        else
        {
            m_SpriteRenderer.flipX = true;
        }
    }
}