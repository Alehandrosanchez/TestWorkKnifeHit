using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]GameObject Panel_Pause;
    [SerializeField] GameObject Panel_GameOver;
    [SerializeField] GameObject Panel_Game;

    Wood WD;
    void Start()
    {
        
        Time.timeScale = 1f;
        Panel_Pause.SetActive(false);
        Panel_GameOver.SetActive(false);
        Panel_Game.SetActive(true);
        Global.BestScore = PlayerPrefs.GetInt("Best");
        Global.Apples = PlayerPrefs.GetInt("Apples");
    }

   
    public void Times(float TimeScale)
    {
        Time.timeScale = TimeScale;
    }
    
    public void GO()
    {
        StartCoroutine(Game_Over(1f,null,false));
    }
    public void CG()
    {
        StartCoroutine(Continue_Game(0f));
    }
    public void PG()
    {
        StartCoroutine(Pause(0f));
    }
    public void RG()
    {
        StartCoroutine(Restart_Game(0f));
    }
    public IEnumerator Game_Over (float Time, GameObject Wood,bool DestroyWood)
    {
        if (DestroyWood)
        {
            Wood.GetComponent<Wood>().Crash();
        }
        if (Global.BestScore < Global.Score)
        {
            PlayerPrefs.SetInt("Best", Global.Score);
        }
        PlayerPrefs.SetInt("Apples", Global.Apples);
        yield return new WaitForSeconds(Time);
        Panel_Pause.SetActive(false);
        Panel_GameOver.SetActive(true);
        Panel_Game.SetActive(false);

    }
    public IEnumerator Continue_Game(float Time)
    {
        yield return new WaitForSeconds(Time);

        Times(1f);
        Panel_Pause.SetActive(false);
        Panel_GameOver.SetActive(false);
        Panel_Game.SetActive(true);
    }
    public IEnumerator Pause(float Time)
    {


        yield return new WaitForSeconds(Time);
        Times(0f);
        Panel_Pause.SetActive(true);
        Panel_GameOver.SetActive(false);
        Panel_Game.SetActive(false);
    }
    public IEnumerator Restart_Game(float Time)
    {
        Global.Score = 0;
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(1);
    }

}
