//www.youtube.com/watch?v=eMJATZI0A7c
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BrushController : NetworkBehaviour
{


    public GameObject prefab;
    // public GameObject go;
    private LineRenderer currLine;
    private int numClicks = 0;

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

   public  void proceed_create(Vector3 pos)
    {
        if (isServer)
        {
            RpcCreateLine(pos);
            Debug.Log("Trigger touch 1");
        }
        else
        {
            CmdCreateLine(pos);
            Debug.Log("Trigger touch 2");
        }
    }

    public void proceed_start(Vector3 pos)
    {
        if (isServer)
        {
            RpcStartLine(pos,Color.red);
            Debug.Log("Trigger touch 1");
        }
        else
        {
            CmdStartLine(pos,Color.blue);
            Debug.Log("Trigger touch 2");
        }
    }

    [ClientRpc]
    public void RpcStartLine(Vector3 pos,Color color)
    {
        Debug.Log("po");
        GameObject line = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
        currLine = line.GetComponent<LineRenderer>();
        currLine.material = new Material(Shader.Find("Particles/Additive"));
        currLine.SetColors(color, color);
        currLine.SetWidth(0.1f, 0.1f);
        NetworkServer.Spawn(line);
    }

    [ClientRpc]
    public void RpcCreateLine(Vector3 pos)
    {
        currLine.SetVertexCount(numClicks + 1);
        currLine.SetPosition(numClicks, pos);
        numClicks++;
    }

    [Command]
    public void CmdStartLine(Vector3 pos,Color color)
    {
        Debug.Log("client");
        RpcStartLine(pos,color);
    }

    [Command]
    public void CmdCreateLine(Vector3 pos)
    {
        RpcCreateLine(pos);
    }
}