using UnityEngine;
using System.Collections;

public class ConnectorGenerator : MonoBehaviour {
    public Vector3[] positionsToBeGenerated;
    public Vector3[] connectorNormals;
    public GameObject ConnectorModel;

	public ConnectorControls cc;

	// Use this for initialization
	void Start () {
        GameObject Wrapper = new GameObject("Piece");

        transform.SetParent(Wrapper.transform);

		for (int i = 0; i < positionsToBeGenerated.Length; i++) {
			GameObject c = (GameObject) Instantiate (ConnectorModel);
			c.transform.parent = this.transform;
			c.transform.localPosition = positionsToBeGenerated[i];
            Vector3 normal = connectorNormals[i].normalized;

            c.transform.Rotate(-Mathf.Asin(normal.y) * Mathf.Rad2Deg, Mathf.Atan2(normal.x, normal.z) * 180 / Mathf.PI, 0);

            //Mathf.Atan2(normal.x, normal.z) * 180 / Mathf.PI

            cc.connectors.Add(c);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
