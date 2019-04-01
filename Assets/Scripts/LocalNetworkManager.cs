//github.com/pauberson/MultiuserVive
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LocalNetworkManager : NetworkManager {

    private CustomNetworkDiscovery netDiscovery;
    public bool runAsServer = true;

	void Start () {

        netDiscovery = GetComponent<CustomNetworkDiscovery>();
        netDiscovery.Initialize();

		if (!runAsServer) {
			SetupClient();
        } else {
            SetupServer();
		}
	}

    void netDiscovery_OnReceivedNetworkBroadcast(string fromAddress, string data)
    {
       
        networkAddress = fromAddress;
        StartClient();
        netDiscovery.StopBroadcast();
    }

	void SetupServer(){
        
        networkAddress = "localhost";
		StartHost();
        netDiscovery.StartAsServer();
	}

	void SetupClient(){

        netDiscovery.OnReceivedNetworkBroadcast += netDiscovery_OnReceivedNetworkBroadcast;
        netDiscovery.StartAsClient();
	}

    override public void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        StopClient();
        enabled = false;
        netDiscovery.enabled = false;
        Invoke("ProcessDisconnect", 0.5f);
    }
    
    void ProcessDisconnect()
    {
		enabled = true;
		netDiscovery.enabled = true;
        netDiscovery.Initialize();
        netDiscovery.StartAsClient();
    }

}
