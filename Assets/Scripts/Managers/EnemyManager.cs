using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoints;
    public float spawnInterval = 10;

    RezTween.Timer spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawn Enemy");
        Transform spawnPoint = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount));
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawn()
    {
        spawnTimer = new RezTween.Timer(spawnInterval, gameObject);
        spawnTimer.onTick = SpawnEnemy;
        spawnTimer.Start();

        SpawnEnemy();
    }

    public void StopSpawnAndDestroyEnemies()
    {
        Debug.Log("Stop spawn enemies");
        spawnTimer.Stop(100);
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Ship")) Destroy(child);
        }
    }
}