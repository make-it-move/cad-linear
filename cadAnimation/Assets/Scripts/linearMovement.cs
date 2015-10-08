using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MovablePart {
	public int standartMovementSpeed = 0;
	private int moving;
	private int movementSpeed;
	public Slider serial;
	private bool periodicMovement = false;
	public Slider periodicStart;
	public Slider periodicEnd;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		setSpeed ((int)serial.value);
		if (periodicMovement) {
			//Debug.Log("moving periodicly");
			if(moving == -1 && transform.position.z < periodicStart.value){
				moveRight();
			} else if ( moving == 1 && transform.position.z > periodicEnd.value){
				moveLeft();
			}
			transform.Translate (new Vector3 (0,
			                                  0, 
			                                  movementSpeed * Time.deltaTime));
		} else {
			if (transform.position.z < 390 && transform.position.z > -35) {
				transform.Translate (new Vector3 (0,
			                                  0, 
			                                  movementSpeed * Time.deltaTime));
			}
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

	public void togglePeriodicMovement(){
		periodicMovement = !periodicMovement;
		moveRight ();
	}
}
