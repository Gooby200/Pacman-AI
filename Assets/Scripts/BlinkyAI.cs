using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : MonoBehaviour {

    public GameObject Pacman;
	private Vector3 lastIntersectionHit;
	private Vector3 currentIntersection;

	private float smallTrack1 = 99999f;
	private float smallTrack2 = 99999f;
	private bool allowPass = false;

    private int movingDirection = 1;
	private int requestedMovingDirection = 1;

	void Start() {
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
		if (col.gameObject.tag == "Intersection") {
			if (lastIntersectionHit != currentIntersection) {
				Vector3 a = new Vector3 (this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);
				Vector3 b = new Vector3 (col.gameObject.transform.position.x, 0, col.gameObject.transform.position.z);

				smallTrack1 = Vector3.SqrMagnitude (a - b);
				if (smallTrack1 < smallTrack2) {
					smallTrack2 = smallTrack1;
				}

				if (smallTrack2 < smallTrack1) {
					allowPass = true;
				}


				if (allowPass) {
					Debug.Log (smallTrack1 + ", " + smallTrack2);

					lastIntersectionHit = currentIntersection;
					allowPass = false;
					smallTrack1 = 99999f;
					smallTrack2 = 99999f;

					//get the distances to pacman
					float left = col.gameObject.GetComponent<Intersection> ().Left ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x - 0.4f, 0, this.transform.position.z), new Vector3 (Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
					float right = col.gameObject.GetComponent<Intersection> ().Right ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x + 0.4f, 0, this.transform.position.z), new Vector3 (Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
					float up = col.gameObject.GetComponent<Intersection> ().Up ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x, 0, this.transform.position.z + 0.4f), new Vector3 (Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;
					float down = col.gameObject.GetComponent<Intersection> ().Down ? Mathf.Abs (Vector3.Distance (new Vector3 (this.transform.position.x, 0, this.transform.position.z - 0.4f), new Vector3 (Pacman.transform.position.x, 0, Pacman.transform.position.z))) : 99999f;

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
				smallTrack1 = 99999f;
				smallTrack2 = 99999f;
			}


		}
    }
}
