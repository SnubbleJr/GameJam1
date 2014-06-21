using UnityEngine;
using System.Collections;

public class OutComeBehaviour : MonoBehaviour {

	enum choices
	{
		wheel,
		car,
		man,
		cat,
		bird,
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
		case "wheel":
			wheel ();
			break;
		case "car":
			car ();
			break;
		case "man":
			man ();
			break;
		case "cat":
			cat ();
			break;
		case "bird":
			bird ();
			break;
		case "tree":
			tree ();
			break;
		default:
			break;
		}
	}
	
	void wheel()
	{
		print ("wheel");
		Destroy(gameObject);
	}
	void car()
	{
	}
	void man()
	{
	}
	void cat()
	{
	}
	void bird()
	{
	}
	void tree()
	{
	}
}
