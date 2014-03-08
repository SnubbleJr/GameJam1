using UnityEngine;
using System.Collections;

public class ShooterBehaviour : MonoBehaviour {

	private int safteyCatch;
	public Texture2D textureToDisplay;

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
		if (safteyCatch == 1)
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
		if (Input.GetMouseButtonDown(0) && safteyCatch == 0)
		{
			fire();
		}
		if (Input.GetMouseButtonDown(1))
		{
			setSafety(++safteyCatch%2);
		}
	}

	void fire()
	{
		print("please fire!");
	}

	[RPC] void setSafety(int safteyValue)
	{
		safteyCatch = safteyValue;
		
		if (networkView.isMine)
			networkView.RPC("setSafety", RPCMode.OthersBuffered, safteyCatch);
	}
	
	public int getSafety()
	{
		return safteyCatch;
	}

	public void activate(bool activation)
	{
		print("trying to act");
		activated = activation;
	}
}
