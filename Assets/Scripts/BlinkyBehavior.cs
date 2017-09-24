using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyBehavior : MonoBehaviour {
	public GameObject Pacman;

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawCube (new Vector3 (Pacman.transform.position.x, Pacman.transform.position.y, Pacman.transform.position.z), new Vector3(0.2f, 1f, 0.2f));
	}
}
