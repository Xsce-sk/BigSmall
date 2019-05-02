using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    [Serializable]
    public class OnDeathEvent : UnityEvent<PlayerDamageable>
    { }

    public OnDeathEvent OnDeath;

    [Header("Settings")]
    public GameObject bloodParticles;
    public Sprite deathSprite;

    [SerializeField] public static int health;
    public int startHealth = 10;

    [Header("Debug")]
    [SerializeField] Transform m_Transform;
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    [SerializeField] MovementController m_MovementController;
    [SerializeField] Animator m_Animator; 

    private void Awake()
    {
        health = startHealth;
    }

    private void Start()
    {
        m_Transform = this.transform;
        m_MovementController = GetComponent<MovementController>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {  
        if (health > 0)
        {
            health -= damage;
            if (health == 0)
                OnDeath.Invoke(this);
            GameObject blood = Instantiate(bloodParticles, m_Transform.position, Quaternion.identity);
            Destroy(blood, 2);
            StartCoroutine(HitAnim());
        }
    }

    public void PlayerDeath()
    {
        m_MovementController.enabled = false;
        m_Animator.enabled = false;
        m_SpriteRenderer.sprite = deathSprite;
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
