using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemyManager : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public int damage;
    public float attackRange;
    public string attackAnimation;
    public string hitAnimation;
    public GameObject Biggums;
    public GameObject Smalls;
    public Transform meleePos;
    public float circleSize;
    public LayerMask playerMask;
    public float followRange;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_TransformBiggums;
    [SerializeField] private Transform m_TransformSmalls;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private float m_HitboxRange;

    private void Awake()
    {
        m_HitboxRange = meleePos.localPosition.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_TransformBiggums = Biggums.transform;
        m_TransformSmalls = Smalls.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToBiggums = m_TransformBiggums.position - m_Transform.position;
        Vector3 directionToSmalls = m_TransformSmalls.position - m_Transform.position;

        m_Animator.SetFloat("Velocity", m_Rigidbody2D.velocity.magnitude);

        if (directionToBiggums.magnitude <= followRange)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(hitAnimation) == false)
            {
                if (directionToBiggums.magnitude <= attackRange)
                {

                    m_Rigidbody2D.velocity = Vector2.zero;
                    if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
                    {
                        m_Animator.Play(attackAnimation);
                    }
                }
                else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
                {
                    m_Rigidbody2D.velocity = directionToBiggums.normalized * moveSpeed;
                    FlipSprite();
                }
            }
            else
            {
                m_Rigidbody2D.velocity = Vector2.zero;
            }
        }
        else if (directionToSmalls.magnitude <= followRange)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(hitAnimation) == false)
            {
                if (directionToSmalls.magnitude <= attackRange)
                {

                    m_Rigidbody2D.velocity = Vector2.zero;
                    if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
                    {
                        m_Animator.Play(attackAnimation);
                    }
                }
                else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
                {
                    m_Rigidbody2D.velocity = directionToSmalls.normalized * moveSpeed;
                    FlipSprite();
                }
            }
            else
            {
                m_Rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    private void FlipSprite()
    {
        if (m_Rigidbody2D.velocity.x > 0)
        {
            m_SpriteRenderer.flipX = false;
            meleePos.localPosition = new Vector3(m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
        }
        else
        {
            m_SpriteRenderer.flipX = true;
            meleePos.localPosition = new Vector3(-m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
        }
    }

    private void MeleeAttack()
    {
        Collider2D[] playersHit = Physics2D.OverlapCircleAll(meleePos.position, circleSize, playerMask);
        foreach (Collider2D player in playersHit)
        {
            player.GetComponent<PlayerDamageable>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePos.position, circleSize);
    }
}