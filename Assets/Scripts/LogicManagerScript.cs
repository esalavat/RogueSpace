using System.Collections;
using System.Collections.Generic;
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
    

    void Start() {
        InvokeRepeating("addScore", scoreDelay, scoreDelay);
    }


    [ContextMenu("addScore")]
    public void addScore() {
        gameState.score += scoreIncrement;
        scoreText.text = gameState.score.ToString();
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        CancelInvoke("addScore");
    }
}
