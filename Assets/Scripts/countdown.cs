using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class countdown : MonoBehaviour
{

    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countdownCoroutine());
    }

    IEnumerator countdownCoroutine() 
    {
        int i = 7;
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.5f);

        countdownText.text = "" + i--;
        yield return new WaitForSeconds(0.01f);

        SceneManager.LoadScene("Play");        
    }
}
