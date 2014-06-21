using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine){

			this.enabled = true;
			
		}
		
		else{
			
			this.enabled = false;
			
		}
	}
}
