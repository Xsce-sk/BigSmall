using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool m_IsShaking;
    [SerializeField] float m_CameraDist;
    [SerializeField] Vector3 m_CurrentPos;
    [SerializeField] Vector2 m_Offset;
    [SerializeField] Transform m_Transform;

    void Start()
    {
        m_Transform = transform;

        m_CameraDist = m_Transform.position.z;
    }

    public void Shake(float duration, float range = 0.1f, float step = 0.1f)
    {
        StartCoroutine(ShakeRoutine(duration, range, step));
    }

    IEnumerator ShakeRoutine(float duration, float range, float step)  
    {
        m_CurrentPos = m_Transform.position;

        while (duration > 0)
        {
            m_Offset = new Vector2(Random.Range(-range / 2, range / 2), Random.Range(-range / 2, range / 2));
            m_Transform.position = new Vector3(m_CurrentPos.x + m_Offset.x, m_CurrentPos.y + m_Offset.y, m_CameraDist);

            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;

            m_CurrentPos = new Vector2(m_Transform.position.x - m_Offset.x, m_Transform.position.y - m_Offset.y);
        }

        m_Transform.position = new Vector3(m_CurrentPos.x, m_CurrentPos.y, m_CameraDist);
        m_Offset = new Vector2();
    }
}
