using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyManager : MonoBehaviour
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
    public Transform shieldTransform;
    public BoxCollider2D backCollider;
    public float followRange;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Transform m_Target;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Transform m_TransformBiggums;
    [SerializeField] private Transform m_TransformSmalls;
    [SerializeField] private float m_DistanceToBiggums;
    [SerializeField] private float m_DistanceToSmalls;
    [SerializeField] private float m_HitboxRange;

    private float shieldStartingX;
    private float backStartingX;

    private void Awake()
    {
        m_HitboxRange = meleePos.localPosition.x;
        shieldStartingX = shieldTransform.localPosition.x;
        backStartingX = backCollider.offset.x;
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
        Vector3 directionToSmalls = m_TransformSmalls.position - m_Transform.position;
        Vector3 directionToBiggums = m_TransformBiggums.position - m_Transform.position;

        m_DistanceToBiggums = directionToBiggums.magnitude;
        m_DistanceToSmalls = directionToSmalls.magnitude;

        m_Animator.SetFloat("Velocity", m_Rigidbody2D.velocity.magnitude);

        if (m_DistanceToBiggums < m_DistanceToSmalls)
        {
            if(m_DistanceToBiggums <= followRange)
                MoveToBiggums(directionToBiggums);
        }
        else
        {
            if(m_DistanceToSmalls <= followRange)
                MoveToSmalls(directionToSmalls);
        }
    }

    private void MoveToBiggums(Vector3 directionToBiggums)
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(hitAnimation) == false)
        {
            m_Rigidbody2D.velocity = directionToBiggums.normalized * moveSpeed;
            if (m_DistanceToBiggums <= attackRange)
            {
                m_Rigidbody2D.velocity = Vector2.zero;
                m_Animator.Play(attackAnimation);
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

    private void MoveToSmalls(Vector3 directionToSmalls)
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(hitAnimation) == false)
        {
            m_Rigidbody2D.velocity = directionToSmalls.normalized * moveSpeed;
            if (m_DistanceToSmalls <= attackRange)
            {
                m_Rigidbody2D.velocity = Vector2.zero;
                m_Animator.Play(attackAnimation);
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

    private void FlipSprite()
    {
        if (m_Rigidbody2D.velocity.x > 0)
        {
            m_SpriteRenderer.flipX = false;
            meleePos.localPosition = new Vector3(m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
            shieldTransform.localPosition = new Vector3(shieldStartingX, shieldTransform.localPosition.y, shieldTransform.localPosition.z);
            backCollider.offset = new Vector2(backStartingX, backCollider.offset.y);
        }
        else
        {
            m_SpriteRenderer.flipX = true;
            meleePos.localPosition = new Vector3(-m_HitboxRange, meleePos.localPosition.y, meleePos.localPosition.z);
            shieldTransform.localPosition = new Vector3(-shieldStartingX, shieldTransform.localPosition.y, shieldTransform.localPosition.z);
            backCollider.offset = new Vector2(-backStartingX, backCollider.offset.y);
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
