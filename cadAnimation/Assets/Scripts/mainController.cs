using UnityEngine;
using System.Collections;

public class mainController : MonoBehaviour {
	public GameObject[] linearComponents;
	public GameObject[] rotationComponents;
	public GameObject[] assemblyComponents;
	public rotationPad rotation;
	public linearMovement linear;
	public int stateCounter;
	public bool readyForNextMoveLinear = true;
	public bool readyForNextMoveRotation = true;
	public bool runSong1 = false;
	public bool runSong2 = false;

	// Use this for initialization
	void Start () {
		stateCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (runSong1) {

			if (readyToMove (new int[] { 
				1, 2, 3, 4, 5, 
				7, 15, 17, 23, 25, 
				29, 31, 43, 45, 51,
			  	52, 53})) {
				linear.moveOneStepRightStart ();
			} else if (readyToMove (new int[] { 
				9, 11, 13, 19, 20, 
				21, 27, 35, 37, 39,
				41, 47, 48, 49, 54,
				56, 58, 59})) {
				linear.moveOneStepLeftStart ();
			} else if (readyToMove (new int[] {
				6, 8, 10, 12, 14,
				16, 18, 22, 24, 26,
				28, 30, 32, 34, 36, 
				38, 40, 42, 44, 46, 
				49, 54, 57, 60, 33})) {
				rotation.hitOnceStart ();
			} //33Delay

		} else if (runSong2) {

			if (readyToMove (new int[] {
				2, 4, 6, 8, 12, 19
				})) {
				linear.moveOneStepRightStart ();
			} else if (readyToMove (new int[] { 
				17
				})) {
				linear.moveOneStepLeftStart ();
			} else if (readyToMove (new int[] {
				1, 3, 5, 7, 9, 11, 13, 14, 15, 16, 18, 10
				})) {
				rotation.hitOnceStart ();
			}
		}
	}

	bool readyToMove (int[] states){
		if(readyForNextMoveLinear && readyForNextMoveRotation){
			foreach (int state in states) {
				if (stateCounter == state) {
					stateCounter ++;
					return true;
				}
			}
		}
		return false;
	}

	public void startSong1(){
		linear.sendSerialData ("P100");
		runSong1 = true;
		runSong2 = false;
		stateCounter = 1;
		linear.moveFullyLeft ();
	}

	public void startSong2(){
		linear.sendSerialData ("P200");
		runSong1 = false;
		runSong2 = true;
		stateCounter = 1;
		linear.moveFullyLeft ();
	}

	void toogleActivityOfComponents(GameObject[] components, bool active){
		foreach (GameObject obj in components) {
			obj.SetActive(active);
		}
	}

	void toggleLinear(bool active){
		toogleActivityOfComponents (linearComponents, active);
	}

	void toggleRotation(bool active){
		toogleActivityOfComponents (rotationComponents, active);
	}

	void toggleAssembly(bool active){
		toogleActivityOfComponents (assemblyComponents, active);
	}
}
