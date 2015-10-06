using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class linearMovement : MonoBehaviour {
	public int movementSpeed = 0;

	// Use this for initialization
	void Start () {
		
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
		movementSpeed = -20;
	}

	void moveRight(){
		movementSpeed = 20;
		sendData ();
	}

	void stop() {
		movementSpeed = 0;
	}

	void sendData(){
		SerialPort serialPort = new SerialPort ();
		string VER_Command = "R";
	
		if (serialPort is SerialPort) {
			serialPort.PortName = "/dev/cu.usbmodem1421";
			serialPort.DataBits = 8;
			serialPort.Parity = Parity.None;
			serialPort.StopBits = StopBits.One;
			serialPort.BaudRate = 9600;
		
			try {
				serialPort.Open ();
				serialPort.DiscardOutBuffer ();
				serialPort.DiscardInBuffer ();
			
				//serialPort.DataReceived += new SerialDataReceivedEventHandler (responseHandler);
				serialPort.Write (VER_Command);
			} catch (System.Exception exc) {
			}// end CATCH portion of TRY/CATCH block
		}// end IF serialPort is viable
	}

}
