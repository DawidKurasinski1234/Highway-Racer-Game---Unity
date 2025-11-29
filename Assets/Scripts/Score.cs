using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public TMP_Text screenText;
    public TMP_Text HighScoreText;// Przypisujesz to w Inspektorze!
    bool isGameActive = true;
    float distance = 0f;

    private void Start()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);

        if (HighScoreText != null)
        {
        HighScoreText.text = "High Score: " + highScore.ToString("0");
        }
    }
    void Update()
    {
        if (isGameActive == true)
        {
            distance = Time.timeSinceLevelLoad * 10f;

            // Dodajemy proste zabezpieczenie, gdybyœ zapomnia³ przypisaæ w Inspektorze
            if (screenText != null)
            {
                screenText.text = distance.ToString("Score: 0");
            }
        }
    }

    public void StopCount()
    {
        isGameActive = false;
        float OldRecord = PlayerPrefs.GetFloat("HighScore", 0);

        if (distance > OldRecord)
        {
            PlayerPrefs.SetFloat("HighScore", distance);
            if (HighScoreText != null)
            {
                HighScoreText.text = "New High Score: " + distance.ToString("0");
                Debug.Log("Zapisano nowy rekord)");
            }
        }
    }
}