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
    public GameObject levelProgress;
    public GameObject nebula;

    private float sunIntensity = 0;
    private float timer = 0;
    private bool debug = false;
    private GameObject levelProgressIndicator;
    private GameObject levelProgressBackground;

    public static bool isAlive = true;
    public static bool bossFight = false;
    public static bool boss1Complete = false;
    public static bool nebulaOn = false;

    void Start() {
        debug = GameStateManager.Instance.gameState.debug;
        boss1Time /= (debug ? 15 : 1);
        levelProgressIndicator = levelProgress.transform.Find("ProgressIndicator").gameObject;
        levelProgressBackground = levelProgress.transform.Find("ProgressBackground").gameObject;
        updateProgressBarBackground();
    }

    void OnEnable() {
        EventManager.OnEnemyDestroyed += addScore;
        EventManager.OnBoss1End += boss1End;
        timer = 0;
        isAlive = true;
        bossFight = false;
        boss1Complete = false;
        nebulaOn = false;
        sunIntensity = 0;
    }

    void OnDisable() {
        EventManager.OnEnemyDestroyed -= addScore;
        EventManager.OnBoss1End -= boss1End;
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

            if(!nebulaOn && timer > boss1Time + 1) {
                nebulaOn = true;
                nebula.SetActive(true);
            }
        }
    }

    private void updateSunIntensity() {
        sunIntensity = Math.Min(sunMaxIntensity, (timer * (debug ? 15 : 1)) / 100f);
        sun.GetComponent<Light2D>().intensity = sunIntensity;
        foreach(var light in lightRays) {
            light.GetComponent<Light2D>().intensity = sunIntensity/10;
        }
    }

    private void updateLevelProgress() {
        float endTime = boss1Time;
        float startY = -100;
        float endY = 410;
        
        if(GameStateManager.Instance.gameState.boss1EverComplete) {
            endTime = boss1Time*4;
        }
        
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

    private void updateProgressBarBackground() {
        Debug.Log("updateProgressBarBackground boss1EverComplete: " + GameStateManager.Instance.gameState.boss1EverComplete);
        if(GameStateManager.Instance.gameState.boss1EverComplete) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/LevelProgress2");
            levelProgressBackground.transform.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }
    }

    private void boss1End() {
        boss1Complete = true;
        GameStateManager.Instance.gameState.boss1EverComplete = true;
        updateProgressBarBackground();
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
