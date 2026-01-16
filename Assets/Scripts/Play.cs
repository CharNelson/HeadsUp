using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public class Play : MonoBehaviour
{

    [SerializeField]
    Text currentCard;

    private List<string> cards;
    private string deckDirectory;
    private string deck;

    private string perCardResults;
    private string perCardResults_symbol;

    public int score;

    private string resultSymbol;
    private string correctSymbol;
    private string skipSymbol;



    void Awake() 
    {
        cards = new List<string>();
        perCardResults = "";
        score = 0;
        //correctSymbol = "✅";
        //skipSymbol = "❌";
        correctSymbol = "[CORRECT]";
        skipSymbol = "[SKIP]";

        PlayerPrefs.SetString("perCardResults", ""); //reset results to default
        
        deckDirectory = PlayerPrefs.GetString("deckDirectory", "");
        deck = PlayerPrefs.GetString("deck", "");
        getCards(deckDirectory + "/" + deck + ".csv");
    }

    void Start() 
    {
        //start timer
        StartCoroutine(timerCoroutine());
    }

    public void nextCard(bool success)
    {
        //increment score if moving to next card cuz they got this card;
        //if they just Skipped, then dont give em anything other than new card
        score = success ? score+1 : score;

        //save data so as to display it later in the Results scene
        updateResults(success);

        //remove now-old card
        cards.Remove(currentCard.text);
        //select next card & display it
        currentCard.text = selectNextCard();
    }

    void updateResults(bool success)
    {
        //get current card's data
        string cardText = currentCard.text;

        //append card to perCardResults for seeing at the results screen later
        perCardResults += "" + cardText;
        //prepare appropriate symbol for correct vs skip
        resultSymbol = success ? correctSymbol : skipSymbol;
        //append symbol for success or failure,
        //then line break to prepare for next card result
        perCardResults_symbol += "\n" + resultSymbol;

        //print("new result added: " + currentCard.text + " \t" + resultSymbol); //debug

        //update player prefs so the Results script in the Results scene
        //will actually be able to see this stuff
        PlayerPrefs.SetString("perCardResults", perCardResults);
        PlayerPrefs.SetString("perCardResults_symbol", perCardResults_symbol);
        PlayerPrefs.SetInt("score", score);
    }

    string selectNextCard() 
    {
        //make sure we don't run out of cards to avoid out of bounds exception
        if (cards.Count == 0) {
            print("RAN OUT OF CARDS"); //debug
            getCards(deckDirectory + "/" + deck + ".csv");
        }
        string nextCard = "" + cards[Random.Range(0, cards.Count)];
        print("nextCard: " + nextCard); //debug
        return nextCard;
    }


    void getCards(string deckPath)
    {
        //parse csv file into list "cards" !
        // https://youtu.be/lJ9nArexsfA

        //print("read path: " + deckPath); //debug
        string fileData = File.ReadAllText(deckPath);
        //print("fileData: " + fileData); //debug
        splitStringAsCSV(fileData, cards);

        //bodgey fix to first element in CSV not starting with new line
        //cards[0] = "\n" + cards[0];

        //debug
        //print("cards acquired: "); //debug
        foreach (string card in cards) { //debug
            //print("\t" + card); //debug
        }

        //and make sure to set random one to first currentCard value
        currentCard.text = "" + cards[Random.Range(0, cards.Count)];
    }

    void splitStringAsCSV(string input, List<string> output) 
    {
        string cardSeparator = ",";

        //print("splitting string " + input + " by separator " + cardSeparator); //debug

        //find first delimiter/separator
        int index = input.IndexOf(cardSeparator);

        //print("index of first separator: " + index); //debug

        //print("full data: " + input); //debug

        //loop thru & snag all elements that are followed by a delimiter
        while (index > 0) {

            //print("index of first separator: " + index); //debug

            //add next element
            output.Add(input.Substring(0,index));

            //print("new element: " + input.Substring(0,index)); //debug

            //remove that element from input
            input = input.Substring(index+1); //+1 to get past comma
            //get index of next separator
            index = input.IndexOf(cardSeparator);

            //make sure no empty cards just in case
            output.Remove("");
        }

        //snag that last element that is not followed by a delimiter
        output.Add(input);
    }

    IEnumerator timerCoroutine() {
        int timer = PlayerPrefs.GetInt("timer", 60); //default to a minute
        yield return new WaitForSeconds(timer);
        endPlay();
    }


    public void endPlay() {
        //save score for the last time this round
        PlayerPrefs.SetInt("score", score);
        //move from Play scene to Results scene
        SceneManager.LoadScene("Results");
    }

}
