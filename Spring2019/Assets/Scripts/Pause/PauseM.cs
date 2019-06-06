﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseM : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject PauseMenuUI;
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume();
			} 
			else {
				Pause();
			}
		}
	}

	void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

    public void SetBool()
    {
        GameIsPaused = false; 
    }
		
}