using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyManager : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public GameObject Biggums;
    public GameObject Smalls;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_TransformBiggums;
    [SerializeField] private Transform m_TransformSmalls;
    [SerializeField] float m_DistanceToBiggums;
    [SerializeField] float m_DistanceToSmalls;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();

        m_TransformBiggums = Biggums.transform;
        m_TransformSmalls = Smalls.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToBiggums = m_Transform.position - m_TransformBiggums.position;
        Vector3 directionToSmalls = m_Transform.position - m_TransformSmalls.position;

        m_DistanceToBiggums = directionToBiggums.magnitude;
        m_DistanceToSmalls = directionToSmalls.magnitude;

        if(m_DistanceToBiggums < m_DistanceToSmalls)
        {
            m_Rigidbody2D.velocity = directionToBiggums.normalized * moveSpeed;
        }
        else
        {
            m_Rigidbody2D.velocity = directionToSmalls.normalized * moveSpeed;
        }

        
    }
}
