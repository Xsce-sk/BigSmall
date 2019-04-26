using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltronScript : MonoBehaviour
{
    public Transform smallsTransform;
    public Transform biggumsTransform;
    public Rigidbody2D smallsRB2D;
    public MovementController smallsMC;
    public float voltronDistance = 1f;
    public KeyCode voltronKey;
    public Transform biggumBack;

    [SerializeField] private bool onBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onBack && Input.GetKeyDown(voltronKey))
        {
            Debug.Log("aaa");
            smallsRB2D.simulated = true;
            smallsMC.enabled = true;
            onBack = false;
        }

        if (WithinVoltronDistance() && Input.GetKeyDown(voltronKey) && !onBack)
        {
            smallsTransform.localPosition = biggumBack.localPosition;
            smallsRB2D.simulated = false;
            smallsMC.enabled = false;
            onBack = true;
        }
    }

    bool WithinVoltronDistance()
    {
        if(Vector3.Distance(smallsTransform.position, biggumsTransform.position) <= voltronDistance)
            return true;

        return false;
    }
}
