using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isometric : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 offset;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Vector3 m_CurrentPosition;
    [SerializeField] private Vector3 m_CheckLocation;

    void Start()
    {
        m_Transform = this.transform;
    }

    void Update()
    {
        m_CurrentPosition = m_Transform.position;
        m_CheckLocation = new Vector3(m_CurrentPosition.x + offset.x, m_CurrentPosition.y - m_Transform.localScale.y / 2 + offset.y, m_CurrentPosition.x + offset.z);
        Debug.DrawRay(m_CheckLocation, new Vector3 (-1, 0, 0), Color.green);
        m_Transform.position = new Vector3(m_CurrentPosition.x, m_CurrentPosition.y, m_CheckLocation.y);
    }
}
