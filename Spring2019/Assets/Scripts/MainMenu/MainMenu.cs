using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SFXObj;
    public GameObject mouse;
    public Animator anim; 
    public GameObject lookAtThis;
    private Vector3 lookCoords; 
    private bool animMouse;

    private void Start()
    {
        lookCoords = lookAtThis.GetComponent<Transform>().position;
    }

    private void Update()
    {
        if (animMouse)
        {
            mouse.GetComponent<Transform>().LookAt(lookCoords);
            anim.Play("RunForwardBattle");
            mouse.GetComponent<Rigidbody>().transform.Translate(transform.forward * Time.deltaTime * 5);
        }
    }

    public void playButton1(string scenename)
    {
        StartCoroutine(RollCredits()); 
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameCo());
    }

    public void quit()
    {
        Application.Quit();
    }

    IEnumerator PlayGameCo()  
    {
        SFXObj.GetComponent<MainMenuMusic>().PlayStartSFX();
        animMouse = true; 
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Level1");
    }

    IEnumerator RollCredits() 
    {
        SFXObj.GetComponent<MainMenuMusic>().PlayStartSFX();
        yield return new WaitForSeconds(1.3f);
        Application.LoadLevel("Credits");
    }
}
