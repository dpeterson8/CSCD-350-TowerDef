using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAiMove : MonoBehaviour
{
    public float setSpeed;
    private Waypoints wPoints;
    private int wayPointIndex;

    void Start() {

        wPoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

    }

    void Update() {

        MoveChar(wPoints, setSpeed, wayPointIndex); 

    }

    private void MoveChar(Waypoints curWayPoints, float speed, int index) {
        // If character is in bounds of map move
        if(!(index > curWayPoints.waypoints.Length - 1)) {

            transform.position = Vector2.MoveTowards(transform.position, curWayPoints.waypoints[index].position, speed * Time.deltaTime);    

            if(Vector2.Distance(transform.position, curWayPoints.waypoints[index].position) < 0.1f) {
                index++;
            }

        } else {
            // If character reached last waypoint Destroy it
            Destroy(gameObject);
        }
    }
}
