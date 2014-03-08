using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit ()
	{
		OutCome();
	}

	[RPC] void OutCome()
	{
		gameObject.SendMessage("Activate");

		if (networkView.isMine)
		{
			networkView.RPC("OutCome", RPCMode.OthersBuffered);
		}
	}

}
