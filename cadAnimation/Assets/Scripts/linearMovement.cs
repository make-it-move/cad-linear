using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MovablePart {
	public int standartMovementSpeed = 0;
	private int moving = 0;
	private int movementSpeed;
	public Slider speedSlider;
	private bool periodicMovement = false;
	public Slider periodicStart;
	public Slider periodicEnd;

	private bool leftStep = false;
	private bool rightStep = false;
	public float stepLength = 10.0f;
	public float stepStartingPoint;
	public mainController master;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (standartMovementSpeed != (int) speedSlider.value) {
			setSpeed ((int)speedSlider.value);
		}

		if (leftStep && stepStartingPoint - transform.position.z > stepLength) {
			leftStep = false;
			master.readyForNextMoveLinear = true;
			stop ();
		} else if (rightStep && transform.position.z - stepStartingPoint > stepLength) {
			rightStep = false;
			master.readyForNextMoveLinear = true;
			stop();
		} else if (periodicMovement) {
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
		Debug.Log("L" + convertMovementSpeedToMotorSpeed(-1 * standartMovementSpeed));
		sendSerialData ("L" + convertMovementSpeedToMotorSpeed(-1 * standartMovementSpeed));
		movementSpeed = -1 * standartMovementSpeed;
		moving = -1;
	}

	void moveRight(){
		
		Debug.Log("L" + convertMovementSpeedToMotorSpeed( standartMovementSpeed));
		sendSerialData ("L" + convertMovementSpeedToMotorSpeed(standartMovementSpeed));
		movementSpeed = standartMovementSpeed;
		moving = 1;
	}

	void stop() {
		sendSerialData ("L090");
		movementSpeed = 0;
		moving = 0;
	}

	public void moveOneStepLeftStart(){
		master.readyForNextMoveLinear = false;
		stepStartingPoint = transform.position.z;
		leftStep = true;
		rightStep = false;
		periodicMovement = false;
		moveLeft ();
	}

	public void moveOneStepRightStart(){
		master.readyForNextMoveLinear = false;
		stepStartingPoint = transform.position.z;
		rightStep = true;
		leftStep = false;
		periodicMovement = false;
		moveRight ();
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
	
	string convertMovementSpeedToMotorSpeed (int speed) {
		int max = (int)speedSlider.maxValue;
		int motorSpeed = (int) ((speed + max) * 180.0 / (max+max));
		string speedString = motorSpeed.ToString ();
		while (speedString.Length < 3) {
			speedString = "0" + speedString;
		}
		return speedString;
	}
}
