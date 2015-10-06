using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class rotationPad : MonoBehaviour {
	public int standartRotationSpeed = 0;
	public GameObject rotateAround;
	private SerialPort serialPort;
	private int rotationSpeed = 0;
	// Use this for initialization
	void Start () {
		openAndCofigureSerialPort ();
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
		rotationSpeed = -1* standartRotationSpeed;
	}

	void stop() {
		sendSerialData ("S");
		rotationSpeed = 0;
	}

	void sendSerialData(string direction){
		if (serialPort.IsOpen) {
			serialPort.Write(direction);
		}
	}
	
	void openAndCofigureSerialPort(){
		serialPort = new SerialPort ();
		serialPort.PortName = "/dev/cu.usbmodem1421";
		serialPort.DataBits = 8;
		serialPort.Parity = Parity.None;
		serialPort.StopBits = StopBits.One;
		serialPort.BaudRate = 9600;
		
		try {
			serialPort.Open ();
			serialPort.DiscardOutBuffer ();
			serialPort.DiscardInBuffer ();
		} catch (System.Exception exc) {
		}// end CATCH portion of TRY/CATCH block
	}
}
