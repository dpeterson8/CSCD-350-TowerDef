using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float setSpeed;
    private Waypoints wPoints;
    private int wayPointIndex;
    public int health = 10;
    public int damage = 1;
    public int enemyValue = 10;

    public simpleSpawn enemyCount;

    void Start() {

        wPoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

    }

    void Update() {

        wayPointIndex = MoveChar(wPoints, setSpeed, wayPointIndex); 

    }

    private int MoveChar(Waypoints curWayPoints, float speed, int index) {
        // If character is in bounds of map move
        if(!(index > curWayPoints.waypoints.Length - 1)) {

            transform.position = Vector2.MoveTowards(transform.position, curWayPoints.waypoints[index].position, speed * Time.deltaTime);    

            if(Vector2.Distance(transform.position, curWayPoints.waypoints[index].position) < 0.1f) {
                index++;
            }

        } else {
            onEnd();
        }

        return index;
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if(health <= 0) {
            Player.Instance.money += enemyValue;
            Destroy(gameObject);
        }
    }

    public void onEnd() {
        Player.Instance.health -= damage;
        Destroy(gameObject);
    }
}
