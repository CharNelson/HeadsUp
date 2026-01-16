using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine;

public class Editor : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    TMP_InputField inputField_deckTitle;

    string pathStem;
    string originalTitle, newTitle;

    string deck;
    string fileData;

    void Awake()
    {
        //get deck
        deck = PlayerPrefs.GetString("deck", "ERROR: deck NOT SET AT EDITOR.AWAKE");
        newTitle = originalTitle = deck;
        //print("deck to edit: " + deck); //debug

        //get working directory
        pathStem = PlayerPrefs.GetString("deckDirectory",
                   "ERROR: deckDirectory NOT SET AT EDITOR.AWAKE") + "/";

        //read corresponding file
        //print("reading from " + pathStem + ensureExtension(deck)); //debug
        fileData = File.ReadAllText(pathStem + ensureExtension(deck));

        //set input field to display fileData
        inputField.text = fileData;
        //set title input field to display deck's title
        inputField_deckTitle.text = deck;
    }

    public void updateTitle() { 
        newTitle = inputField_deckTitle.text;
    }

    public void updateFileData() {
        fileData = inputField.text;
    }

    private string ensureExtension(string fileName) {
        string output = fileName;

        if (output.Length < 4) {
            //input somehow empty; dont try to substring it
            //print("FILENAME HAS LENGTH < 4"); //debug
            return output + ".csv";
        }

        //if not empty, then you can substring probably
        if (!fileName.Substring(fileName.Length-4).Equals(".csv")) {
            output+=".csv";
        }

        return output;
    }

    // Update is called once per frame
    public void SaveAndExitBackToMenuObviously(bool save) {

        if (save) {
            if (!(ensureExtension(originalTitle)).Equals(ensureExtension(newTitle))) {
                //if they entered new title, "overwrite" previous file with new title
                //print("deleting old file: " + pathStem + originalTitle); //debug
                File.Delete(pathStem + ensureExtension(originalTitle));
            }
            //write corresponding file
            string writePath = pathStem + ensureExtension(newTitle);
            //print("writePath: " + writePath); //debug
            //print("write data: " + fileData); //debug
            File.WriteAllText(writePath, fileData);
        }
        SceneManager.LoadScene("Menu");
    }

}
