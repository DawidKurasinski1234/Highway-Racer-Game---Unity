using UnityEngine;
// using Unity.Mathematics; <--- TO USUN¥£EM, BO POWODOWA£O B£¥D Z RANDOM

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject [] obstaclePrefabs;
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
        float[] lanes = { -6.75f, -2.5f, 2.5f, 6.75f };
        int laneIndex = Random.Range(0, lanes.Length);
        float randomX = lanes[laneIndex];

        // Wybieramy model
        int losowyModelIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject wybranyPrefab = obstaclePrefabs[losowyModelIndex];

        Quaternion finalnaRotacja;
        float additionalSpeed = 0f;

        // Logika pasów
        if (laneIndex <= 1) // Lewa strona (Auta jad¹ NA NAS)
        {
            additionalSpeed = 30f;
            // Obrót 180 stopni = widzimy maskê samochodu
            finalnaRotacja = Quaternion.Euler(0, 180, 0);
        }
        else // Prawa strona (Auta jad¹ Z NAMI)
        {
            additionalSpeed = 8f;
            // Obrót 0 stopni = widzimy baga¿nik samochodu
            finalnaRotacja = Quaternion.Euler(0, 0, 0);
        }

        Vector3 spawnPosition = new Vector3(randomX, 0, transform.position.z);

        int randomModelIndex = Random.Range(0, obstaclePrefabs.Length);

        GameObject chosenPrefab = obstaclePrefabs[randomModelIndex];

        GameObject NewCar = Instantiate(chosenPrefab, spawnPosition, finalnaRotacja);

        NewCar.GetComponent<ObstacleMovement>().customSpeed = additionalSpeed;
    }
}