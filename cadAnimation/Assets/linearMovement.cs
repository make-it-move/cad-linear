using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MonoBehaviour {
	public int movementSpeed = 0;
	SerialPort serialPort;

	// Use this for initialization
	void Start () {
		openAndCofigureSerialPort ();
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

	void sendSerialData(string direction){
		if (serialPort.IsOpen) {
			serialPort.Write(direction);
		}
	}

	void openAndCofigureSerialPort(){
		serialPort = new SerialPort ();
		serialPort.PortName = "/dev/cu.usbmodem1411";
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
