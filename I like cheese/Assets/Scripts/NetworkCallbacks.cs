using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player;

    public GameObject engineRoom;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {

        Vector3 spawnPos = new Vector3(0, 0, 0);
        if (BoltNetwork.IsServer) {
            BoltNetwork.Instantiate(engineRoom, spawnPos, Quaternion.identity);
        }

        spawnPos.y = 5;

        BoltNetwork.Instantiate(player, spawnPos, Quaternion.identity);
    }
}
