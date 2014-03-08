using UnityEngine;
using System.Collections;

public class ShooterBehaviour : MonoBehaviour {

	private bool safteyCatch;
	public Texture2D textureToDisplay;
	public GameObject gunmodel;
	private GameObject gun;

	private bool activated = false;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void OnGUI() {
		if (safteyCatch)
		{
			if (networkView.isMine)
			{
				GUI.Label(new Rect(10, 40, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (networkView.isMine && activated)
		{
			InputMovement();
		}   
		else
		{
			SyncedMovement();
		}
	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	


	void InputMovement()
	{	
		GetComponent<MouseLook>().enabled = true;
		if (Input.GetMouseButtonDown(0) &! safteyCatch)
		{
			fire();
		}
		if (Input.GetMouseButtonDown(1))
		{
			setSafety();
		}
	}

	void fire()
	{
		print("please fire!");
	}

	[RPC] void setSafety()
	{
		safteyCatch = !safteyCatch;
		gun.SendMessage("setSafety", safteyCatch);
		
		if (networkView.isMine)
		{
			networkView.RPC("setSafety", RPCMode.OthersBuffered);
		}
	}
	
	public bool getSafety()
	{
		return safteyCatch;
	}

	public void activate()
	{
		activated = true;
	}

	public void setGun(GameObject gunOb)
	{
		gun = gunOb;
	}

	public void animateShot()
	{
		gunmodel.animation.Play();
	}
}
