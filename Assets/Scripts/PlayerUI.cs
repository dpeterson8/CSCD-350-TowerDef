using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject rangeTowerPre;
    public GameObject mageTowerPre;
    public void CreateTower(string towerTypeEnum) {
        if(towerTypeEnum.ToLower() == "range") {
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            Instantiate(rangeTowerPre, spawnPos, Quaternion.identity);
        } else if(towerTypeEnum.ToLower() == "mage") {
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            Instantiate(mageTowerPre, spawnPos, Quaternion.identity);
        }
    }
}
