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

    public void StopSpawn()
    {
        spawnTimer.Stop();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Ship")) Destroy(child);
        }
    }
}