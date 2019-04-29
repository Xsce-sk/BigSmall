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

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_TransformBiggums;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();

        m_TransformBiggums = Biggums.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToBiggums = m_TransformBiggums.position - m_Transform.position;

        if(directionToBiggums.magnitude <= attackRange)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Animator.Play("SpikeEnemy_Attack");
            //do actual attack
        }
        else if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SpikeEnemy_Attack"))
        {
            m_Rigidbody2D.velocity = directionToBiggums.normalized * moveSpeed;
        }
    }
}
