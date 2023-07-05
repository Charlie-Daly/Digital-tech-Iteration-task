using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float P_Wspeed = 12f;
    public float P_Sspeed;
    public float P_TurnSpeed = 180f;
    public float P_UpRoatianSpeed = 100f;

    private Rigidbody P_Rigidbody;
    private float P_MovementForwardInputValue;
    private float P_MovementSidewaysInputValue;

    //public Transform P_Eyes;

    //private Rigidbody P_EyesRigidBody;

    //public Transform EyesPosition;

    private float P_TurnInputValue;
    //private float P_UpRotationInputValue;


   LayerMask m_LayerMask;

    private void Awake()
    {
        P_Rigidbody = GetComponent<Rigidbody>();

        m_LayerMask = LayerMask.GetMask("Ground");

       // P_EyesRigidBody = P_Eyes.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //when the tank is turned on, make sure it is not kinematic
        P_Rigidbody.isKinematic = false;

        //also reset the input values
        P_MovementForwardInputValue = 0f;

        P_MovementSidewaysInputValue = 0f;


        P_TurnInputValue = 0f;
       // P_UpRotationInputValue = 0f;
    }

    private void OnDisable()
    {
        //when player is not moving, set it to kinematic so it stops moving
        P_Rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
       // Cursor.lockState = CursorLockMode.Locked;

        P_MovementForwardInputValue = Input.GetAxis("Vertical");
        P_MovementSidewaysInputValue = Input.GetAxis("Horizontal");
        P_TurnInputValue = Input.GetAxis("Mouse X");
        //P_UpRotationInputValue = Input.GetAxis("Mouse Y");

       // P_EyesRigidBody.position = EyesPosition.transform.position;


        /*
        Vector3 mousePos = Input.mousePosition;

        transform.LookAt(Input.mousePosition, Vector3.forward);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            transform.LookAt(hit.point);
        }
        */
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        //create a vector in the direction the tank is facing with a magnitude
        //based on the input, speed and time between frams
        Vector3 movement = transform.forward * P_MovementForwardInputValue * P_Wspeed * Time.deltaTime;

        Vector3 Sidemovement = transform.right * P_MovementSidewaysInputValue * P_Wspeed * Time.deltaTime;

        //Apply this movement to the rigidbody's position
        P_Rigidbody.MovePosition(P_Rigidbody.position + movement);

        P_Rigidbody.MovePosition(P_Rigidbody.position + Sidemovement);

        
    }

    private void Turn()
    {
        //determine the number of degrees to be turned based on the input,
        //speed and time between frames
        float turn = P_TurnInputValue * P_TurnSpeed * Time.deltaTime;
        //float up = P_UpRotationInputValue * P_UpRoatianSpeed * Time.deltaTime;

        //make this into a rotation in the y axis
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //Quaternion upRotation = Quaternion.Euler(up, 0f, 0f);

        //apply this rotation to the rigidbody's rotation
        P_Rigidbody.MoveRotation(P_Rigidbody.rotation * turnRotation);

        //Vector3 UpMovement = transform.up * up * P_TurnSpeed * Time.deltaTime;

        //P_Rigidbody.MovePosition(P_Rigidbody.position + UpMovement);

        //P_EyesRigidBody.MoveRotation(P_Eyes.rotation * upRotation);
        //P_EyesRigidBody.MoveRotation(P_Eyes.rotation * turnRotation);
    }

    
}
