using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Heat amounts")]
    public int maxHeat= 100;
    public int currentHeat;

    public OverheatBarScript healthBar;
    void Start()
    {
        currentHeat = 0;
        healthBar.SetMaxHeat(maxHeat);
        healthBar.SetCurrentHeat(currentHeat);
        healthBar = FindAnyObjectByType<OverheatBarScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }    
    }
    public void TakeDamage(float heat)
    {
        
        //currentHeat += heat;
        healthBar.SetCurrentHeat(heat);
    }
}
