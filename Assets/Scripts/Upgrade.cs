using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Upgrade 
{
    public static readonly Upgrade LaserSpeed1 = new Upgrade(1, "Laser Speed 1.0", 25, "Sprites/Laser", 4, 100);
    public static readonly Upgrade Shield1 = new Upgrade(2, "Shield 1.0", 50, "Sprites/Shield", 100, 100);
    public static readonly Upgrade ShieldRegen = new Upgrade(3, "Shield Regen", 100, "Sprites/Shield", 100, 100);
    public static readonly Upgrade LaserSpeed2 = new Upgrade(4, "Laser Speed 2.0", 150, "Sprites/Laser", 4, 100);
    public static readonly Upgrade Shield2 = new Upgrade(5, "Shield 2.0", 200, "Sprites/Shield", 100, 100);
    public static readonly Upgrade DoubleLaser = new Upgrade(6, "Double Laser", 300, "Sprites/Laser", 4, 100);
    
    //public static readonly Upgrade TwoLaser = new Upgrade(4, "Double Laser", 500, "Sprites/Laser", 4, 100);
    //public static readonly Upgrade Torpedo = new Upgrade(7, "Photon Torpedo", 1000, "Sprites/Laser", 4, 100);
    
    public static IEnumerable<Upgrade> Values 
    {
        get
        {
            yield return LaserSpeed1;
            yield return Shield1;
            //yield return TwoLaser;
            yield return Shield2;
            yield return LaserSpeed2;
            yield return ShieldRegen;
            yield return DoubleLaser;
            //yield return Torpedo;
            
        }
    }

    public int id;
    public string displayName;
    public int cost;
    public string imagePath;
    public float imageWidth;
    public float imageHeight;

    Upgrade(int id, string name, int cost, string imagePath, float imageWidth, float imageHeight) => 
        (this.id, this.displayName, this.cost, this.imagePath, this.imageWidth, this.imageHeight) = (id, name, cost, imagePath, imageWidth, imageHeight);

    public override string ToString() => displayName;
    
    private bool Equals(Upgrade other)
    {
        return null != other && id == other.id;
    }
    public override bool Equals(object obj)
    {
        return Equals((obj as Upgrade));
    }
    public override int GetHashCode()
    {
        return id;
    }
}