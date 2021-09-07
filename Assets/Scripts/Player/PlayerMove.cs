using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //나중에 정교하게 만들기

    [SerializeField] private float playerSpeed;

    private Rigidbody playerRigid;
    private Animator playerAnimator;
    private Transform cameraArm;
    private Transform playerTrans;

    private void Awake()
    {
        playerTrans = transform.GetChild(0).transform;
        playerRigid = playerTrans.GetComponent<Rigidbody>();
        playerAnimator = playerTrans.GetComponent<Animator>();

        cameraArm = transform.GetChild(1).GetComponent<Transform>();
    }


    private void Update()
    {
        LookAround();
        Move();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        //playerAnimator.SetBool("isWalking", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            playerTrans.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }
}
