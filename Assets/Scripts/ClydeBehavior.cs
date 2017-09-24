using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeBehavior : MonoBehaviour {

	public GameObject Pacman;
	public GameObject RetreatPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (checkInRadius ()) {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = RetreatPosition.transform.position;
		} else {
			this.gameObject.GetComponent<MovementAI> ().TargetLocation = Pacman.transform.position;
		}
	}


	void OnDrawGizmos() {
		Gizmos.color = new Color (Color.blue.r, Color.blue.g, Color.blue.b, 0.2f);
		Gizmos.DrawSphere (this.transform.position, this.transform.localScale.x * 8f);

		Gizmos.color = new Color (Color.blue.r, Color.blue.g, Color.blue.b, 1f);
		if (checkInRadius ()) {
			Gizmos.DrawCube (RetreatPosition.transform.position, new Vector3(0.2f, 1f, 0.2f));
		} else {
			Gizmos.DrawCube (Pacman.transform.position, new Vector3(0.2f, 1f, 0.2f));
		}



	}

	bool checkInRadius() {
		Collider[] collisions = Physics.OverlapSphere (this.transform.position, this.transform.localScale.x * 8f);
		foreach (Collider col in collisions) {
			if (col.gameObject == Pacman) {
				return true;
			}
		}

		return false;
	}
}
