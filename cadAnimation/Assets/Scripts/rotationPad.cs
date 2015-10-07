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
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		setSpeed ((int)slider.value);
		transform.RotateAround (rotateAround.transform.position, new Vector3 (0, 1, 0), rotationSpeed * Time.deltaTime);

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


}
