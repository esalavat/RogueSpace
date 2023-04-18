using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameVars gameState;
    public static GameStateManager Instance { get; private set; }
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
            Destroy(this); 
        else 
            Instance = this; 
    }

    public GameVars getGameState() {
        return gameState;
    }

    public bool hasUpgrade(Upgrades upgrade) {
        if(Array.IndexOf(gameState.purchasedUpgrades, upgrade) >= 0) {
            return true;
        }

        return false;
    }
}
