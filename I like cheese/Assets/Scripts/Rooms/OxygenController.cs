using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class OxygenController : Photon.Bolt.EntityBehaviour<IRoomState>
{
    public int maxOxygen;
    public int currentOxygen;

    public int playerCount;

    public int oxygenDonateSpeed;

    private GameObject[] playersInRoom;

    // Start is called before the first frame update
    public override void Attached() {
        playersInRoom = new GameObject[playerCount];

        InvokeRepeating("GiveOxygen", 1, 1);
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        
    }

    public void GiveOxygen() {
        foreach (GameObject player in playersInRoom) {
            player.GetComponent<PlayerMovement>().GainOxygen(donateOxygen());
        }
    }

    private int donateOxygen() {
        if (currentOxygen - oxygenDonateSpeed > 0) {
            currentOxygen = currentOxygen - oxygenDonateSpeed;
            return oxygenDonateSpeed;
        }
        else {
            int returnNum = (currentOxygen - oxygenDonateSpeed) * -1;
            currentOxygen = 0;
            return returnNum;
        }
    }
}
