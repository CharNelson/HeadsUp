using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public Button[] deckButtons = new Button[36]; //keep in mind, inspector value (currently also 36) will override default size of 36
    [SerializeField]
    public Button[] editButtons = new Button[36]; //keep in mind, inspector value (currently also 36) will override default size of 36

    [SerializeField]
    Slider timerSlider;
    [SerializeField]
    Text timer;

    public string deckDirectory;
    private List<string> decks;

    void Awake()
    {
        //reset score; score is per play per deck
        PlayerPrefs.SetInt("score", 0);
        decks = new List<string>();
        deckDirectory = PlayerPrefs.GetString("deckDirectory", "");
        //print("deckDirectory end of Awake: " + deckDirectory); //debug
    }

    // Start is called before the first frame update
    void Start()
    {
        updateTimer();
        decks = getDeckTitles();
        assignEditButtonTasks(editButtons, decks);
        assignButtonTasks(deckButtons, decks);
    }

    public void updateTimer()
    {
        timer.text = "" + secsToMinSec((int)timerSlider.value);
        PlayerPrefs.SetInt("timer", (int)timerSlider.value);
    }

    private string secsToMinSec(int seconds)
    {
        int mins = seconds / 60;
        string output = "" + mins;
        output += ":";
        int secs = seconds - (mins*60);
        if (secs < 10)
            output += "0";
        output += "" + secs;
        return output;
    }

    string[] getFiles(string directory) {
        return Directory.GetFiles(directory);
    }



    private List<string> getDeckTitles()
    {

        //I'm trying out switching to only storing files locally within the app's persistent storage
        //cuz file access crap on Android has not been working
        //hence:
        deckDirectory = "";
        deckDirectory = Application.dataPath;
        //print("DATAPATH: " + deckDirectory); //debug
        //DATAPATH: /data/data/com.DefaultCompany.com.unity.template.mobile2D/pram-shadow-files
        deckDirectory = Application.persistentDataPath;
        //print("PERSISTENT: " + deckDirectory); //debug
        //PERSISTENT: /storage/emulated/0/Android/data/com.DefaultCompany.com.unity.template.mobile2D/files

        //set global var accordingly
        PlayerPrefs.SetString("deckDirectory", deckDirectory);

        //access directory given by path deckDirectory,
        //get all file names within that directory,
        List<string> outputList = new List<string>(Directory.GetFiles(deckDirectory));


        //for some odd reason GetFiles is returning a list
        //that starts with an empty-titled filename as its first element?
        //even tho ".csv" is not a file in this directory according to adb???
        //idk i hope this fix aint destructive
        outputList.Remove(deckDirectory+"/.csv");

        //print("filepaths of files in deck directory: "); //debug
        //print list; debug
        for (int i = 0; i < outputList.Count; i++) {
            //print("\t" + outputList[i] + "\t"); //debug
        }

        //to get filenames & working directory separately,
        //we must remove the directory from the start of every filename
        int directoryLength = deckDirectory.Length;

        //prune all non-csv (for defensiveness) & long, pull-path names
        for (int i = 0; i < outputList.Count; i++) {
            string deckTitle = outputList[i];
            if (!deckTitle.Substring(deckTitle.Length-4).Equals(".csv")) {
                outputList.Remove(deckTitle); i--;
                //print("removed " + deckTitle + " from list; not .csv"); //debug
            } else {
                //trim beginning by directoryLength number of characters,
                outputList[i] = outputList[i].Substring(directoryLength+1); //+1 for extra "\"
                //and trim end by 4 to remove the ".csv" file extension
                outputList[i] = outputList[i].Substring(0,deckTitle.Length-directoryLength-4-1); //see above
            }
        }

        //print("filenames in deck directory after non-csv pruning: "); //debug
        //print list; debug
        for (int i = 0; i < outputList.Count; i++) {
            //print("\t" + outputList[i] + "\t"); //debug
        }

        //& return !
        return outputList;
    }

    private void assignButtonTasks(Button[] buttons, List<string> decks) {

        //incrementation variables
        Button currentButton;
        string deckTitle;

        //set default text of buttons to just be empty
        foreach (Button button in buttons) {
            button.GetComponentInChildren<Text>().text = "";
        }

        //only assign as many tasks as we need assigned, to as many buttons as we have available
        int maxButtons = decks.Count < buttons.Length ? decks.Count : buttons.Length;

        //loop thru buttons
        for (int i = 0; i < maxButtons; i++) {
            //get each button & deck title
            currentButton = buttons[i];
            deckTitle = decks[i];

            //print("deckTitle incrementation variable: " + deckTitle + "\n"); //debug

            //set each button's display text to match its task
            currentButton.GetComponentInChildren<Text>().text = deckTitle;
            
            
            //set each button's task to be activating the selectDeck() script with corresponding parameter deckTitle
            //currentButton.onClick.AddListener(delegate {selectDeck(deckTitle); });
            //^ THIS dont work for no good reason (that i am aware of)
            //so instead we gotsta do this:
            switch(i) {
                case 0:
                    currentButton.onClick.AddListener( () => selectDeck(0) );
                    break;
                case 1:
                    currentButton.onClick.AddListener( () => selectDeck(1) );
                    break;
                case 2:
                    currentButton.onClick.AddListener( () => selectDeck(2) );
                    break;
                case 3:
                    currentButton.onClick.AddListener( () => selectDeck(3) );
                    break;
                case 4:
                    currentButton.onClick.AddListener( () => selectDeck(4) );
                    break;
                case 5:
                    currentButton.onClick.AddListener( () => selectDeck(5) );
                    break;
                case 6:
                    currentButton.onClick.AddListener( () => selectDeck(6) );
                    break;
                case 7:
                    currentButton.onClick.AddListener( () => selectDeck(7) );
                    break;
                case 8:
                    currentButton.onClick.AddListener( () => selectDeck(8) );
                    break;
                case 9:
                    currentButton.onClick.AddListener( () => selectDeck(9) );
                    break;
                case 10:
                    currentButton.onClick.AddListener( () => selectDeck(10) );
                    break;
                case 11:
                    currentButton.onClick.AddListener( () => selectDeck(11) );
                    break;
                case 12:
                    currentButton.onClick.AddListener( () => selectDeck(12) );
                    break;
                case 13:
                    currentButton.onClick.AddListener( () => selectDeck(13) );
                    break;
                case 14:
                    currentButton.onClick.AddListener( () => selectDeck(14) );
                    break;
                case 15:
                    currentButton.onClick.AddListener( () => selectDeck(15) );
                    break;
                case 16:
                    currentButton.onClick.AddListener( () => selectDeck(16) );
                    break;
                case 17:
                    currentButton.onClick.AddListener( () => selectDeck(17) );
                    break;
                case 18:
                    currentButton.onClick.AddListener( () => selectDeck(18) );
                    break;
                case 19:
                    currentButton.onClick.AddListener( () => selectDeck(19) );
                    break;
                case 20:
                    currentButton.onClick.AddListener( () => selectDeck(20) );
                    break;
                case 21:
                    currentButton.onClick.AddListener( () => selectDeck(21) );
                    break;
                case 22:
                    currentButton.onClick.AddListener( () => selectDeck(22) );
                    break;
                case 23:
                    currentButton.onClick.AddListener( () => selectDeck(23) );
                    break;
                case 24:
                    currentButton.onClick.AddListener( () => selectDeck(24) );
                    break;
                case 25:
                    currentButton.onClick.AddListener( () => selectDeck(25) );
                    break;
                case 26:
                    currentButton.onClick.AddListener( () => selectDeck(26) );
                    break;
                case 27:
                    currentButton.onClick.AddListener( () => selectDeck(27) );
                    break;
                case 28:
                    currentButton.onClick.AddListener( () => selectDeck(28) );
                    break;
                case 29:
                    currentButton.onClick.AddListener( () => selectDeck(29) );
                    break;
                case 30:
                    currentButton.onClick.AddListener( () => selectDeck(30) );
                    break;
                case 31:
                    currentButton.onClick.AddListener( () => selectDeck(31) );
                    break;
                case 32:
                    currentButton.onClick.AddListener( () => selectDeck(32) );
                    break;
                case 33:
                    currentButton.onClick.AddListener( () => selectDeck(33) );
                    break;
                case 34:
                    currentButton.onClick.AddListener( () => selectDeck(34) );
                    break;
                case 35:
                    currentButton.onClick.AddListener( () => selectDeck(35) );
                    break;
                case 36:
                    currentButton.onClick.AddListener( () => selectDeck(36) );
                    break;

            }
        }

    }

    private void assignEditButtonTasks(Button[] buttons, List<string> decks) {

        //set default display of buttons
        foreach (Button button in buttons) {            
            button.GetComponentInChildren<Text>().text = "";
        }

        //incrementation variables
        Button currentButton;
        string deckTitle;

        //only assign as many tasks as we need assigned, to as many buttons as we have available
        int maxButtons = decks.Count < buttons.Length ? decks.Count : buttons.Length;
        //print("maxButtons: " + maxButtons);


        //loop thru buttons
        for (int i = 0; i < maxButtons; i++) {
            //get each button & deck title
            currentButton = buttons[i];
            deckTitle = decks[i];

            //print("index: " + i); //debug
            //print("current edit button: " + currentButton); //debug
            //print("corresponding play button: " + deckButtons[i]); //debug            
            //print("deckTitle: " + deckTitle); //debug

            //print("deckTitle incrementation variable: " + deckTitle + "\n"); //debug

            //set button text to simply be "[edit]" (when that slot is not empty)
            currentButton.GetComponentInChildren<Text>().text = "[edit]";
            //if (i%2 == 0) {currentButton.GetComponentInChildren<Text>().text = "[TEST]";} //debug
            
            //set each Edit button's task to be activating the editDeck() script,
            //attaching its own button index for reference to the deck button to which it corresponds
            //DYNAMIC TASK DELEGATION WAS *NOT* WORKING;
            //I'D TELL IT TO SET EACH INDIVIDUAL BUTTON'S TASK PARAMS TO "i"
            //SO ITD BE DIFFERENT FOR EACH BUTTON & CORRESPOND TO THE SAME SLOT IN THE OTHER ARRAY OF BUTTONS
            //INSTEAD, THE TASK (WHEN RUN FROM ANY BUTTON) WAS ALWAYS COMPLETED WITH PARAM 33,
            //THE LAST VALUE OF i
            //SO INSTEAD, I HAVE CONSTRUCTED *THIS* ABSURD THING
            //GAZE, AND BE DISHEARTENED

            switch(i) {
                case 0:
                    currentButton.onClick.AddListener( () => editDeck(0) );
                    break;
                case 1:
                    currentButton.onClick.AddListener( () => editDeck(1) );
                    break;
                case 2:
                    currentButton.onClick.AddListener( () => editDeck(2) );
                    break;
                case 3:
                    currentButton.onClick.AddListener( () => editDeck(3) );
                    break;
                case 4:
                    currentButton.onClick.AddListener( () => editDeck(4) );
                    break;
                case 5:
                    currentButton.onClick.AddListener( () => editDeck(5) );
                    break;
                case 6:
                    currentButton.onClick.AddListener( () => editDeck(6) );
                    break;
                case 7:
                    currentButton.onClick.AddListener( () => editDeck(7) );
                    break;
                case 8:
                    currentButton.onClick.AddListener( () => editDeck(8) );
                    break;
                case 9:
                    currentButton.onClick.AddListener( () => editDeck(9) );
                    break;
                case 10:
                    currentButton.onClick.AddListener( () => editDeck(10) );
                    break;
                case 11:
                    currentButton.onClick.AddListener( () => editDeck(11) );
                    break;
                case 12:
                    currentButton.onClick.AddListener( () => editDeck(12) );
                    break;
                case 13:
                    currentButton.onClick.AddListener( () => editDeck(13) );
                    break;
                case 14:
                    currentButton.onClick.AddListener( () => editDeck(14) );
                    break;
                case 15:
                    currentButton.onClick.AddListener( () => editDeck(15) );
                    break;
                case 16:
                    currentButton.onClick.AddListener( () => editDeck(16) );
                    break;
                case 17:
                    currentButton.onClick.AddListener( () => editDeck(17) );
                    break;
                case 18:
                    currentButton.onClick.AddListener( () => editDeck(18) );
                    break;
                case 19:
                    currentButton.onClick.AddListener( () => editDeck(19) );
                    break;
                case 20:
                    currentButton.onClick.AddListener( () => editDeck(20) );
                    break;
                case 21:
                    currentButton.onClick.AddListener( () => editDeck(21) );
                    break;
                case 22:
                    currentButton.onClick.AddListener( () => editDeck(22) );
                    break;
                case 23:
                    currentButton.onClick.AddListener( () => editDeck(23) );
                    break;
                case 24:
                    currentButton.onClick.AddListener( () => editDeck(24) );
                    break;
                case 25:
                    currentButton.onClick.AddListener( () => editDeck(25) );
                    break;
                case 26:
                    currentButton.onClick.AddListener( () => editDeck(26) );
                    break;
                case 27:
                    currentButton.onClick.AddListener( () => editDeck(27) );
                    break;
                case 28:
                    currentButton.onClick.AddListener( () => editDeck(28) );
                    break;
                case 29:
                    currentButton.onClick.AddListener( () => editDeck(29) );
                    break;
                case 30:
                    currentButton.onClick.AddListener( () => editDeck(30) );
                    break;
                case 31:
                    currentButton.onClick.AddListener( () => editDeck(31) );
                    break;
                case 32:
                    currentButton.onClick.AddListener( () => editDeck(32) );
                    break;
                case 33:
                    currentButton.onClick.AddListener( () => editDeck(33) );
                    break;
                case 34:
                    currentButton.onClick.AddListener( () => editDeck(34) );
                    break;
                case 35:
                    currentButton.onClick.AddListener( () => editDeck(35) );
                    break;
                case 36:
                    currentButton.onClick.AddListener( () => editDeck(36) );
                    break;

            }
            //AND IT WORKS.
            //AUGGH!

            //print("event for button #" + i + ": " + currentButton.onClick); //debug
        }

        //make the rest of the buttons (for which there is not yet a file) [new] buttons
        int maxCreateFileButtons = buttons.Length - maxButtons; int index=0;
        print("buttons used: " + maxButtons + "; extra buttons available: " + maxCreateFileButtons); //debug
        for (int i = 0; i < maxCreateFileButtons; i++) {
            //dont forget to offset as to not overwrite what we've already done in the previous loop
            index = maxButtons + i;

            //get button object
            currentButton = buttons[index];
            print("extra button " + currentButton + " will become New button"); //debug
            //no corresponding deck title to get, because this is for new files

            //tell this button to activate newDeck script
            currentButton.onClick.AddListener( () => newDeck() );
            //update display accordingly
            currentButton.GetComponentInChildren<Text>().text = "[new]";
        }

    }
    

    void selectDeck(int index) {
        string text = deckButtons[index].GetComponentInChildren<Text>().text;
        //print("" + text + "'s PLAY button pressed"); //debug
        PlayerPrefs.SetString("deck", text);
        setActiveScene("countdown");
    }

