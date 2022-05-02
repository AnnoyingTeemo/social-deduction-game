using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using System;
using UnityEngine.UI;

public class serverControll : GlobalEventListener
{
    public Text lobbyCode;

    public void startHost() {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()
    {
        if (lobbyCode.text != "") {
            BoltMatchmaking.CreateSession(sessionID: lobbyCode.text, sceneToLoad: "Game");
        }
        else {
            BoltMatchmaking.CreateSession(sessionID: "LobbyCode", sceneToLoad: "Game");
        }
    }

    public void startClient() {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        string sessionID = lobbyCode.text;

        if (sessionID == "") {
            sessionID = "LobbyCode";
        }

        BoltMatchmaking.JoinSession(sessionID);

        //foreach (var session in sessionList) {
        //    UdpSession photonSession = session.Value as UdpSession;
        //    if (photonSession.Source == UdpSessionSource.Photon) {
        //        BoltMatchmaking.JoinSession(photonSession);
        //    }
        //}
    }
}
