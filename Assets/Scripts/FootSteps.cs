﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootSteps : MonoBehaviour
{
    [Header("Settings")]
    public float stepInterval;
    public AudioClip stepSound;
    public UnityEvent OnStep;

    [Header("Debug")]
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] Rigidbody2D m_Rigidbody2D;
    [SerializeField] CameraShake m_CameraShake;

    private void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_CameraShake = Camera.main.GetComponent<CameraShake>();

        StartCoroutine(StepLoop());
    }

    IEnumerator StepLoop()
    {
        while (true)
        {
            if (m_Rigidbody2D.velocity != Vector2.zero)
            {
                m_AudioSource.pitch = Random.Range(0.9f, 1.1f);
                m_AudioSource.PlayOneShot(stepSound);
                m_CameraShake.Shake(0.1f);

                yield return new WaitForSeconds(stepInterval);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
