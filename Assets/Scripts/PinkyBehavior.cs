using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyBehavior : MonoBehaviour {

	public GameObject Pacman;
	private int FacingDirection = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FacingDirection = Pacman.GetComponent<PacmanAI> ().FacingDirection;

		if (FacingDirection == 1) {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 2) {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = new Vector3 (Pacman.transform.position.x + (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 3) {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z + (Pacman.transform.localScale.x * 4f));
		} else if (FacingDirection == 4) {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = new Vector3 (Pacman.transform.position.x, Pacman.transform.position.y, Pacman.transform.position.z - (Pacman.transform.localScale.x * 4f));
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		if (FacingDirection == 1) {
			Gizmos.DrawCube (new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z), new Vector3(0.2f, 1f, 0.2f));
		} else if (FacingDirection == 2) {
			Gizmos.DrawCube (new Vector3 (Pacman.transform.position.x + (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z), new Vector3(0.2f, 1f, 0.2f));
		} else if (FacingDirection == 3) {
			Gizmos.DrawCube (new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 4f), Pacman.transform.position.y, Pacman.transform.position.z + (Pacman.transform.localScale.x * 4f)), new Vector3(0.2f, 1f, 0.2f));
		} else if (FacingDirection == 4) {
			Gizmos.DrawCube (new Vector3 (Pacman.transform.position.x, Pacman.transform.position.y, Pacman.transform.position.z - (Pacman.transform.localScale.x * 4f)), new Vector3(0.2f, 1f, 0.2f));
		}
	}
}
