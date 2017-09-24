using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyBehavior : MonoBehaviour {

	public GameObject Pacman;
	public GameObject Blinky;

	private int FacingDirection = 1;

	// Update is called once per frame
	void Update () {
		FacingDirection = Pacman.GetComponent<PacmanAI> ().FacingDirection;

		Vector3 normTarget = new Vector3(0, 0, 0);
		Vector3 extendedTarget = new Vector3 (0, 0, 0);

		if (FacingDirection == 1) {
			normTarget = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 2) {
			normTarget = new Vector3 (Pacman.transform.position.x + (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 3) {
			normTarget = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z + (Pacman.transform.localScale.x * 2f));
		} else if (FacingDirection == 4) {
			normTarget = new Vector3 (Pacman.transform.position.x, Pacman.transform.position.y, Pacman.transform.position.z - (Pacman.transform.localScale.x * 2f));
		}

		extendedTarget = new Vector3 (((normTarget.x - Blinky.transform.position.x) * 2f) + Blinky.transform.position.x, normTarget.y, ((normTarget.z - Blinky.transform.position.z) * 2f) + Blinky.transform.position.z);
		this.gameObject.GetComponent<MovementAI> ().TargetLocation = extendedTarget;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;


		Vector3 normTarget = new Vector3(0, 0, 0);
		Vector3 extendedTarget = new Vector3 (0, 0, 0);
		if (FacingDirection == 1) {
			normTarget = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 2) {
			normTarget = new Vector3 (Pacman.transform.position.x + (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z);
		} else if (FacingDirection == 3) {
			normTarget = new Vector3 (Pacman.transform.position.x - (Pacman.transform.localScale.x * 2f), Pacman.transform.position.y, Pacman.transform.position.z + (Pacman.transform.localScale.x * 2f));
		} else if (FacingDirection == 4) {
			normTarget = new Vector3 (Pacman.transform.position.x, Pacman.transform.position.y, Pacman.transform.position.z - (Pacman.transform.localScale.x * 2f));
		}

		extendedTarget = new Vector3 (((normTarget.x - Blinky.transform.position.x) * 2f) + Blinky.transform.position.x, normTarget.y, ((normTarget.z - Blinky.transform.position.z) * 2f) + Blinky.transform.position.z);
		Gizmos.DrawCube (normTarget, new Vector3 (0.2f, 1f, 0.2f));
		Gizmos.DrawLine (Blinky.transform.position, extendedTarget);
		Gizmos.DrawCube (extendedTarget, new Vector3(0.2f, 1f, 0.2f));
	}
}
