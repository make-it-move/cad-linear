using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class rotationPad : MovablePart {
	public int standartRotationSpeed = 0;
	public GameObject rotateAround;
	private int rotationSpeed = 0;
	public Slider speedSlider;
	private int rotating = 0;
	public int limitAClockwise = 30;
	public int limitClockwise = 330;
	private bool periodicMovement = false;
	private bool hitOnce = false;
	private bool hit = false;
	public Slider periodicStart;
	public Slider periodicEnd;
	public mainController master;

	// Use this for initialization
	void Start () {
		//base.Start ();
		transform.RotateAround (rotateAround.transform.position, new Vector3 (0, 0, 1), 90);
	}
	
	// Update is called once per frame
	void Update () {
		if (standartRotationSpeed != (int) speedSlider.value) {
			setSpeed ((int)speedSlider.value);
		}
		if (hitOnce) {
			if(!hit && rotating != -1){
				rotateAnticlockwise(false);
			} else if (hit && rotating == 1 && transform.localRotation.eulerAngles.z > sliderToRotation(periodicEnd.value)){
				stop(false);
				master.readyForNextMoveRotation = true;
				hitOnce = false;
				hit = false;
			} else if (rotating == -1 && 
			           transform.localRotation.eulerAngles.z < sliderToRotation(periodicStart.value)){
				hit = true;
				rotateClockwise(false);
			}

		} else if (periodicMovement) {
			if (rotating == -1 && 
			    transform.localRotation.eulerAngles.z < sliderToRotation(periodicStart.value)) {
				rotateClockwise ();
			} else if (rotating == 1 && 
			           transform.localRotation.eulerAngles.z > sliderToRotation(periodicEnd.value)) {
				rotateAnticlockwise ();
			}
		} else {
			if (rotating == 1 && transform.localRotation.eulerAngles.z > limitClockwise)
				stop ();
			if (rotating == -1 && transform.localRotation.eulerAngles.z < limitAClockwise)
				stop ();
		}
		transform.RotateAround (rotateAround.transform.position, new Vector3 (0, 0, 1), rotationSpeed * Time.deltaTime);
		
	}

	void rotateClockwise(){
		rotateClockwise (true);
	}

	void rotateClockwise(bool sendSerial){
		if (sendSerial) {
			sendSerialData ("R" + convertMovementSpeedToMotorSpeed (standartRotationSpeed));
		}
		rotationSpeed = standartRotationSpeed;
		rotating = 1;
	}

	void rotateAnticlockwise(){
		rotateAnticlockwise (true);
	}

	void rotateAnticlockwise(bool sendSerial){
		if (sendSerial) {
			sendSerialData ("R" + convertMovementSpeedToMotorSpeed (-1 * standartRotationSpeed));
		}
		rotationSpeed = -1 * standartRotationSpeed;
		rotating = -1;
	}

	void stop(){
		stop (true);
	}

	void stop(bool sendSerial) {
		if (sendSerial) {
			sendSerialData ("R090");
		}
		rotationSpeed = 0;
		rotating = 0;
	}

	public void hitOnceStart(){
		master.readyForNextMoveRotation = false;
		hitOnce = true;
		periodicMovement = false;
	}

	public void setSpeed(int value){
		standartRotationSpeed = value;
		if (rotating > 0) {
			rotateClockwise();
		} else if (rotating < 0 ) {
			rotateAnticlockwise();
		}
	}

	public void togglePeriodicMovement(){
		periodicMovement = !periodicMovement;
		if (periodicMovement) {
			hitOnce = false;
			rotateClockwise ();
		} else {
			stop();
		}
	}

	float sliderToRotation ( float sliderValue){
		return sliderValue;
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
