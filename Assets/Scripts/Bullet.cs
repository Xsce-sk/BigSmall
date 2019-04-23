using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float bulletSpeed;
    
    [Header("Debug")]
    [SerializeField] Transform m_Transform;
    [SerializeField] Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(m_Transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

}
