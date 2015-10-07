using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class rotationPad : MovablePart {
	public int standartRotationSpeed = 0;
	public GameObject rotateAround;
	private int rotationSpeed = 0;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (rotateAround.transform.position, new Vector3 (0, 1, 0), rotationSpeed * Time.deltaTime);

	}

	void rotateClockwise(){
		sendSerialData ("R");
		rotationSpeed = standartRotationSpeed;
	}

	void rotateAnticlockwise(){
		sendSerialData ("L");
		rotationSpeed = -1 * standartRotationSpeed;
	}

	void stop() {
		sendSerialData ("S");
		rotationSpeed = 0;
	}


}
