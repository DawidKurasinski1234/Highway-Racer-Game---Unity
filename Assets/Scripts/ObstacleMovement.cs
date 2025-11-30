using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    //public float predkosc = 10f; // Szybkoœæ zbli¿ania siê
    public float customSpeed = 0f;

    void Update()
    {
        if (GameManager.isGameOver == false)
        {

            float actualSpeed = GameManager.GlobalSpeed + customSpeed;

            transform.Translate(Vector3.back * actualSpeed * Time.deltaTime, Space.World);

            if (transform.position.z < -50f)
            {

                Destroy(gameObject);
            }
        }
    }
}