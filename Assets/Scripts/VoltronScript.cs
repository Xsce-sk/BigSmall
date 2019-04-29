using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltronScript : MonoBehaviour
{
    [Header("Settings")]
    public float voltronDistance;
    public float voltronDuration;
    public float jumpoffDistance;
    public KeyCode voltronKey;

    [Header("Biggums")]
    public Transform biggumsTransform;
    public Transform biggumBack;

    [Header("Debug")]
    [SerializeField] private Transform m_Transform;
    [SerializeField] private MovementController m_MovementController;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private bool m_OnBack = false;
    [SerializeField] private bool m_IsAcceptingInput = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = transform;
        m_MovementController = GetComponent<MovementController>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WithinVoltronDistance() && Input.GetKeyDown(voltronKey) && !m_OnBack && m_IsAcceptingInput)
        {
            m_Animator.SetBool("IsJumping", true);
            StartCoroutine(Combine());

            m_MovementController.enabled = false;
            m_OnBack = true;

            m_Rigidbody2D.velocity = Vector2.zero;
            m_Animator.SetFloat("Velocity", 0);
        }

        if (m_OnBack && Input.GetKeyDown(voltronKey) && m_IsAcceptingInput)
        {
            m_Transform.parent = null;
            
            m_OnBack = false;

            m_Animator.SetBool("IsJumping", true);
            StartCoroutine(Disassemble());
        }
    }

    IEnumerator Combine()
    {
        m_IsAcceptingInput = false;
        float t = 0;
        Vector3 startPos = m_Transform.position;

        while (t < 1)
        {
            m_Transform.position = Vector3.Lerp(startPos, biggumBack.position, t);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime/voltronDuration;
        }

        m_Transform.parent = biggumsTransform;
        m_Transform.localPosition = biggumBack.localPosition;

        m_Animator.SetBool("IsJumping", false);
        m_IsAcceptingInput = true;
    }

    IEnumerator Disassemble()
    {
        m_IsAcceptingInput = false;
        float t = 0;
        Vector3 startPos = m_Transform.position;
        Vector3 targetPos = Vector3.MoveTowards(startPos, Camera.main.transform.position, jumpoffDistance);
        while (t < 1)
        {
            m_Transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / voltronDuration;
        }

        m_MovementController.enabled = true;
        m_Animator.SetBool("IsJumping", false);
        m_IsAcceptingInput = true;
    }

    bool WithinVoltronDistance()
    {
        if(Vector3.Distance(m_Transform.position, biggumsTransform.position) <= voltronDistance)
            return true;

        return false;
    }
}
