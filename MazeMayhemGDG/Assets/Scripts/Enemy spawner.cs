using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int spawnAmount;
    [SerializeField] int spawnRate;

    int spawnedCount;
    float spawnTimer;

    bool startSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnedCount < spawnAmount && spawnTimer >= spawnRate)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        spawnTimer = 0;
        spawnedCount++;
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        GameManager.instance.updateGameGoal(1);
    }
}
