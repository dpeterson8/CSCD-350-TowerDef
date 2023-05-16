using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager: Singleton<GameManager> {

    public void SpawnHeroes() {

    }

    void
     spawnUnit(GameObject type, Vector3 position) {

        GameObject unit = Instantiate(Resources.Load("Prefabs/" + type.ToString()) as GameObject, position, Quaternion.identity);
    }

}