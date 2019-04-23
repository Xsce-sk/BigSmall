using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public string verticalAxis;
    public string horizontalAxis;

    [Header("Debug")]
    [SerializeField] private Rigidbody2D m_RigidBody2D;

    public void Awake()
    {
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        m_RigidBody2D.velocity = new Vector3(Input.GetAxis(horizontalAxis) * moveSpeed,
                                             Input.GetAxis(verticalAxis) * moveSpeed,
                                             0f);
    }
}
