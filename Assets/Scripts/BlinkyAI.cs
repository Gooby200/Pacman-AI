using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : MonoBehaviour {

    public GameObject Left;
    public GameObject Right;
    public GameObject Top;
    public GameObject Bottom;

    private int movingDirection = 1;
    private int requestedDirection = 1;

    private bool colLeft = false;
    private bool colRight = false;
    private bool colTop = false;
    private bool colBottom = false;
	
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


        //check collisions
        //checkCollisions();

        //determine direction to move
        //changeDirection();
	}

    void changeDirection() {
        if (colLeft == false && movingDirection != 1) {
            movingDirection = 1;
        } else if (colRight == false && movingDirection != 2) {
            movingDirection = 2;
        } else if (colTop == false && movingDirection != 3) {
            movingDirection = 3;
        } else if (colBottom == false && movingDirection != 4) {
            movingDirection = 4;
        }
    }

    //if blinky has hit a wall, then its already too late to choose a direction, go the opposite way of what we were doing
    void OnTriggerEnter(Collider col) {
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

    void checkCollisions() {
        if (Left.GetComponent<CollisionDetection>().isColliding) {
            colLeft = true;
        } else {
            colLeft = false;
        }

        if (Right.GetComponent<CollisionDetection>().isColliding) {
            colRight = true;
        } else {
            colRight = false;
        }

        if (Top.GetComponent<CollisionDetection>().isColliding) {
            colTop = true;
        } else {
            colTop = false;
        }

        if (Bottom.GetComponent<CollisionDetection>().isColliding) {
            colBottom = true;
        } else {
            colBottom = false;
        }
    }
}
