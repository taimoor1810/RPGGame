using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;
    public Animator animator;
         public AudioSource hitSound;
     public AudioSource dieSound;

    void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage (int damageAmount)
    {
        HP -= damageAmount;
        if(HP<=0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            dieSound.Play();
            Destroy(gameObject,3f);
        }
        else
        {
            animator.SetTrigger("damage");
            hitSound.Play();
        }
    }
}
