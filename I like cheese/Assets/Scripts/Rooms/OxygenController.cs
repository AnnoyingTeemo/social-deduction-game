using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class OxygenController : Photon.Bolt.EntityBehaviour<IRoomState>
{
    public int maxOxygen;

    //public int playerCount;

    public int oxygenDonateSpeed;

    public GameObject playerInRoom;

    // Start is called before the first frame update
    public void Start() {
        if (entity.IsOwner) {
            InvokeRepeating("GiveOxygen", 1, 1);
        }
        if (BoltNetwork.IsServer) {
            state.RoomOxygen = maxOxygen;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void GiveOxygen() {
        if (playerInRoom != null) {
            playerInRoom.GetComponent<PlayerMovement>().GainOxygen(donateOxygen());
        }
    }

    private int donateOxygen() {
        if (state.RoomOxygen - oxygenDonateSpeed > 0) {
            state.RoomOxygen = state.RoomOxygen - oxygenDonateSpeed;
            state.RoomOxygen = state.RoomOxygen;
            return oxygenDonateSpeed;
        }
        else {
            int returnNum = (state.RoomOxygen - oxygenDonateSpeed) * -1;
            state.RoomOxygen = 0;
             
            return returnNum;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetComponent<BoltEntity>().IsOwner) {
                playerInRoom = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetComponent<BoltEntity>().IsOwner) {
                playerInRoom = null;
            }
        }
    }
}
