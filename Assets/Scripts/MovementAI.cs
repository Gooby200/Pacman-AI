using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour {

    public GameObject Pacman;
	private Vector3 lastIntersectionHit;
	private Vector3 currentIntersection;

	private float futureDistance = 99999f;
	private float currentDistance = 99999f;

	private bool allowPass = false;

	private int movingDirection = 1;
	private int requestedMovingDirection = 1;

	public Vector3 TargetLocation;

	void Start() {
		movingDirection = ((int) (Random.value * 2)) + 1;
		TargetLocation = Pacman.transform.position;

		lastIntersectionHit = new Vector3 (0, 0, 0);
		currentIntersection = new Vector3 (0, 0, 0);
	}

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
				requestedMovingDirection = 2;
            } else if (movingDirection == 2) {
				requestedMovingDirection = 1;
            } else if (movingDirection == 3) {
				requestedMovingDirection = 4;
            } else if (movingDirection == 4) {
				requestedMovingDirection = 3;
            }
			movingDirection = requestedMovingDirection;
        }
    }

    void OnTriggerStay(Collider col) {
		//this decision making is standard for every ghost
		if (col.gameObject.tag == "Intersection") {
			if (lastIntersectionHit != currentIntersection) {
				//Vector3 a = new Vector3 (this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);
				Vector3 b = new Vector3 (col.gameObject.transform.position.x, 0, col.gameObject.transform.position.z);


				//replacement for below
				//
				Vector3 futurePosition = new Vector3(0, 0, 0);
				if (movingDirection == 1)
					futurePosition = transform.position + (Vector3.left * Time.deltaTime);
				if (movingDirection == 2)
					futurePosition = transform.position + (Vector3.right * Time.deltaTime);
				if (movingDirection == 3)
					futurePosition = transform.position + (Vector3.forward * Time.deltaTime);
				if (movingDirection == 4)
					futurePosition = transform.position + (Vector3.back * Time.deltaTime);
				//


				//Replace this with some sort of calculation of future step to make determination whether to move or not
				//==============
				futureDistance = Vector3.SqrMagnitude (futurePosition - b);
				if (futureDistance < currentDistance) {
					currentDistance = futureDistance;
				}

				if (currentDistance < futureDistance) {
					allowPass = true;
				}
					
				//==============

				if (allowPass) {
					lastIntersectionHit = currentIntersection;
					allowPass = false;
					futureDistance = 99999f;
					currentDistance = 99999f;

					//get the distances to pacman
					float left = col.gameObject.GetComponent<Intersection> ().Left ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x - Pacman.transform.localScale.x, 0, this.transform.position.z), new Vector3 (TargetLocation.x, 0, TargetLocation.z))) : 99999f;
					float right = col.gameObject.GetComponent<Intersection> ().Right ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x + Pacman.transform.localScale.x, 0, this.transform.position.z), new Vector3 (TargetLocation.x, 0, TargetLocation.z))) : 99999f;
					float up = col.gameObject.GetComponent<Intersection> ().Up ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x, 0, this.transform.position.z + Pacman.transform.localScale.x), new Vector3 (TargetLocation.x, 0, TargetLocation.z))) : 99999f;
					float down = col.gameObject.GetComponent<Intersection> ().Down ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x, 0, this.transform.position.z - Pacman.transform.localScale.x), new Vector3 (TargetLocation.x, 0, TargetLocation.z))) : 99999f;

					float min = 99999f;

					if (movingDirection == 1) {
						min = Mathf.Min (left, up, down);
						if (min == left) {
							requestedMovingDirection = 1;
						} else if (min == up) {
							requestedMovingDirection = 3;
						} else if (min == down) {
							requestedMovingDirection = 4;
						}
					} else if (movingDirection == 2) {
						min = Mathf.Min (right, up, down);
						if (min == right) {
							requestedMovingDirection = 2;
						} else if (min == up) {
							requestedMovingDirection = 3;
						} else if (min == down) {
							requestedMovingDirection = 4;
						}
					} else if (movingDirection == 3) {
						min = Mathf.Min (left, right, up);
						if (min == left) {
							requestedMovingDirection = 1;
						} else if (min == right) {
							requestedMovingDirection = 2;
						} else if (min == up) {
							requestedMovingDirection = 3;
						}
					} else if (movingDirection == 4) {
						min = Mathf.Min (left, right, down);
						if (min == left) {
							requestedMovingDirection = 1;
						} else if (min == right) {
							requestedMovingDirection = 2;
						} else if (min == down) {
							requestedMovingDirection = 4;
						}
					}

					movingDirection = requestedMovingDirection;

				}
			} else {
				//log the intersection we are on
				currentIntersection = col.gameObject.transform.position;
				allowPass = false;
				futureDistance = 99999f;
				currentDistance = 99999f;
			}


		}
    }
}
