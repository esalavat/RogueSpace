using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BattleManager : MonoBehaviour
{
    public AsteroidSpawerScript asteroidSpawerScript;
    public GameObject boss1;

    void OnEnable() {
        EventManager.OnBoss1Start += startBattle;
        EventManager.OnBoss1End += stopBattle;
    }

    void OnDisable() {
        EventManager.OnBoss1Start -= startBattle;
        EventManager.OnBoss1End -= stopBattle;
    }

    private void startBattle() {
        Debug.Log("startBattle");
        asteroidSpawerScript.spawnAsteroids = false;
        asteroidSpawerScript.spawnEnemies = false;
        LogicManagerScript.bossFight = true;
        Invoke("spawnBoss1", 4);
    }

    private void spawnBoss1() {
        Instantiate(boss1, new Vector3(0, 5.75f, 0), Quaternion.identity);
    }

    private void stopBattle() {
        Debug.Log("stopBattle");
        asteroidSpawerScript.spawnAsteroids = true;
        asteroidSpawerScript.spawnEnemies = true;
        LogicManagerScript.bossFight = false;
        LogicManagerScript.boss1Complete = true;
    }
    
}
