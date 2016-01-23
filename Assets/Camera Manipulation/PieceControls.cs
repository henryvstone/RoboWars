using UnityEngine;
using System.Collections;

public class PieceControls : MonoBehaviour {
	
	// Use this for initialization
	public Material assembliesSelected;
    public Material assembliesDeselected;

    public ArrayList assemblies;
	
	ArrayList selectedAssemblies;
	
	// Use this for initialization
	void Start () {
        assemblies = new ArrayList();
        selectedAssemblies = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rh = new RaycastHit ();
			if (Physics.Raycast (ray, out rh)) {
				if (rh.collider.tag.Equals ("Piece")) {
                    //check to make sure piece is a member of a wrapper
                    if (rh.collider.gameObject.transform.parent != null)
                    {
                        GameObject wrapper = rh.collider.gameObject.transform.parent.gameObject;
                        
					    if (selectedAssemblies.Contains (wrapper)) {
                            selectedAssemblies.Remove (wrapper);

                            //apply material to all subobjetcs
                            foreach(Transform child in wrapper.transform)
                            {
                                child.GetComponent<MeshRenderer>().material = assembliesDeselected;
                            }
                        } else {
                            selectedAssemblies.Add (wrapper);

                            //apply material to all subobjetcs
                            foreach (Transform child in wrapper.transform)
                            {
                                child.GetComponent<MeshRenderer>().material = assembliesSelected;
                            }
                        }
                    }
					
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.A)) {
            if(selectedAssemblies.Count > 0)
            {
                clearSelections ();
            }
            else
            {
                fillSelections ();
            }
			
		}

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < selectedAssemblies.Count; i++)
            {
                ((GameObject)selectedAssemblies[i]).transform.Rotate(0, 45, 0);
            }
        }

        if (Input.GetKeyDown (KeyCode.S)) {
			for (int i = 0; i < selectedAssemblies.Count; i++) {
                //apply physics to all subobjetcs
                foreach (Transform child in ((GameObject)(selectedAssemblies[i])).transform)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                }
			}
		}

//        if (Input.GetKeyDown(KeyCode.D))
//        {
//            for (int i = 0; i < selectedAssemblies.Count; i++)
//            {
//                Instantiate((GameObject)selectedAssemblies[i], new Vector3(0,0,0), Quaternion.identity);
//            }
//        }

        //left mo
		if (Input.GetMouseButton (1)) {
			float angle = -GetComponent<CameraRotator>().angle;
			float dist = GetComponent<CameraRotator>().distance;

			float deltaX = Input.mousePosition.x - lastPosition.x;
			for (int i = 0; i < selectedAssemblies.Count; i++) {
                //move all subobjects globally
                foreach (Transform child in ((GameObject)(selectedAssemblies[i])).transform)
                {
                    child.Translate(Mathf.Cos(angle) * deltaX / 1000 * dist, 0, Mathf.Sin(angle) * deltaX / 1000 * dist, Space.World);
                }
            }

			float deltaY = Input.mousePosition.y - lastPosition.y;
			for (int i = 0; i < selectedAssemblies.Count; i++) {
                //move all subobjects globally
                foreach (Transform child in ((GameObject)(selectedAssemblies[i])).transform)
                {
                    child.Translate(0, deltaY / 1000 * dist, 0, Space.World);
                }
            }
		}

		lastPosition = Input.mousePosition;
	}

	Vector2 lastPosition;

    public void clearSelections()
    {
        foreach (GameObject wrapper in selectedAssemblies)
        {
            foreach (Transform child in wrapper.transform)
            {
                child.GetComponent<MeshRenderer>().material = assembliesDeselected;
            }
        }
        selectedAssemblies.Clear();
    }

    public void fillSelections()
    {
        foreach (GameObject wrapper in selectedAssemblies)
        {
            foreach (Transform child in wrapper.transform)
            {
                child.GetComponent<MeshRenderer>().material = assembliesDeselected;
            }
        }
        selectedAssemblies.Clear();
    }

    public void disable(){
		
	}
}
