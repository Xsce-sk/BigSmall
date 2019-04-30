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
    [SerializeField] SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        health = startHealth;
    }

    private void Start()
    {
        m_Transform = this.transform;
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject blood = Instantiate(bloodParticles, m_Transform.position, Quaternion.identity);
        Destroy(blood, 2);
        StartCoroutine(HitAnim());
    }

    IEnumerator HitAnim()
    {
        StartCoroutine(FlashRed());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FlashRed());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        m_SpriteRenderer.color = Color.white;
    }
}
