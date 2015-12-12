using UnityEngine;
using System.Collections;

public class BuildModeControls : MonoBehaviour {
	public Material connectorSelected;
	public Material connectorDeselected;

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

		if (Input.GetKeyDown (KeyCode.S)) {
			for(int i = 0;i < selectedConnectors.Count;i++){
				((GameObject)selectedConnectors[i]).transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}

	public void clearSelections(){
		foreach(GameObject con in selectedConnectors){
			con.GetComponent<MeshRenderer>().material = connectorDeselected;
		}
		selectedConnectors.Clear();
	}
}
