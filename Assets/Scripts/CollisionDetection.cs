using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

    public bool isColliding = false;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Wall") {
            isColliding = true;
        } else {
            isColliding = false;
        }
    }
}
