using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class TestGunFire : EntityEventListener<IPlayerWeaponState>
{
    [SerializeField]
    GameObject child;

    public override void Attached()
    {
        state.triggerPressed = false;

        if (entity.IsOwner) {

        }
    }

    public override void SimulateOwner()
    {
        if (entity.IsOwner) {
            if (Input.GetMouseButton(0)) {
                state.triggerPressed = true;
            }
            if (Input.GetMouseButtonUp(0)) {
                state.triggerPressed = false;
            }

            if (state.triggerPressed) {
                int layerMask = 1 << 7;

                //layerMask = ~layerMask;

                RaycastHit hit;

                if (Physics.Raycast(child.transform.position, child.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
                    Debug.DrawRay(child.transform.position, child.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    hit.collider.gameObject.GetComponent<PlayerMovement>().takeDamage(1);
                    Debug.Log("Did Hit");
                }
                else {
                    Debug.DrawRay(child.transform.position, child.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
    }


}
