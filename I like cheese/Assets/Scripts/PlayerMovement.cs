using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PlayerMovement : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public Camera playerCamera;

    public float camspeed;

    public float speed;

    Rigidbody rgbdy;

    // Start is called before the first frame update
    public override void Attached()
    {
        rgbdy = gameObject.GetComponent<Rigidbody>();
        state.SetTransforms(state.PlayerTransform, gameObject.transform);
        if (entity.IsOwner) {
            playerCamera.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            //movement.x -= speed;
            rgbdy.velocity = -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            //movement.x += speed;
            rgbdy.velocity = transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W)) {
            //movement.z += speed;
            rgbdy.velocity = transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            //movement.z -= speed;
            rgbdy.velocity = -transform.forward * speed * Time.deltaTime;
        }

        if (movement != Vector3.zero) {
            //transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }

        float h = camspeed * Input.GetAxis("Mouse X");
        // float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);
    }
}
