using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Shooter : MonoBehaviour
{
    [Header("Settings")]
    public GameObject bulletPrefab;
    public KeyCode shootKey;
    public float cooldown;
    public Vector2 offset;
    

    [Header("Sounds")]
    public List<AudioClip> shootSounds;
    public float shootVolume;

    [Header("Events")]
    public UnityEvent OnShoot;

    [Header("Debug")]
    [SerializeField] private bool m_CanShoot;
    [SerializeField] Transform m_Transform;
    [SerializeField] AudioSource m_AudioSource;


    private void Awake()
    {
        m_CanShoot = true;
    }

    void Start()
    {
        m_Transform = this.transform;
        m_AudioSource = this.GetComponent<AudioSource>();
    }

 
    void Update()
    {
        if(Input.GetKey(shootKey) && m_CanShoot)
        {
            Shoot();
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        m_CanShoot = false;
        yield return new WaitForSeconds(cooldown);
        m_CanShoot = true;
    }

    public void Shoot()
    {
        PlayShootSound();

        Vector3 spawnOffset = (m_Transform.right * offset.x) +  (m_Transform.up * offset.y);
        Vector3 spawnPos = m_Transform.position + spawnOffset;
        Instantiate(bulletPrefab, spawnPos, m_Transform.rotation);

        OnShoot.Invoke();
    }

    void PlayShootSound()
    {
        m_AudioSource.volume = shootVolume;
        m_AudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        m_AudioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);
    }
}
