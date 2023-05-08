using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class LogicManagerScript : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public int scoreIncrement = 1;
    public int scoreDelay =  3;
    public float sunMaxIntensity = 3f;
    public float boss1Time = 300;

    public GameObject gameOverScreen;
    public GameObject sun;
    public List<GameObject> lightRays;
    public GameObject levelProgressIndicator;

    private float sunIntensity = 0;
    private float timer = 0;

    public static bool isAlive = true;
    public static bool bossFight = false;
    public static bool boss1Complete = false;

    void Awake() {
        Debug.Log("LogicManagerScript OnEnable");
        timer = 0;
        isAlive = true;
        bossFight = false;
        boss1Complete = false;
        sunIntensity = 0;
        EventManager.OnEnemyDestroyed += addScore;
    }

    void Update() {
        if(!bossFight && isAlive) {
            timer += Time.deltaTime;
            updateSunIntensity();
            updateLevelProgress();

            if(!boss1Complete && timer >= boss1Time) {
                bossFight = true;
                EventManager.Boss1Start();
            }
        }
    }

    private void updateSunIntensity() {
        sunIntensity = Math.Min(sunMaxIntensity, timer / 100f);
        sun.GetComponent<Light2D>().intensity = sunIntensity;
        foreach(var light in lightRays) {
            light.GetComponent<Light2D>().intensity = sunIntensity/10;
        }
    }

    private void updateLevelProgress() {
        float endTime = boss1Time;
        float startY = -100;
        float endY = 400;
        float currentProgress = timer/endTime;
        float barRange = (endY - startY);
        float progressY = (barRange * currentProgress);
        float currentY = startY + progressY;
        
        if(currentProgress > .5f) {
            GameStateManager.Instance.gameState.showProgress = true;
        }
        
        if(GameStateManager.Instance.gameState.showProgress) {
            levelProgressIndicator.transform.parent.gameObject.SetActive(true);
        }
        
        var rectTransform = levelProgressIndicator.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentY);
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
