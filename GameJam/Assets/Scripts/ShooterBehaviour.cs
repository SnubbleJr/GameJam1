using UnityEngine;
using System.Collections;

public class ShooterBehaviour : MonoBehaviour {

	private int safteyCatch;
	public Texture2D textureToDisplay;

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

	}

	void OnGUI() {
		if (safteyCatch == 1)
		{
			GUI.Label(new Rect(10, 40, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (networkView.isMine)
		{
			InputMovement();
		}

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
}
