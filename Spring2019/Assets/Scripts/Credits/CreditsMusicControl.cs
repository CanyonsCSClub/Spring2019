using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusicControl : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    public float m_MySliderValue;
    public bool musicSlower; 

    void Start()
    {
        m_MySliderValue = 0.5f;
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.Play();
        StartCoroutine(MusicTimer()); 
    }

    private void Update()
    {
        if (musicSlower && m_MySliderValue > 0)
        {
            m_MySliderValue = m_MySliderValue - 0.003f;
            m_MyAudioSource.volume = m_MySliderValue;
        }
        else if (m_MySliderValue <= 0)
        {
            musicSlower = false; 
        }
    }

    IEnumerator MusicTimer()
    {
        yield return new WaitForSeconds(22);
        musicSlower = true; 
    }
}