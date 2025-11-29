using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float predkosc = 3.0f;
   
    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            float moveX = Input.GetAxis("Horizontal");


            Vector3 move = new Vector3(moveX, 0, 0);

            transform.Translate(move * predkosc * Time.deltaTime);
        }
    }
}
