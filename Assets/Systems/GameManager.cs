using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGameOver()
    {

    }



    
}
