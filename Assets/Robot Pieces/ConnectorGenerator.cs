using UnityEngine;
using System.Collections;

public class ConnectorGenerator : MonoBehaviour {
	public Vector3[] positionsToBeGenerated;
	public GameObject ConnectorModel;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < positionsToBeGenerated.Length; i++) {
			GameObject c = (GameObject) Instantiate (ConnectorModel);
			c.transform.parent = this.transform;
			c.transform.localPosition = positionsToBeGenerated[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
