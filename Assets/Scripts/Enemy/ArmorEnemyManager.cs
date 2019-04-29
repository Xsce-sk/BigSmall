using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemyManager : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public GameObject Smalls;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_TransformSmalls;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();

        m_TransformSmalls = Smalls.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToSmalls = m_Transform.position - m_TransformSmalls.position;

        m_Rigidbody2D.velocity = directionToSmalls.normalized * moveSpeed;
    }
}
