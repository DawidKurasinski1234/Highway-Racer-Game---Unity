using NUnit.Framework;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
   // public float speed = 10f;
    public float tileLength = 50f;
    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            transform.Translate(Vector3.back * GameManager.GlobalSpeed * Time.deltaTime, Space.World);

            if (transform.position.z < -tileLength)
            {

                transform.position += new Vector3(0, 0, tileLength * 2);
            }
        }   
    }
}