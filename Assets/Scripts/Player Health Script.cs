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
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }    
    }
    void TakeDamage(int heat)
    {
        
        currentHeat += heat;
        healthBar.SetCurrentHeat(currentHeat);
    }
}
