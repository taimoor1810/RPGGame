using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saved : MonoBehaviour
{
     public Animator animator;
     public AudioSource saveSound;

      private IEnumerator WaitForSceneLoad() {
     yield return new WaitForSeconds(4);
     SceneManager.LoadScene("GameOverScene");
     
 }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("You saved the girl");
            animator.SetBool("isSaved", true);
            saveSound.Play();
             StartCoroutine(WaitForSceneLoad());

     }
}

}