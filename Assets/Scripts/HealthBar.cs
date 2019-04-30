using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int fullHealth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector2(((float) PlayerDamageable.health / fullHealth )* 2f, this.transform.localScale.y);
    }
}
