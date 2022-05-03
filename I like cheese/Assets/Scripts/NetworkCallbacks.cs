using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player;

    public GameObject lobby;

    public GameObject engineRoom;

    public GameObject weaponsRoom;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        if (BoltNetwork.IsServer) {
            BoltNetwork.Instantiate(engineRoom, spawnPos, Quaternion.identity);

            spawnPos = new Vector3(0, 0, (float)32.49);

            BoltNetwork.Instantiate(weaponsRoom, spawnPos, Quaternion.identity);

            spawnPos = new Vector3(-20, 0, -20);

            BoltNetwork.Instantiate(lobby, spawnPos, Quaternion.identity);

        }
        int minx = -24;
        int maxx = -16;

        int x = Random.Range(minx, maxx + 1);
        int miny = -24;
        int maxy = -16;
        int y = Random.Range(miny, maxy + 1);

        spawnPos = new Vector3(x, 5, y);

        GameObject playerTemp = BoltNetwork.Instantiate(player, spawnPos, Quaternion.identity);
    }
}
