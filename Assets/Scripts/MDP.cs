using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;

		int rows = 20;
		int columns = 20;
	
		float tileWidth = (this.transform.localScale.x * 10) / columns;
		float tileHeight = (this.transform.localScale.z * 10) / rows;

		Gizmos.DrawWireCube (new Vector3 (0, 0, 0), new Vector3 (tileWidth, 2f, tileHeight));

		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				//Gizmos.DrawCube (new Vector3 (c, 2f, r), new Vector3 (rows, 2f, columns));
			}
		}


	}
}
