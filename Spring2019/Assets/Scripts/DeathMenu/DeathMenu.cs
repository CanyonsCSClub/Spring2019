using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public Animator anim;
    public AudioSource sound;
    public AudioSource music; 

	void Start ()
    {
        anim.Play("Die"); 
	}

    public void Retry()
    {
        music.enabled = false;
        sound.enabled = true; 
        StartCoroutine(GetUpMouse()); 
    }

    public void MainMenu()
    {
        music.enabled = false;
        sound.enabled = true;
        StartCoroutine(GoToMain());
    }

    IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel("Madian-MainMenu");
    }

    IEnumerator GetUpMouse()
    {
        anim.Play("DieRecover");
        yield return new WaitForSeconds(1f); 
        Application.LoadLevel("Level1");
    }
}
