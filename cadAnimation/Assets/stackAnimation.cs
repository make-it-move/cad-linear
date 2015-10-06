using UnityEngine;
using System.Collections;

public class stackAnimation : MonoBehaviour {
	public int stackingSpeed = 10;
	private bool canAssemble = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (canAssemble && transform.position.y > 30) {
			transform.Translate ( new Vector3(0, 0, -1 * stackingSpeed * Time.deltaTime));
		}
	}

	void startAssembly() {
		canAssemble = true;
	}
}