/*
    void editDeck(string deckTitle) {
        print("" + deckTitle + "'s EDIT button pressed"); //debug
        PlayerPrefs.SetString("deck", deckTitle);
        print("playerPref DECK set to "+PlayerPrefs.GetString("deck"));
        setActiveScene("Editor");
    }
*/

    void editDeck(int index) {
        string text = deckButtons[index].GetComponentInChildren<Text>().text;
        //print("corresponding button object: " + deckButtons[index]); //debug
        //print("corresponding text component: " + deckButtons[index].GetComponentInChildren<Text>()); //debug
        //print("" + text + "'s EDIT button pressed (#" + index + ")"); //debug
        PlayerPrefs.SetString("deck", text);
        setActiveScene("Editor");
    }

    void newDeck() {
        PlayerPrefs.SetString("deck", "<placeholder-title>");
        setActiveScene("Editor");
    }


    //ok this was meant to be "defensive" or "robust" or whatever but now it just seems to be dumb
    public static void setActiveScene(string scene) {
        switch (scene) {
            case "Pre-Menu":
                SceneManager.LoadScene("Pre-Menu");
                break;

            case "Menu":
                SceneManager.LoadScene("Menu");
                break;

            case "Editor":
                SceneManager.LoadScene("Editor");
                break;
            
            case "countdown":
                SceneManager.LoadScene("countdown");
                break;

            case "Play":
                SceneManager.LoadScene("Play");
                break;

            case "Results":
                SceneManager.LoadScene("Results");
                break;
        }
    }

}
