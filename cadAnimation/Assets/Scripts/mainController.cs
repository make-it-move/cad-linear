using UnityEngine;
using System.Collections;

public class mainController : MonoBehaviour {
	public GameObject[] linearComponents;
	public GameObject[] rotationComponents;
	public GameObject[] assemblyComponents;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
