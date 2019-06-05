using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour {


    

    public void playButton1(string scenename)
    {
        Application.LoadLevel(scenename);
        
    }

    public void quit()
    {
        Application.Quit();
    }
}
