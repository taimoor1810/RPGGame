using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeHit : MonoBehaviour
{
     private int HP = 100;
    public Slider healthBar;
    public Animator animator;
    public GameObject particleEffect = null;
     public AudioSource hitSound;
     public AudioSource dieSound;
    void Update()
    {
        healthBar.value = HP;
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "AttackArm")
        {
           HP -= 5;
           GameObject obj = Instantiate(particleEffect);
            obj.transform.position = this.transform.position;
            hitSound.Play();
        if(HP<=0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            dieSound.Play();
            Destroy(gameObject,3f);
        }
        // else
        // {
        //     animator.SetTrigger("damage");
        // }
        }
    }
}
