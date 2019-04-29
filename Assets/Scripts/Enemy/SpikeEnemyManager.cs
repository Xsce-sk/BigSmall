using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemyManager : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float attackRange;
    public string attackAnimation;
    public GameObject Biggums;
    public Transform meleePos;
    public float circleSize;
    public LayerMask playerMask;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_TransformBiggums;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_TransformBiggums = Biggums.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToBiggums = m_TransformBiggums.position - m_Transform.position;

        if (directionToBiggums.magnitude <= attackRange)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Animator.Play(attackAnimation);
            //do actual attack
        }
        else if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation) == false)
        {
            m_Rigidbody2D.velocity = directionToBiggums.normalized * moveSpeed;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if(m_Rigidbody2D.velocity.x > 0)
        {
            m_SpriteRenderer.flipX = false;
        }
        else
        {
            m_SpriteRenderer.flipX = true;
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine("HitCooldown");
        Collider2D[] playersHit = Physics2D.OverlapCircleAll(meleePos.position, circleSize, playerMask);
        foreach (Collider2D enemy in playersHit)
        {

        }
    }
}