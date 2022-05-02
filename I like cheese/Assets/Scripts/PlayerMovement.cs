using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using UnityEngine.UI;

public class PlayerMovement : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public Camera playerCamera;

    public float camspeed;

    public float speed;

    public int oxygen;
    public int maxOxygen;

    public Text oxygenText;

    Rigidbody rgbdy;

    // Start is called before the first frame update
    public override void Attached()
    {
        if (entity.IsOwner) {
            oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();

            rgbdy = gameObject.GetComponent<Rigidbody>();
            state.SetTransforms(state.PlayerTransform, gameObject.transform);
            state.PlayerOxygen = oxygen;
            if (entity.IsOwner) {
                playerCamera.gameObject.SetActive(true);
            }

            InvokeRepeating("DrainOxygen", 1, 1);
        }
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (entity.IsOwner) {
            Vector3 movement = Vector3.zero;

            if (Input.GetKey(KeyCode.A)) {
                //movement.x -= speed;
                rgbdy.AddForce(-transform.right * speed);
            }
            if (Input.GetKey(KeyCode.D)) {
                //movement.x += speed;
                rgbdy.AddForce(transform.right * speed);
            }
            if (Input.GetKey(KeyCode.W)) {
                //movement.z += speed;
                rgbdy.AddForce(transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.S)) {
                //movement.z -= speed;
                rgbdy.AddForce(-transform.forward * speed);
            }

            if (movement != Vector3.zero) {
                //transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
            }

            float h = camspeed * Input.GetAxis("Mouse X");
            // float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(0, h, 0);

            //Let everyone know this players oxygen level
            state.PlayerOxygen = oxygen;

            oxygenText.GetComponent<Text>().text = "Oxygen: " + oxygen.ToString();
        }
    }

    public void DrainOxygen() {
        if (entity.IsOwner) {
            oxygen -= 1;

            if (oxygen < 0) {
                oxygen = 0;
            }

            state.PlayerOxygen = oxygen;
        }
    }

    public void GainOxygen(int amount) {
        if (entity.IsOwner) {
            oxygen += amount;
            if (oxygen > maxOxygen) {
                oxygen = maxOxygen;
            }

            state.PlayerOxygen = oxygen;
        }
    }
}
