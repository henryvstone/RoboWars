using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {
	public float distance = 1;
	public float period = 1;
	private float angle = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		angle += Mathf.PI * 2 / period * Time.deltaTime;
		gameObject.transform.localPosition = new Vector3(-Mathf.Sin (angle)*distance, .5f, -Mathf.Cos (angle)*distance);
		gameObject.transform.eulerAngles = new Vector3(35, angle*Mathf.Rad2Deg, 0);
	}
}
