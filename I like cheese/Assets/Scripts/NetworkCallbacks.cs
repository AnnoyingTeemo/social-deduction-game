using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {

        Vector3 spawnPos = new Vector3(0, 0, 0);

        BoltNetwork.Instantiate(player, spawnPos, Quaternion.identity);

    }
}
