using UnityEngine;
using gui = UnityEngine.GUILayout;

public class GameMenu : MonoBehaviour
{
    public GameObject ShooterPrefab, GunPrefab;
    string ip = "127.0.0.1";


	public void CreatePlayer(GameObject prefab)
	{
		connected = true;
		var g = (GameObject)Network.Instantiate(prefab, transform.position, transform.rotation, 1);
		g.camera.enabled = true;
		camera.enabled = false;
	}  
    void OnDisconnectedFromServer()
    {
        connected = false;
    }
    void OnPlayerDisconnected(NetworkPlayer pl)
    {
        Network.DestroyPlayerObjects(pl);
    }
    void OnConnectedToServer()
    {
        CreatePlayer(GunPrefab);
    }
    void OnServerInitialized()
    {
        CreatePlayer(ShooterPrefab);
    }
    bool connected;
    void OnGUI()
    {
        if (!connected)
        {
            ip = gui.TextField(ip);
            if (gui.Button("connect"))
            {
                Network.Connect(ip, 5300);
            }
            if (gui.Button("host"))
            {
                Network.InitializeServer(3, 5300, false);
            }
        }
    }
}