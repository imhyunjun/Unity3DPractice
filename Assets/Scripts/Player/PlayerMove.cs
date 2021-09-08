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

    StateMachine sm =;

    private void Awake()
    {
        playerTrans = transform.GetChild(0).transform;
        playerRigid = playerTrans.GetComponent<Rigidbody>();
        playerAnimator = playerTrans.GetComponent<Animator>();

        cameraArm = transform.GetChild(1).GetComponent<Transform>();
    }

    private void Start()
    {
        IPlayerState idleState = new Idle();
        IPlayerState walkState = new Walk();

        sm = new StateMachine(idleState);
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

/* 상태 패턴 */
public enum EState { E_IDLE, E_Walk }
public interface IPlayerState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
}

public class StateMachine
{
    //현재 상태
    public IPlayerState currState { get; private set; }

    public StateMachine(IPlayerState _defaultState)
    {
        currState = _defaultState;
    }

    public void SetState(IPlayerState _state)
    {
        //같은 상태일 경우엔 리턴
        if (currState == _state) return;

        currState.OnExit();
        currState = _state;
        currState.OnEnter();
    }

    public void OnUpdate()
    {
        currState.OnUpdate();
    }
}

public class Idle : IPlayerState
{
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}

public class Walk : IPlayerState
{
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}

