using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamerController : MonoBehaviour
{
    public static GamerController instance;

   

    public int score;
    
    private void Awake()
    {
        
       
         if (instance == null)//checar se o "instanceplayer" é nulo
         {
            instance = this;
            DontDestroyOnLoad(gameObject);//mantém o objeto entre cenas
         }
         else if (instance != this)
         {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
         }
       // PlayerPrefs.DeleteAll();
    }


    private void Start()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("score") > 0)
        {
            score = PlayerPrefs.GetInt("score");
            Player.instancePlayer.scoreText.text = "x " + score.ToString();
        }
       


        GetCoin();
    }

    public void GetCoin()
    {
        score++;
        Player.instancePlayer.scoreText.text = "x " + score.ToString();

        PlayerPrefs.SetInt("score", score);
    }

   
    

    public void ShowGameOver()
    {
        Time.timeScale = 0;
       Player.instancePlayer.gameOver.SetActive(true);
        //Player.instancePlayer.OnHit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteAll();

    }

    
}
