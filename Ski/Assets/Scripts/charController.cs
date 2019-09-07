using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;
    [SerializeField]
    private Vector3 inputVector;
    [SerializeField]
    private bool isGrounded;
    private float distanceToGround;

    public float landingRay;
    

    public float spinTorque;
    public float flipTorque;
    public float jumpHeight;
    public GameObject ground;

    public GameObject groundCheckFL;
    public GameObject groundCheckBL;
    public GameObject groundCheckFR;
    public GameObject groundCheckBR;

    public float speed = 50f;
    public float turnSpeed = 35f;


    void Start()
    {
        // get rigidbody component from current game object
        playerBody = GetComponent<Rigidbody>();
    }

    bool IsGrounded()
    {

        RaycastHit hit;
        if (Physics.Raycast(groundCheckFR.transform.position, groundCheckFR.transform.TransformDirection(-Vector3.up), out hit, landingRay)
            || Physics.Raycast(groundCheckBR.transform.position, groundCheckBR.transform.TransformDirection(-Vector3.up), out hit, landingRay)
            || Physics.Raycast(groundCheckFL.transform.position, groundCheckFL.transform.TransformDirection(-Vector3.up), out hit, landingRay)
            || Physics.Raycast(groundCheckBL.transform.position, groundCheckBL.transform.TransformDirection(-Vector3.up), out hit, landingRay)
            )
        {
            Debug.DrawRay(groundCheckFR.transform.position, groundCheckFR.transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.DrawRay(groundCheckBR.transform.position, groundCheckBR.transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.DrawRay(groundCheckBL.transform.position, groundCheckBL.transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.DrawRay(groundCheckFL.transform.position, groundCheckFL.transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Grounded");
            return true;
        }
        else
        {
            Debug.DrawRay(groundCheckFR.transform.position, groundCheckFR.transform.TransformDirection(-Vector3.up) * 10, Color.white);
            Debug.DrawRay(groundCheckBR.transform.position, groundCheckBR.transform.TransformDirection(-Vector3.up) * 10, Color.white);
            Debug.DrawRay(groundCheckBL.transform.position, groundCheckBL.transform.TransformDirection(-Vector3.up) * 10, Color.white);
            Debug.DrawRay(groundCheckFL.transform.position, groundCheckFL.transform.TransformDirection(-Vector3.up) * 10, Color.white);
            //Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 10, Color.white);
            Debug.Log("Not Grounded");
            return false;
        }
    }

    void FixedUpdate()
    {
        if (!IsGrounded())
        {
            playerBody.constraints = RigidbodyConstraints.None;
            // flips and spins and shit
            float spin = Input.GetAxisRaw("Horizontal") * 50f * Time.deltaTime;
           // if (Input.GetAxisRaw("Horizontal") != 0)
           // {
                playerBody.AddTorque(transform.up * spinTorque * spin);
           // }
            
            float flip = Input.GetAxisRaw("Vertical") * 100f * Time.deltaTime;
           // if (Input.GetAxisRaw("Vertical") != 0)
           // {
                playerBody.AddTorque(transform.right * flipTorque * flip);
           // }
            
        }
        else
        {
            // what
            transform.localRotation = new Quaternion(0, 0, 0, 0);

            // checks if player went through the ground ( does this even work?)
            if (playerBody.transform.position.y < ground.transform.position.y)
            {
                playerBody.transform.position = new Vector3(playerBody.transform.position.x, ground.transform.position.y + 5.0f, playerBody.transform.position.z);
            }
            // set constraints
            playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                // movement
                inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * turnSpeed, playerBody.velocity.y, //Input.GetAxisRaw("Vertical") * 
                speed);
                

                //rotation


            }
            playerBody.velocity = inputVector;

            //playerBody

            ResetPlayer();

            // player jump
            if (Input.GetKeyDown("space") && IsGrounded())
            {
                playerBody.AddForce(ground.transform.up * jumpHeight);
                Debug.Log("jump");
            }
        }
    }

    void ResetPlayer()
    {
        // reset rotation
        playerBody.rotation = ground.transform.rotation;
    }
}
