using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    [Header("Heat amounts")]
    public string playSceneName="Reactor Chamber";
    public string menuSceneName = "MainMenu";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(playSceneName);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
