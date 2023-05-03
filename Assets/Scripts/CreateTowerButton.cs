using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTowerButton : MonoBehaviour
{
    public GameObject towerPre;
    void OnMouseDown() {
        Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Instantiate(towerPre, spawnPos, Quaternion.identity);
    }
}
