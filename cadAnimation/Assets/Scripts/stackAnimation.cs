using UnityEngine;
using System.Collections;

public class stackAnimation : MonoBehaviour {
	public int stackingSpeed = 10;
	public GameObject parent;
	public GameObject child;
	private bool isFree = true;
	private bool canAssemble = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isFree && canAssemble) {
			/*child.transform.position = new Vector3 (child.transform.position.x, 
			                                        child.transform.position.y, 
			                                        parent.transform.position.z);*/
			child.transform.parent = parent.transform;
			child.transform.localPosition = new Vector3 (child.transform.localPosition.x,
			                                             child.transform.localPosition.y,
			                                        45);

			isFree = false;
		}

		if (canAssemble && child.transform.position.y > 33) {
			child.transform.Translate ( new Vector3(0, -1 * stackingSpeed * Time.deltaTime, 0),Space.World);
		}
	}

	void startAssembly() {
		canAssemble = true;
	}
}
