using UnityEngine;
using System.Collections;

public class PieceGenerator : MonoBehaviour {

    public GameObject cube;
    public GameObject bar9;

    public ConnectorControls cc;
    public ControlTypeManager ctm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createCube()
    {
        GameObject o = (GameObject)Instantiate(cube, Vector3.zero, Quaternion.identity);

        //allow object to add to overall connectors list.
        o.GetComponent<ConnectorGenerator>().cc = cc;

        //if in piece mode, disable connectors
        if(ctm.controlType == 1)
        {
            foreach(GameObject con in transform)
            {
                con.GetComponent<MeshRenderer>().enabled = false;
                con.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void createBar9()
    {
        GameObject o = (GameObject)Instantiate(bar9, Vector3.zero, Quaternion.identity);

        //allow object to add to overall connectors list.
        o.GetComponent<ConnectorGenerator>().cc = cc;

        //if in piece mode, disable connectors
        if (ctm.controlType == 1)
        {
            foreach (GameObject con in transform)
            {
                con.GetComponent<MeshRenderer>().enabled = false;
                con.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
