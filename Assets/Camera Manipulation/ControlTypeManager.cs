using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlTypeManager : MonoBehaviour {

	//0 - connector controls
	//1 - piece controls
	public int controlType = 0;
	private int maxControls = 2;

	public GameObject controlName;
	private string[] controlNames = {"Connector Mode", "Piece Mode"};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			controlType += 1;
			if(controlType == maxControls){
				controlType = 0;
			}

			if(controlType == 0){
				GetComponent<ConnectorControls>().enabled = true;
				GetComponent<PieceControls>().clearSelections();
				GetComponent<PieceControls>().enabled = false;
				GetComponent<ConnectorControls>().start();
			}else if(controlType == 1){
				GetComponent<ConnectorControls>().stop();
				GetComponent<ConnectorControls>().enabled = false;
				GetComponent<PieceControls>().enabled = true;
			}

			controlName.GetComponent<Text>().text = controlNames[controlType];
		}
	}
}
