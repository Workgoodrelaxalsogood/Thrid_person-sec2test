using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
  

{
    CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private  Camera followCamera;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vercialInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0)
            * new Vector3(horizontalInput, 0, vercialInput);

        Vector3 movementDirenction = movementInput.normalized;
        characterController.Move(movementInput * playerSpeed * Time.deltaTime);
        if (movementDirenction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirenction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
