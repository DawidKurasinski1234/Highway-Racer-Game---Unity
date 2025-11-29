using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    //public float predkosc = 10f; // Szybkoœæ zbli¿ania siê

    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            transform.Translate(Vector3.forward * GameManager.GlobalSpeed * Time.deltaTime);

            if (transform.position.z < -50f)
            {

                Destroy(gameObject);
            }
        }
    }
}