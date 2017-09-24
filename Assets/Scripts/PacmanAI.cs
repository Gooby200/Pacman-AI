using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAI : MonoBehaviour {
	public Vector3 FacingPosition;
	public int FacingDirection = 2;

	public int MovingDirection = 2;

	// Update is called once per frame
	void Update () {
		//do AI stuff here (markov decision process?)

		//update the position of the location that pacman is facing
		if (MovingDirection == 1) {
			FacingPosition = new Vector3 (this.transform.position.x - 0.3757539f, 0, this.transform.position.z);
		} else if (MovingDirection == 2) {
			FacingPosition = new Vector3 (this.transform.position.x + 0.3757539f, 0, this.transform.position.z);
		} else if (MovingDirection == 3) {
			FacingPosition = new Vector3 (this.transform.position.x, 0, this.transform.position.z + 0.3757539f);
		} else if (MovingDirection == 4) {
			FacingPosition = new Vector3 (this.transform.position.x, 0, this.transform.position.z - 0.3757539f);
		}

		//update the facing direction (left, right, up or down)
		FacingDirection = MovingDirection;
	}
}
