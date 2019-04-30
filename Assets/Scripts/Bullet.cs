using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float bulletSpeed;
    public int damage;
    
    [Header("Debug")]
    [SerializeField] Transform m_Transform;
    [SerializeField] Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Transform = this.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(m_Transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            EnemyDamageable damageableComponent = collision.GetComponent<EnemyDamageable>();
            
            if (damageableComponent != null)
            {
                Debug.Log("a");
                if (collision.CompareTag("ArmorEnemy") && collision.GetComponent<ArmorEnemyManager>().HasArmor() == false)
                {
                    Debug.Log("b");
                    damageableComponent.TakeDamage(damage);
                }
                else if(collision.CompareTag("ArmorEnemy") == false)
                {
                    Debug.Log("c");
                    damageableComponent.TakeDamage(damage);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
