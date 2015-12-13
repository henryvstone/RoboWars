using UnityEngine;
using System.Collections;

public class PieceControls : MonoBehaviour {
	
	// Use this for initialization
	public Material connectorSelected;
	public Material connectorDeselected;
	
	public ArrayList connectors;
	
	ArrayList selectedConnectors;
	
	// Use this for initialization
	void Start () {
		connectors = new ArrayList();
		selectedConnectors = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rh = new RaycastHit ();
			if (Physics.Raycast (ray, out rh)) {
				if (rh.collider.tag.Equals ("Piece")) {
					GameObject connector = rh.collider.gameObject;
					if (selectedConnectors.Contains (connector)) {
						selectedConnectors.Remove (connector);
						connector.GetComponent<MeshRenderer> ().material = connectorDeselected;
					} else {
						selectedConnectors.Add (connector);
						connector.GetComponent<MeshRenderer> ().material = connectorSelected;
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			clearSelections ();
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			for (int i = 0; i < selectedConnectors.Count; i++) {
				((GameObject)selectedConnectors [i]).GetComponent<Rigidbody> ().isKinematic = false;
			}
		}

		if (Input.GetMouseButton (1)) {
			float angle = -GetComponent<CameraRotator>().angle;
			float dist = GetComponent<CameraRotator>().distance;

			float deltaX = Input.mousePosition.x - lastPosition.x;
			for (int i = 0; i < selectedConnectors.Count; i++) {
				((GameObject)selectedConnectors [i]).transform.Translate(Mathf.Cos(angle)*deltaX/1000*dist, 0, Mathf.Sin(angle)*deltaX/1000*dist);
			}

			float deltaY = Input.mousePosition.y - lastPosition.y;
			for (int i = 0; i < selectedConnectors.Count; i++) {
				((GameObject)selectedConnectors [i]).transform.Translate(0, deltaY/1000*dist, 0);
			}
		}

		lastPosition = Input.mousePosition;
	}

	Vector2 lastPosition;
	
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
