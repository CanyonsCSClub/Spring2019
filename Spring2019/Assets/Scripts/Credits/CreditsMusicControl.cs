/*
 * Programmer:   Hunter Goodin 
 * Date Created: 05/24/2019 @  8:15 PM 
 * Last Updated: 08/13/2019 @  2:20 PM by Hunter Goodin
 * File Name:    CreditsMusicControl.cs 
 * Description:  This script will be responsible for 
 * 				 controlling the music on the main menu. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusicControl : MonoBehaviour
{
    AudioSource m_MyAudioSource; 	// The music
    public float m_MySliderValue;	// The volume of the music
    public bool musicSilencer; 		// When this is toggled, the music will begin to quiet down. 

    void Start()
    {
        m_MySliderValue = 0.5f;							// slider is set to half
        m_MyAudioSource = GetComponent<AudioSource>();	// set the audio source to the music
        m_MyAudioSource.Play();							// Play the music
        StartCoroutine(MusicTimer()); 					// Start count down to quiet town 
    }

    private void Update()								// every frame... 
    {
        if (musicSilencer && m_MySliderValue > 0)			// if musicSilencer is true and the volume isn't already 0... 
        {
            m_MySliderValue = m_MySliderValue - 0.003f;	// decrease volumme by this much 
            m_MyAudioSource.volume = m_MySliderValue;	// set the volume equal to m_MySliderValue 
        }
        else if (m_MySliderValue <= 0)					// If the volume is alread 0, 
        {
            musicSilencer = false; 						// stop quieting the music
        }
    }

    IEnumerator MusicTimer()					// This coroutine is to time when the music should be slowed. 
    {
        yield return new WaitForSeconds(22);	// In 22 seconds... 
        musicSilencer = true; 					// Set musicSilencer to true so the music will start quieting 
    }	
}