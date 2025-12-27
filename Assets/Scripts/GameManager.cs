using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public GameObject completeLevelUI;
    public static float GlobalSpeed = 10f;
    public static float maxSpeed = 50f;

    private void Start()
    {
        maxSpeed = 50f;
        GlobalSpeed = 10f;
        isGameOver = false;
    }
    void Update()
    {
        if (!isGameOver)
        {
            maxSpeed += 0.5f * Time.deltaTime;
            GlobalSpeed = Mathf.Lerp(GlobalSpeed, Input.GetAxis("Vertical") * maxSpeed, 0.5f* Time.deltaTime);
            GlobalSpeed = Mathf.Clamp(GlobalSpeed, 0, maxSpeed);

        }
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        Debug.Log("Level Complete!");
    }
    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("Game Over!");

            Invoke ("Restart", 5f);
        }
        
    }
    public void Restart()
    {
              SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
