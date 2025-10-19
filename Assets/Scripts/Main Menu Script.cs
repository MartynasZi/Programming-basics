using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    [Header("Heat amounts")]
    public string playSceneName="Reactor Chamber";
    public void PlayGame ()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
