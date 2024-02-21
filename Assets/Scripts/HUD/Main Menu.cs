using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject controlMenu;
    public GameObject optionsMenu;

   public float VolMaster;

   public void StarGame()
   {
        SceneManager.LoadScene(1);
   }

    public void Control()
    {
        controlMenu.SetActive(true);
    }

    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("saiu");
    }

    public void musicBackGroundMenu(float vol)
    {
        VolMaster = vol;

        AudioListener.volume = VolMaster;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }


}
