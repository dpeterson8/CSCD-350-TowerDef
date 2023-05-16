using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class BasicEnemyUnit: ScriptableObject {

    [SerializeField] private GCNotificationStatus _stats;


    public string Description;
    public Sprite MenuSprite;

}

[Serializable]
public struct Stats {
    public int health;
    public int damage;
    public int speed;
}