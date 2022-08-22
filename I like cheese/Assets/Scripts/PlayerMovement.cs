using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using UnityEngine.UI;

public class PlayerMovement : EntityEventListener<IPlayerState>
{
    public Camera playerCamera;

    public float camspeed;

    public float speed;

    public int oxygen;
    public int maxOxygen;

    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxHealth = 100;

    public Text oxygenText;

    Rigidbody rgbdy;

    public List<Vector3> spawnLocations = new List<Vector3>();

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform, gameObject.transform);

        currentHealth = maxHealth;

        if (entity.IsOwner) {
            state.PlayerCurrentHealth = currentHealth;
            state.PlayerMaxHealth = maxHealth;
            oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();

            rgbdy = gameObject.GetComponent<Rigidbody>();
            
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
        currentHealth = state.PlayerCurrentHealth;

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

            oxygenText.GetComponent<Text>().text = "Oxygen: " + state.PlayerOxygen.ToString();


            if (Input.GetKeyDown(KeyCode.E)) {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2)) {
                    if (hit.collider.gameObject.tag == "StartButton") {
                        var startEvent = GameStart.Create(GlobalTargets.Everyone);
                        startEvent.Send();
                    }
                }
            }
        }
    }

    //public override void OnEvent(GameStart evnt)
    //{
    //    Debug.Log(evnt.String);

    //    int randomLocation = Random.Range(0, spawnLocations.Count);
    //    gameObject.transform.position = spawnLocations[randomLocation];
    //}

    public void DrainOxygen() {
        if (entity.IsOwner) {
            state.PlayerOxygen -= 1;

            if (state.PlayerOxygen < 0) {
                state.PlayerOxygen = 0;
            }
        }
    }

    public void GainOxygen(int amount) {
        state.PlayerOxygen = state.PlayerOxygen;

        state.PlayerOxygen += amount;
        if (state.PlayerOxygen > maxOxygen) {
            state.PlayerOxygen = maxOxygen;
        }
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth < 0) {
            currentHealth = 0;
        }

        state.PlayerCurrentHealth = currentHealth;
    }
}
