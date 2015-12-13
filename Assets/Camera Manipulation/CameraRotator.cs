using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {
	public float distance = 1;
	public float period = 1;
	public float angle = 0;
	private float verticalAngle = .5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		angle += Mathf.PI * 2 / period * Time.deltaTime * (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
		angle -= Mathf.PI * 2 / period * Time.deltaTime * (Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
		verticalAngle += Mathf.PI * 2 / period * Time.deltaTime * (Input.GetKey(KeyCode.UpArrow) ? 1 : 0);
		verticalAngle -= Mathf.PI * 2 / period * Time.deltaTime * (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);

		gameObject.transform.localPosition = new Vector3(-Mathf.Sin (angle)*distance*Mathf.Cos(verticalAngle), Mathf.Sin(verticalAngle)*distance, -Mathf.Cos (angle)*distance*Mathf.Cos(verticalAngle));
		gameObject.transform.eulerAngles = new Vector3(verticalAngle*Mathf.Rad2Deg, angle*Mathf.Rad2Deg, 0);
	
		if (Input.GetKey (KeyCode.Comma)) {
			distance *= 1 + Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.Period)) {
			distance *= 1 - Time.deltaTime;
		}
	}
}
