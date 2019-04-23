using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [Header("Settings")]
    public float stepInterval;
    public AudioClip stepSound;

    [Header("Debug")]
    [SerializeField] private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();

        StartCoroutine(StepLoop());
    }

    IEnumerator StepLoop()
    {
        while(true)
        {
            m_AudioSource.pitch = Random.Range(0.9f, 1.1f);
            m_AudioSource.PlayOneShot(stepSound);
            yield return new WaitForSeconds(stepInterval);
        }
    }
}
