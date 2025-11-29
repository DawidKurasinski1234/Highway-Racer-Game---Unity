using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    float licznik = 0f;

    void Update()
    {
        if (Time.time > licznik)
        {
            if (Time.time > licznik && GameManager.isGameOver == false)
            {
                GameManager.GlobalSpeed += 2f;
                licznik += 10f;
                SpawnObstacle();
                licznik = Time.time + spawnInterval;
            }
            //SpawnObstacle();
            //licznik = Time.time + spawnInterval;

        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(-10f, 10f);
        Debug.Log("Wylosowano: " + randomX);
        Vector3 spawnPosition = new Vector3(randomX, 0, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
    }
}
