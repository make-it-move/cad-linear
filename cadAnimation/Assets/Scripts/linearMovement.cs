using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MovablePart {
	public int standartMovementSpeed = 0;
	private int moving;
	private int movementSpeed;
	public Slider serial;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		setSpeed ((int)serial.value);
		if (transform.position.z < 390 && transform.position.z > -35) {
			transform.Translate (new Vector3 (0,
			                                  0, 
			                                  movementSpeed * Time.deltaTime));
		}
	}

	void moveLeft(){
		sendSerialData ("L");
		movementSpeed = -1 * standartMovementSpeed;
		moving = -1;
	}

	void moveRight(){
		sendSerialData ("R");
		movementSpeed = standartMovementSpeed;
		moving = 1;

	}

	void stop() {
		sendSerialData ("S");
		movementSpeed = 0;
		moving = 0;
	}

	public void setSpeed(int value){
		standartMovementSpeed = value;
		if (moving > 0) {
			moveRight();
		} else if (moving < 0 ) {
			moveLeft();
		}
	}
}
