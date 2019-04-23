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

    [Header("Debug")]
    [SerializeField] private bool meleeReady;

    // Start is called before the first frame update
    void Start()
    {
        meleeReady = true;
    }

    // Update is called once per frame
    void Update()
    {  
        if(Input.GetKeyDown(meleeKey) && meleeReady)
        {
            Debug.Log("Wiwin is Mr. Wins");
            StartCoroutine("HitCooldown");
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(meleePos.position, circleSize, enemyMask);
            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("We Hit an Enemy");
                GameObject blood = Instantiate(bloodPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(blood, 4);
                // Do something about it
            }
        }
    }

    IEnumerator HitCooldown()
    {
        Debug.Log("pringles");
        meleeReady = false;
        yield return new WaitForSeconds(cooldown);
        meleeReady = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePos.position, circleSize);
    }
}
