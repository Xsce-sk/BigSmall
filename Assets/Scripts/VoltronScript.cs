using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltronScript : MonoBehaviour
{
    public Transform smallsTransform;
    public Rigidbody2D smallsRB2D;
    public Animator smallsAnimator;
    public Transform biggumsTransform;
    public MovementController smallsMC;
    public float voltronDistance = 1f;
    public KeyCode voltronKey;
    public Transform biggumBack;
    
    //public float cooldown;

    [SerializeField] private bool onBack = false;
    [SerializeField] private bool acceptInput = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onBack && Input.GetKeyDown(voltronKey) && acceptInput)
        {
            Debug.Log("aaa");
            smallsTransform.parent = null;
            Debug.Log("b");
            smallsMC.enabled = true;
            Debug.Log("c");
            onBack = false;
            StartCoroutine(HitCooldown());
        }

        if (WithinVoltronDistance() && Input.GetKeyDown(voltronKey) && !onBack && acceptInput)
        {
            smallsTransform.parent = biggumsTransform;
            smallsTransform.localPosition = biggumBack.localPosition;
            smallsMC.enabled = false;
            onBack = true;
            smallsRB2D.velocity = Vector2.zero;
            smallsAnimator.SetFloat("Velocity", 0);
            StartCoroutine(HitCooldown());
        }
    }

    IEnumerator HitCooldown()
    {
        acceptInput = !acceptInput;
        yield return new WaitForSeconds(Time.deltaTime);
        acceptInput = !acceptInput;

    }

    bool WithinVoltronDistance()
    {
        if(Vector3.Distance(smallsTransform.position, biggumsTransform.position) <= voltronDistance)
            return true;

        return false;
    }
}
