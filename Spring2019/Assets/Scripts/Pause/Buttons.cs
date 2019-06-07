using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
	public bool isWork;
    public GameObject PauseMenuUI;

    void Start()
	{
		// isWork = true;
	}

	public void LeaveLevel()
    {
        // isWork = true; 
        Time.timeScale = 1f;
        Application.LoadLevel("Madian-MainMenu");

    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameObject.Find("Canvas").GetComponent<PauseM>().SetBool();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
