using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Results : MonoBehaviour
{
    public int score;
    public string perCardResults, perCardResults_symbol;
    public Text score_Text, perCardResults_Text, perCardResults_symbol_Text;

    void Awake()
    {
        score = PlayerPrefs.GetInt("score", -666);
        perCardResults = PlayerPrefs.GetString("perCardResults", "<nullResults>");
        perCardResults_symbol = PlayerPrefs.GetString("perCardResults_symbol", "<nullSymbol>");
    }

    void Start() 
    {
        //vibrate to indicate we've entered Results and left Play
        Handheld.Vibrate();

        score_Text.text = "" + score;
        perCardResults_Text.text = "" + perCardResults;
        perCardResults_symbol_Text.text = "" + perCardResults_symbol;
    }

    public void ExitResults(bool AGAIN) 
    {
        if (AGAIN)
            SceneManager.LoadScene("countdown");
        else
            SceneManager.LoadScene("Menu");
    }
}
