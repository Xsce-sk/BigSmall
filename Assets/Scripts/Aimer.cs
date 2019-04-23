using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public bool lookAtMouse;

    private Camera m_MainCamera;
    private Vector3 m_MousePos;
    private Vector3 m_Direction;
    private float m_Angle;

    [Header("Debug")]
    [SerializeField] Transform m_Transform;
    [SerializeField] SpriteRenderer m_GunSpriteRenderer;

    private void Start()
    {
        m_Transform = this.transform;
        m_GunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (lookAtMouse)
        {
            m_MousePos = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);

            m_Direction = m_MousePos - transform.position;
            m_Angle = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
        }

        FlipSprite();
    }

    void FlipSprite()
    {
        if (m_Angle > 80 || m_Angle < -90)
            m_GunSpriteRenderer.flipY = true;
        else
            m_GunSpriteRenderer.flipY = false;
    }

    public bool IsGunFacingLeft()
    {
        return m_GunSpriteRenderer.flipY;
    }
}
