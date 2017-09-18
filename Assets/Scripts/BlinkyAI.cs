using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : MonoBehaviour {

    public GameObject Pacman;

    private int movingDirection = 1;
	
	// Update is called once per frame
	void Update () {

        //make the movement
        if (movingDirection == 1)
            transform.Translate(Vector3.left * Time.deltaTime);

        if (movingDirection == 2)
            transform.Translate(Vector3.right * Time.deltaTime);

        if (movingDirection == 3)
            transform.Translate(Vector3.forward * Time.deltaTime);

        if (movingDirection == 4)
            transform.Translate(Vector3.back * Time.deltaTime);

	}
    
    void OnTriggerEnter(Collider col) {
        //if blinky has hit a wall, then its already too late to choose a direction, go the opposite way of what we were doing
        if (col.gameObject.tag == "Wall") {
            if (movingDirection == 1) {
                movingDirection = 2;
                return;
            } else if (movingDirection == 2) {
                movingDirection = 1;
                return;
            } else if (movingDirection == 3) {
                movingDirection = 4;
                return;
            } else if (movingDirection == 4) {
                movingDirection = 3;
                return;
            }
        }
    }

    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Intersection") {
            //ignore Y axis for position tracking
            Vector3 a = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);
            Vector3 b = new Vector3(col.gameObject.transform.position.x, 0, col.gameObject.transform.position.z);

            //make sure we are almost centered with the intersection before making a decision
            if (Vector3.SqrMagnitude(a - b) < 0.0002f) {
                //decide the direction to go

                //calculate z distance to pacman
                float z = Mathf.Abs(this.gameObject.transform.position.z - Pacman.gameObject.transform.position.z);

                //calculate x distance to pacman
                float x = Mathf.Abs(this.gameObject.transform.position.x - Pacman.gameObject.transform.position.x);

                //find the minimum between the two
                float min = Mathf.Max(x, z);

                if (min == x) {
                    if (this.gameObject.transform.position.x - Pacman.gameObject.transform.position.x < 0) {
                        //go left
                        movingDirection = 1;
                    } else {
                        //go right
                        movingDirection = 2;
                    }
                } else {
                    if (this.gameObject.transform.position.z - Pacman.gameObject.transform.position.z < 0) {
                        //go up
                        movingDirection = 3;
                    } else {
                        //go down
                        movingDirection = 4;
                    }
                }
            }
        }
    }
}
