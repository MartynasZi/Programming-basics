using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Power-Up Setup")]
    public GameObject[] PowerUps;
    public Transform[] SpawnLocations;

    [Header("Spawn Timing")]
    public Vector2 SpawnRatioSeconds = new Vector2(5f, 10f);
    public int DestroyAfterSeconds = 10;

    void Start()
    {
        // Automatically fill spawn locations by tag
        GameObject[] tagged = GameObject.FindGameObjectsWithTag("PowerupLocation");
        SpawnLocations = new Transform[tagged.Length];
        for (int i = 0; i < tagged.Length; i++)
            SpawnLocations[i] = tagged[i].transform;

        // Start the spawn loop
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(SpawnRatioSeconds.x, SpawnRatioSeconds.y));
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        if (PowerUps.Length == 0 || SpawnLocations.Length == 0)
            return;

        GameObject prefab = PowerUps[Random.Range(0, PowerUps.Length)];
        Transform spawnPoint = SpawnLocations[Random.Range(0, SpawnLocations.Length)];

        GameObject spawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawned, DestroyAfterSeconds);
    }
}
