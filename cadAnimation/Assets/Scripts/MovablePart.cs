using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class MovablePart : MonoBehaviour {
	//Windows Config
	//public static string SerialPort = "COM3";
	//Mac Config
	public static string SerialPort = "/dev/cu.usbmodem1411";
	private static SerialPort serialPort = new System.IO.Ports.SerialPort();

	// Use this for initialization
	public void Start () {
		openAndCofigureSerialPort ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void sendSerialData(string direction){
		if (serialPort.IsOpen) {
			serialPort.Write(direction);
		}
	}
	
	static void openAndCofigureSerialPort(){
		serialPort = new SerialPort ();
		serialPort.PortName = MovablePart.SerialPort;
		serialPort.DataBits = 8;
		serialPort.Parity = Parity.None;
		serialPort.StopBits = StopBits.One;
		serialPort.BaudRate = 9600;
		
		try {
			serialPort.Open ();
			serialPort.DiscardOutBuffer ();
			serialPort.DiscardInBuffer ();
		} catch (System.Exception exc) {
			Debug.LogError(exc.Message);
		}// end CATCH portion of TRY/CATCH block
	}
}
