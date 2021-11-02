using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highJump : MonoBehaviour
{
    GameObject player;
    public float upForce = 100f;
    private Vector3 velVector = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        print(upForce);
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            CharacterController controller = player.GetComponent<CharacterController>();
            velVector = new Vector3(0, 1, 0);
            velVector = velVector * upForce;

            controller.Move(velVector * Time.deltaTime);
        }
    }
}
