using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public GameObject particleEffect = null;
    public int damageAmount = 10;
   private void OnTriggerEnter (Collider other)
   {    
        if(other.tag == "Enemy")
        {
            GameObject obj = Instantiate(particleEffect);
            obj.transform.position = this.transform.position;
            other.GetComponent<Dragon>().TakeDamage(damageAmount);
            Destroy(gameObject,0.5f);
        }
   }
}
