using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]Text Apples,Best;
    private void Start()
    {
        Apples.text ="Apple: "+ PlayerPrefs.GetInt("Apples");
        Best.text = "BestScore: " + PlayerPrefs.GetInt("Best");
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
