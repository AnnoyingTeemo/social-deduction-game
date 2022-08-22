using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class GunGeneric : EntityEventListener<IPlayerWeaponState>
{

    public GameObject camera;

    public override void Attached()
    {
        state.SetTransforms(state.GunTransform, gameObject.transform);

        if (entity.IsOwner) {
            
        }
    }

    public override void SimulateOwner()
    {
        if (entity.IsOwner) {
            gameObject.transform.rotation = camera.transform.rotation;
            gameObject.transform.position = camera.transform.position;
        }
    }
}
