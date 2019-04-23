using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Shooter : MonoBehaviour
{
    //[Serializable]
 

    [Header("Settings")]
    public GameObject bulletPrefab;
    public KeyCode shootKey;
    public UnityEvent OnShoot;
    public Vector2 offset;


    [Header("Debug")]
    [SerializeField] private Transform m_Transform;

    void Start()
    {
        m_Transform = this.transform;
    }

 
    void Update()
    {
        if(Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        OnShoot.Invoke();
        Vector3 spawnOffset = (m_Transform.right * offset.x) +  (m_Transform.up * offset.y);

        Vector3 spawnPos = m_Transform.position + spawnOffset;

        Instantiate(bulletPrefab, spawnPos, m_Transform.rotation);
    }
}
