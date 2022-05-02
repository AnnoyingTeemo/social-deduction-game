using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class OxygenController : Photon.Bolt.EntityBehaviour<IRoomState>
{
    public int maxOxygen;

    //public int playerCount;

    public int oxygenDonateSpeed;

    public List<GameObject> playersInRoom;

    // Start is called before the first frame update
    public void Start() {
        playersInRoom = new List<GameObject>();

        Debug.Log("WHqhdiwh");

        InvokeRepeating("GiveOxygen", 1, 1);
        state.RoomOxygen = maxOxygen;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void GiveOxygen() {
        Debug.Log("AMONG US");
        foreach (GameObject player in playersInRoom) {
            player.GetComponent<PlayerMovement>().GainOxygen(donateOxygen());
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
            playersInRoom.Add(other.gameObject);
        }
    }
}
