using UnityEngine;
using System.Collections;

public class rotationPad : MonoBehaviour {
	public int rotationSpeed = 5;
	public GameObject rotateAround;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(rotateAround.transform.position, new Vector3(0,1,0), rotationSpeed * Time.deltaTime);
	}
}
