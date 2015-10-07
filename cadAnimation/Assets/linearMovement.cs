using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MovablePart {
	public int movementSpeed = 0;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < 390 && transform.position.z > -35) {
			transform.Translate (new Vector3 (0, 
		                                 0, 
		                                 movementSpeed * Time.deltaTime));
		}
	}

	void moveLeft(){
		sendSerialData ("L");
		movementSpeed = -20;
	}

	void moveRight(){
		sendSerialData ("R");
		movementSpeed = 20;

	}

	void stop() {
		sendSerialData ("S");
		movementSpeed = 0;
	}
}
