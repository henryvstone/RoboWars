using UnityEngine;
using System.Collections;

public class ConnectorControls : MonoBehaviour {

	// Use this for initialization
	public Material connectorSelected;
	public Material connectorDeselected;

	public ArrayList connectors = new ArrayList();
	
	ArrayList selectedConnectors;
	
	// Use this for initialization
	void Start () {
		selectedConnectors = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rh = new RaycastHit();
			if (Physics.Raycast(ray, out rh)){
				if(rh.collider.tag.Equals("PieceConnector")){
					GameObject connector = rh.collider.gameObject;
					if(selectedConnectors.Contains(connector)){
						selectedConnectors.Remove(connector);
						connector.GetComponent<MeshRenderer>().material = connectorDeselected;
					}else{
						selectedConnectors.Add(connector);
						connector.GetComponent<MeshRenderer>().material = connectorSelected;
					}
				}
			}
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			clearSelections();
		}
		
		if (Input.GetKeyDown (KeyCode.C)) {
			if(selectedConnectors.Count >= 2){
				GameObject baseConnector = (GameObject)selectedConnectors[0];
				for(int i = 1;i < selectedConnectors.Count;i++){
					GameObject secondaryConnector = (GameObject)selectedConnectors[i];
					Vector3 distanceToNewLocation = baseConnector.transform.position - secondaryConnector.transform.position;
					secondaryConnector.transform.parent.Translate(distanceToNewLocation);
					FixedJoint fj = baseConnector.transform.parent.gameObject.AddComponent<FixedJoint>();
					fj.connectedBody = secondaryConnector.transform.parent.gameObject.GetComponent<Rigidbody>();
				}
				clearSelections();
			}
		}
	}

	public void stop(){
		clearSelections ();
		foreach (GameObject con in connectors) {
			con.GetComponent<MeshRenderer>().enabled = false;
			con.GetComponent<Collider>().enabled = false;
		}
	}

	public void start(){
		foreach (GameObject con in connectors) {
			con.GetComponent<MeshRenderer>().enabled = true;
			con.GetComponent<Collider>().enabled = true;
		}
	}

	public void clearSelections(){
		foreach(GameObject con in selectedConnectors){
			con.GetComponent<MeshRenderer>().material = connectorDeselected;
		}
		selectedConnectors.Clear();
	}

	public void disable(){
		foreach (GameObject con in connectors) {
			con.GetComponent<Collider> ().enabled = false;
			con.GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}