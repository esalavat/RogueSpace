using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameVars")]
public class GameVars : ScriptableObject
{
    public int credits;
    public List<Upgrade> purchasedUpgrades;
    public bool showProgress;
    public bool debug = false;
}
