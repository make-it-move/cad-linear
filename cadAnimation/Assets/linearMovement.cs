using UnityEngine;
using System.Collections;

public class linearMovement : MonoBehaviour {
	public int movementSpeed = 5;
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
}
