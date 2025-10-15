using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Health amounts")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }    
    }
    void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        Debug.Log(currentHealth);
        healthBar.SetCurrentHealth(currentHealth);
    }
}
