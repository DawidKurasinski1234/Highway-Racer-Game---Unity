using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public GameObject completeLevelUI;
    public static float GlobalSpeed = 10f;

    private void Start()
    {
        GlobalSpeed = 10f;
        isGameOver = false;
    }
    void Update()
    {
        if (!isGameOver)
        {
            GlobalSpeed += 0.5f * Time.deltaTime;
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
