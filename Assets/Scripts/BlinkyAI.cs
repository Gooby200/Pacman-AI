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
            if (Vector3.SqrMagnitude(a - b) < 0.0004f) {
				//get the distances to pacman
				float left = col.gameObject.GetComponent<Intersection>().Left ? (Vector3.Distance(new Vector3(this.transform.position.x - 0.3757539f, 0, this.transform.position.z), new Vector3(Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
				float right = col.gameObject.GetComponent<Intersection>().Right ? (Vector3.Distance(new Vector3(this.transform.position.x + 0.3757539f, 0, this.transform.position.z), new Vector3(Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
				float up = col.gameObject.GetComponent<Intersection>().Up ? (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z + 0.3757539f), new Vector3(Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
				float down = col.gameObject.GetComponent<Intersection>().Down ? (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z - 0.3757539f), new Vector3(Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;

				//find the minimum distance
				float min = Mathf.Min(left, right, up, down);

				if (movingDirection == 1) {
					if (min == down) {
						movingDirection = 4;
					} else if (min == up) {
						movingDirection = 3;
					}
				} else if (movingDirection == 2) {
					if (min == down) {
						movingDirection = 4;
					} else if (min == up) {
						movingDirection = 3;
					}
				} else if (movingDirection == 3) {
					if (min == left) {
						movingDirection = 1;
					} else if (min == right) {
						movingDirection = 2;
					}
				} else if (movingDirection == 4) {
					if (min == left) {
						movingDirection = 1;
					} else if (min == right) {
						movingDirection = 2;
					}
				}
            }
        }
    }
}
