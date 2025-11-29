using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
    SceneManager.LoadScene("SampleScene");
    }
    public void CustomizeCar()
    {
    SceneManager.LoadScene("CustomizeCar");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
