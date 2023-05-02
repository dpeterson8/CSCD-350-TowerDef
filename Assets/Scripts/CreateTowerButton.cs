using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTowerButton : MonoBehaviour
{
    public GameObject towerPre;
    void OnMouseDown() {
        Instantiate(towerPre, Input.mousePosition, Quaternion.identity);
    }
}
