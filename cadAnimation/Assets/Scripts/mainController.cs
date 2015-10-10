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
	public bool runMusic = false;

	// Use this for initialization
	void Start () {
		stateCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (runMusic) {

			/*if (readyToMove (new int[] { 1, 2, 3, 4, 5, 7, 15, 17, 22, 24, 28, 30, 32, 35, 45, 47, 53, 54, 55 })) {
				linear.moveOneStepRightStart ();
			} else if (readyToMove (new int[] { 9, 11, 13, 19, 20, 26, 37, 39, 41, 43, 49, 50, 51, 57,58,60 })) {
				linear.moveOneStepLeftStart ();
			} else if (readyToMove (new int[] {6,8,10,12,14,16,18,21,23,25,27,29,31,33,34,36,38,40,42,44,46,48,52,56,59,61})) {
				//rotation.hitOnceStart ();
			}*/
			if(readyToMove(new int[] {1})){
				linear.moveOneStepLeftStart();
			} else if(readyToMove(new int[] {2})){
				linear.moveOneStepRightStart();
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

	public void startMusic(){
		runMusic = true;
		stateCounter = 1;
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
