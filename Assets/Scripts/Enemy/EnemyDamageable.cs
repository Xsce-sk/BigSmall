using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    public GameObject bloodParticles;
    public int health;

    [Header("Debug")]
    [SerializeField] Transform m_Transform;
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    [SerializeField] Animator m_Animator;

    void Start()
    {
        m_Transform = this.transform;
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject blood = Instantiate(bloodParticles, m_Transform.position, Quaternion.identity);
        Destroy(blood, 4);

        StartCoroutine(HitAnim());
    }

    IEnumerator HitAnim()
    {
        m_Animator.SetBool("IsHit", true);

        StartCoroutine(FlashRed());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FlashRed());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FlashRed());

        m_Animator.SetBool("IsHit", false);
    }

    IEnumerator FlashRed()
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        m_SpriteRenderer.color = Color.white;
    }
}
