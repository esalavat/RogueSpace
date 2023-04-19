using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManagerScript : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public int scoreIncrement = 1;
    public int scoreDelay =  3;
    public GameObject gameOverScreen;
    public GameVars gameState;

    void OnEnable() {
        EventManager.OnEnemyDestroyed += addScore;
    }

    [ContextMenu("addScore")]
    public void addScore() {
        addScore(scoreIncrement);
    }

    public void addScore(int amount) {
        score += amount;
        EventManager.ScoreUpdated(score);
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver() {
        if(!gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(true);
        }
    }
}
