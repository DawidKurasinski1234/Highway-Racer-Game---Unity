using UnityEngine;
// using Unity.Mathematics; <--- TO USUN¥£EM, BO POWODOWA£O B£¥D Z RANDOM

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    float licznik = 0f;

    void Update()
    {
        
        if (Time.time > licznik && GameManager.isGameOver == false)
        {
            SpawnObstacle();
            licznik = Time.time + spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        
        float[] lanes = { -6f, -2f, 2f, 6f };

        int laneIndex = Random.Range(0, lanes.Length);
        float randomX = lanes[laneIndex];

        
        Quaternion rotation = Quaternion.identity;
        float additionalSpeed = 0f;

        if (laneIndex <= 1)
        {
            additionalSpeed = 20f; // Szybko w nasz¹ stronê
            rotation = Quaternion.Euler(0, 180, 0); // Obrót o 180 stopni
        }
        else
        {
            additionalSpeed = -5f; // Uciekaj¹ przed nami
            rotation = Quaternion.identity; // Normalnie
        }

        Vector3 spawnPosition = new Vector3(randomX, 0, transform.position.z);
                
        GameObject NewCar = Instantiate(obstaclePrefab, spawnPosition, rotation);

        NewCar.GetComponent<ObstacleMovement>().customSpeed = additionalSpeed;
    }
}