using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class rotationPad : MovablePart {
	public int standartRotationSpeed = 0;
	public GameObject rotateAround;
	private int rotationSpeed = 0;
	public Slider slider;
	public int rotating;
	public int limitAClockwise = 320;
	public int limitClockwise = 210;
	private bool periodicMovement = false;
	public Slider periodicStart;
	public Slider periodicEnd;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		setSpeed ((int)slider.value);

		if (periodicMovement) {

			if (rotating == -1 && 
			    transform.rotation.eulerAngles.z < sliderToRotation(periodicStart.value) + 10 && 
			    transform.rotation.eulerAngles.z > sliderToRotation(periodicEnd.value)) {
				rotateClockwise ();
			} else if (rotating == 1 && 
			           transform.rotation.eulerAngles.z > sliderToRotation(periodicEnd.value) && 
			           transform.rotation.eulerAngles.z < sliderToRotation(periodicStart.value) - 10) {
				rotateAnticlockwise ();
			}
		} else {


			if (rotating == 1 && transform.rotation.eulerAngles.z > limitClockwise && transform.rotation.eulerAngles.z < limitAClockwise - 10)
				stop ();
			if (rotating == -1 && transform.rotation.eulerAngles.z > limitClockwise + 10 && transform.rotation.eulerAngles.z < limitAClockwise)
				stop ();
		}
			//if(rotating == -1 && transform.rotation.eulerAngles.z
			transform.RotateAround (rotateAround.transform.position, new Vector3 (0, 0, 1), rotationSpeed * Time.deltaTime);
		
	}

	void rotateClockwise(){
		sendSerialData ("r");
		rotationSpeed = standartRotationSpeed;
		rotating = 1;
	}

	void rotateAnticlockwise(){
		sendSerialData ("l");
		rotationSpeed = -1 * standartRotationSpeed;
		rotating = -1;
	}

	void stop() {
		sendSerialData ("s");
		rotationSpeed = 0;
		rotating = 0;
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
			rotateClockwise ();
		} else {
			stop();
		}
	}

	float sliderToRotation ( float sliderValue){
		if (sliderValue < 35) {
			return 360 - (35-sliderValue);
		} else {
			return sliderValue -35;
		}
	}
}
