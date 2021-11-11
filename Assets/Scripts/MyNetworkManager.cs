using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("I've connect to this server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"Player� { numPlayers}");

        player.SetColorPlayer(new Color
            (Random.Range(0f, 1f),
             Random.Range(0f, 1f),
             Random.Range(0f, 1f)));

        Debug.Log($"The Player � {numPlayers} has connect");
    }
}
