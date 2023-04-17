using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public TMP_Text scoreText;

    private void EventManagerOnScoreUpdated(int score)
    {
        scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
    }
}
