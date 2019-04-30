using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    public GameObject bloodParticles;
    [SerializeField] public static int health;
    public int startHealth = 10;

    [Header("Debug")]
    [SerializeField] Transform m_Transform;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
        health = startHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject blood = Instantiate(bloodParticles, m_Transform.position, Quaternion.identity);
        Destroy(blood, 4);
    }

    void Update()
    {
     
    }
}
