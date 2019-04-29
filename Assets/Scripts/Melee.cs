using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("Settings")]
    public float cooldown;
    public KeyCode meleeKey;
    public Transform meleePos;
    public float circleSize;
    public LayerMask enemyMask;
    public GameObject bloodPrefab;
    public string meleeAnimation;
    public int damage;

    [Header("Debug")]
    [SerializeField] private bool meleeReady;
    [SerializeField] private Animator m_Animator;

    void Awake()
    {
        meleeReady = true;
    }

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {  
        if(Input.GetKeyDown(meleeKey) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAnimation) == false)
        {
            m_Animator.Play(meleeAnimation);
        }
    }

    /*
    IEnumerator HitCooldown()
    {
        m_Animator.SetBool("IsPunching", true);
        yield return new WaitForEndOfFrame();
        m_Animator.SetBool("IsPunching", false);
        meleeReady = false;
        yield return new WaitForSeconds(cooldown);
        meleeReady = true;
        
    }
    */

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePos.position, circleSize);
    }

    public void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(meleePos.position, circleSize, enemyMask);
        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<EnemyDamageable>().TakeDamage(damage);
        }
    }
}
