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
                var newWrapper = new GameObject("Wrapper");
                for (int i = 1;i < selectedConnectors.Count;i++){
					GameObject secondaryConnector = (GameObject)selectedConnectors[i];

                    //check to see if objects are already connected / are under the same wrapper
                    if (baseConnector.transform.parent.parent != null && secondaryConnector.transform.parent.parent != null && baseConnector.transform.parent.parent == secondaryConnector.transform.parent.parent)
                    {
                        Debug.Log("Two pieces are already connected");
                    }
                    //if objects can be connected
                    else
                    {
                        Vector3 distanceToNewLocation = baseConnector.transform.position - secondaryConnector.transform.position;

                        //move objects to coincide with the new attachment
                        {
                            GameObject oldWrapper1 = secondaryConnector.transform.parent.parent.gameObject;
                            for (int a = 0; a < oldWrapper1.transform.childCount; a++)
                            {
                                oldWrapper1.transform.GetChild(a).Translate(distanceToNewLocation, Space.World);
                            }
                        }

                            FixedJoint fj = baseConnector.transform.parent.gameObject.AddComponent<FixedJoint>();
					    fj.connectedBody = secondaryConnector.transform.parent.gameObject.GetComponent<Rigidbody>();


                        //move the secondary object and all object in its old wrapper to the new Wrapper.
                        { 
                            GameObject oldWrapper2 = secondaryConnector.transform.parent.parent.gameObject;
                            //move the base object and all object in its old wrapper to the new Wrapper.
                            for (int a = 0; a < oldWrapper2.transform.childCount; a++)
                            {
                                oldWrapper2.transform.GetChild(a).SetParent(newWrapper.transform);
                                a--;
                            }
                            //destroy old wrapper if already connected.
                            if (oldWrapper2 != null)
                            {
                                Destroy(oldWrapper2);
                            }
                        }
                    
                    }


                    //store old wrapper for deletion.
                    GameObject oldWrapper = baseConnector.transform.parent.parent.gameObject;
                    //move the base object and all object in its old wrapper to the new Wrapper.
                    for (int a = 0; a < oldWrapper.transform.childCount; a++)
                    {
                        oldWrapper.transform.GetChild(a).SetParent(newWrapper.transform);
                        a--;
                    }
                    //destroy old wrapper if already connected.
                    if (oldWrapper != null)
                    {
                        Destroy(oldWrapper);
                    }

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