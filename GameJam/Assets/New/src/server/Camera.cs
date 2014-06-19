using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Network.isClient)
        {
            enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
