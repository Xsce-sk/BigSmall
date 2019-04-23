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

    protected Transform m_Transform;

    #region Public Functions

    #endregion

    private void Start()
    {
        m_Transform = this.transform;

        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (lookAtMouse)
        {
            //Debug.Log("sup");
            m_MousePos = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(m_MousePos);

            m_Direction = m_MousePos - transform.position;
            //Debug.Log(m_Direction);
            m_Angle = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
            //Debug.Log(m_Angle);
            transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
        }
    }
}
