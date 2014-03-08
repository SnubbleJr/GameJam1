using UnityEngine;
using System.Collections;

public class OutComeBehaviour : MonoBehaviour {

	enum choices
	{
		APPLE,
		BANANA,
		CANAPLE,
		tree
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Activate()
	{
		string choice = gameObject.name;
		switch(choice)
		{ 
		case "APPLE":
			break;
		case "BANANA":
			break;
		case "CANAPLE":
			break;
		case "tree":
			break;
		default:
			break;
		}
	}
}
