using UnityEngine;
using System.Collections;

public class stackAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 30) {
			transform.Translate ( new Vector3(0, 0, -10 * Time.deltaTime));
		}
	}
}
