using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour {

	public GameObject bullet;

	private int safteyCatch;
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
		shooter = GameObject.FindGameObjectWithTag("shooter");
		transform.parent = shooter.transform.Find("gunModel");
		activateShooter();
	}
	
	void OnGUI() {

	}
	
	// Update is called once per frame
	void Update () {

	//safteyCatch = shooter.GetComponent<ShooterBehaviour>().getSafety(); 
		if (networkView.isMine)
		{
			transform.localPosition = Vector3.zero;
			InputMovement();
		}
		
	}
	
	void InputMovement()
	{	
		if (Input.GetMouseButtonDown(0) && safteyCatch == 0)
		{
			fire(Input.mousePosition);
		}

	}
	
	void fire(Vector3 position)
	{
		if (safteyCatch == 0)
		{
			print("BANG!");

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

	void sendShot(GameObject victim)
	{
		victim.SendMessage("Hit");

	}

	[RPC] void activateShooter()
	{
		shooter.SendMessage("activate", true);

		if (networkView.isMine)
		{
			networkView.RPC("activateShooter", RPCMode.OthersBuffered);
		}

	}
	
}
