using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    public GameObject bloodParticles;
    public int health;

    [Header("Debug")]
    [SerializeField] bool m_IsDead;
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
        if (!m_IsDead)
        {
            health -= damage;
            GameObject blood = Instantiate(bloodParticles, m_Transform.position, Quaternion.identity);
            Destroy(blood, 2);
            StartCoroutine(HitAnim());

            if (health <= 0)
            {
                StartCoroutine(Die());
                m_IsDead = true;
            }
        }
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

    IEnumerator Die()
    {
        m_Animator.SetBool("IsHit", true);

        Color targetColor = new Color(0, 0, 0, 0);

        Vector3 startSize = m_Transform.localScale;
        Vector3 targetSize = new Vector3(0, 0, 0);

        float t = 0;
        while (t < 1)
        {
            m_SpriteRenderer.color = Color.Lerp(Color.white, targetColor, t);
            m_Transform.localScale = Vector3.Lerp(startSize, targetSize, t);
            m_Transform.Rotate(new Vector3(0, 0, t*10));

            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        Destroy(this);
    }
}
