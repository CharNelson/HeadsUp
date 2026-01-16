using System.Collections;
using SimpleFileBrowser;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using UnityEngine;

public class DirectoryPrompt : MonoBehaviour
{

    /*

    void Awake() 
    {
        permRequests();

        getDeckDirectory();
    }

    public void permRequests()
    {
        RequestPermission(Permission.ExternalStorageWrite);
        RequestPermission(Permission.ExternalStorageRead);
        RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        RequestPermission("android.permission.MANAGE_DOCUMENTS");
        RequestPermission("android.permission.READ_BLOCKED_NUMBERS");  
    }

    async void RequestPermission(string permission)
    {
	    AndroidRuntimePermissions.Permission result =
        await AndroidRuntimePermissions.RequestPermissionAsync( permission );
    }

    public void setDeckDirectory()
    {
        StartCoroutine(filePromptCoroutine());
    } 

    IEnumerator filePromptCoroutine()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)) {
            //hooray!!
        } else {
        }

        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, false, null, null, "SELECT DECK FOLDER", "Load" );
    
        string promptInput = FileBrowser.Result[0];
        //then save this to PlayerPrefs
        PlayerPrefs.SetString("deckDirectory", promptInput);
        print("deckDirectory set to: " + promptInput); //debug
    }

    private void getDeckDirectory()
    {
        //check if already saved in PlayerPrefs,
        //if so just use that value but if not prompt & get path for consequent times
        
        if (PlayerPrefs.HasKey("deckDirectory") && PlayerPrefs.GetString("deckDirectory", "") != ""){
            //hooray! we already have it stored in playerprefs
        } else { //welp thats alright aswell
            //prompt user to select directory
            StartCoroutine(filePromptCoroutine());
        }
    }

    public void proceedToMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

*/

}
