using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ReactorBehaviour Reactor;
    [SerializeField] PowerUpManager PowerUpManager;
    [SerializeField] PlayerController PlayerController;
    [SerializeField] GameObject Tutorial;
    [SerializeField] private KeyCode StartKey = KeyCode.Space;
    private bool _GameStarted = false;


    public float TotalScore;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);        
        
    }

    private void Update()
    {
        if(!_GameStarted && Input.GetKey(StartKey))
        {
            StartGame();
        }


    }

    public void OnGameOver()
    {

    }

    private void StartGame()
    {
            Reactor.enabled = true;
            PowerUpManager.enabled = true;
            PlayerController.enabled = true;
        Tutorial.SetActive(false);
    }
    
}
