using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class GunEquip : EntityEventListener<IPlayerWeaponState>
{

    public GameObject playerCam;

    public GameObject gun;

    public override void Attached()
    {
        if (entity.IsOwner) {
            GameObject newGun = BoltNetwork.Instantiate(gun, playerCam.transform.position, playerCam.transform.rotation);

            newGun.GetComponent<GunGeneric>().camera = playerCam;
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
