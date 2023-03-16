using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealBehaviour>().heatTrigger = true; 
        }
    }
}
