using UnityEngine;
using UnityEngine.InputSystem;
public class Player_InputController : MonoBehaviour
{
    private CharacterStatHandler statHandler;
    private Rigidbody rigid;

    private float MoveSpeed;
    private float JumpPower;
    private float curObjRotateX;
    private float mouseSensitive;
    private float camMinX;
    private float camMaxX;

    private bool isGrounded;

    Vector3 curMoveInput;
    Vector2 mouseDelta;

    GameObject camContainer;

    void Awake()
    {
        statHandler = GetComponent<CharacterStatHandler>();
        rigid = GetComponent<Rigidbody>();
        camContainer = GameObject.Find("CamaraContainer");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Init();
    }
    void Init()
    {
        MoveSpeed = statHandler.CurMoveSpeed;//참조 statHandler의 현재 수치가 변동되면 적용될것임
        JumpPower = statHandler.CurJumpPower;
        curObjRotateX = 0f;
        mouseSensitive = 0.1f;
        camMinX = -20f;
        camMaxX = 20;
}
    void FixedUpdate()//rigidbody를 이용한 물리연산
    {
        Move();
    }
    private void LateUpdate()//물리연산 뒤에 실행
    {
        Look();
    }

    void Move()//넘어지거나 다른 물체에 충돌시 돌아버린다 rigidbody인스펙터 x,y,z Freeze
    {
        //rigid.velocity = curMoveInput * MoveSpeed;//월드좌표로 이동한다
        //curMoveInput로 얻는 값이 월드기준인가보다

        Vector3 localDiraction = transform.forward * curMoveInput.z + transform.right * curMoveInput.x;

        rigid.AddForce(localDiraction * MoveSpeed, ForceMode.Force);
    }

    
    void Look()
    {
        //오브젝트의상하 = x 마우스상의 상하 = y
        //오브젝트의좌우 = y 마우스상의 좌우 = x

        //transform.eulerAngles += new Vector3(mouseDelta.y, mouseDelta.x, 0f);//훨윈드를 돌아버린다
        //카메라는 y만 캐릭터는 x만 변경되어야한다 캐릭터가 x,y동시에 변경되니 축 변경으로 인해 복합적인 문제가 발생한다


        curObjRotateX += mouseDelta.y * mouseSensitive;
        //최소치, 최대치를 넘어가면 해당값으로 보정
        curObjRotateX = Mathf.Clamp(curObjRotateX, camMinX, camMaxX);
        //                                                   (curObjRotateX, 0, 0);마우스 조작방향과 반대로 시점이 변경된다
        camContainer.transform.localEulerAngles = new Vector3(-curObjRotateX, 0, 0);

        transform.eulerAngles += new Vector3(0f, mouseDelta.x * mouseSensitive, 0f);

    }

    //camCurXRot += mouseDelta.y* lookSensitivity;
    //camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

    //transform.eulerAngles += new Vector3(0, mouseDelta.x* lookSensitivity, 0);

    public void OnMoveInput(InputAction.CallbackContext context)//버튼처럼 연결될 함수
    {
        //context에 무슨 정보가 담겨있는지 다 알수 없다
        //누르기 시작했을때, 누르고 있을때, 땟을때 정도의 활용
        //누르고 있을때
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector3>().normalized;
            //Debug.Log(curMoveInput);//x,z 1,0,-1 나온다 normalized가 없으면 대각선 이동시 1,0,1
        }
    }

    public void OnMouseDelta(InputAction.CallbackContext context)//버튼처럼 연결될 함수
    {
        mouseDelta = context.ReadValue<Vector2>();
        //Debug.Log(mouseDelta);//x,y 마우스 속도에 따라 100가까운 값도 나온다
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && isGrounded)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}

