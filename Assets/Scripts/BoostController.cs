using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    public bool used = false;
    public bool useDirection = false;
    public Vector3 direction;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!used)
        {
            used = true;
            if(other.gameObject.name == "Cart")
            {
                if(useDirection)
                {
                    other.GetComponent<CartController>().AddBoost(force, direction);
                }
                else
                {
                    other.GetComponent<CartController>().AddBoost(force);
                }
            }
        }
    }
}
