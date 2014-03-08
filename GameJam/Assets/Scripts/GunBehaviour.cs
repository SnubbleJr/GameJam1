using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour {

	public GameObject bullet;

	private bool safetyCatch;
	private GameObject shooter;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			rigidbody.position = syncPosition;
		}
	}
	
	// Use this for initialization
	void Start () {
		findShooter();

	}


	void findShooter()
	{
		shooter = GameObject.FindGameObjectWithTag("shooter");
		transform.parent = shooter.transform.Find("gunModel");
		if (shooter != null)
		{
			activateShooter();
		}
		else{
			findShooter();
		}
	}
	void OnGUI() {

	}
	
	// Update is called once per frame
	void Update () {

		if (networkView.isMine)
		{
			transform.localPosition = Vector3.forward;
			InputMovement();
		}
		
	}
	
	void InputMovement()
	{	
		if (Input.GetMouseButtonDown(0) &! safetyCatch)
		{
			fire(Input.mousePosition);
		}

	}
	
	void fire(Vector3 position)
	{
		if (!safetyCatch)
		{
			cosmeticFire ();

			Ray ray = camera.ScreenPointToRay(position);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.CompareTag("target"))
				{
					sendShot(hit.collider.gameObject);
				}
			}
		}
	}

	[RPC] void cosmeticFire()
	{
		print ("BANG!");
		//audio.Play;
		shooter.SendMessage("animateShot");

		if (networkView.isMine)
		{
			networkView.RPC("cosmeticFire", RPCMode.OthersBuffered);
		}
		
	}

	void sendShot(GameObject victim)
	{
		victim.SendMessage("Hit");

	}

	[RPC] void activateShooter()
	{
		shooter.SendMessage("activate");
		shooter.SendMessage("setGun", gameObject);

		if (networkView.isMine)
		{
			networkView.RPC("activateShooter", RPCMode.OthersBuffered);
		}

	}

	public void setSafety()
	{
		safetyCatch = !safetyCatch;
	}
	
}
