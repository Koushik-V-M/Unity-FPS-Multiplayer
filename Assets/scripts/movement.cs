using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class movement : MonoBehaviour
{
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private Animator animator = null;
    //[SerializeFeild]
    public AudioSource audio_source;
    //audio_source=GetComponent<AudioSource>();
   /*void start()
    {
        audioSource = GetComponent<AudioSource>();
    }*/
    void Update()
    {
        if (pausemenu.isOn)
        {
            return;
        }
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
        animator.SetBool("isWalking", moveDirection.magnitude>2f);
        if (moveDirection.magnitude > 2f)
        {
            if (!audio_source.isPlaying)
            {
                audio_source.Play(0);
            }
        }
        else
        {
            if (audio_source.isPlaying)
            {
                audio_source.Pause();
            }
        }
    }
}