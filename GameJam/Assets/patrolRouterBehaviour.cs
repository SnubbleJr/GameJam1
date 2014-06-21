using UnityEngine;
using System.Collections;

public class patrolRouterBehaviour : MonoBehaviour {

	public GameObject[] patrolRoutes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform[] getRoute()
	{
		
		int rnd = Random.Range(0, patrolRoutes.Length);

		GameObject chosenRoute =  patrolRoutes[rnd];

		Transform[] asdf = chosenRoute.GetComponentsInChildren<Transform>() as Transform[];

		print (asdf[0]);
		return  asdf;

	}
}
