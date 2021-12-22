using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PlayerMovement : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        float speed = 4f;
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            movement.x += speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement.x -= speed;
        }

        if (movement != Vector3.zero) {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
    }
}
