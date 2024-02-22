using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 유저의 입력값에 반응하여 캐릭터를 이동
/// </summary>
public class CharacterMoveInput : NetworkBehaviour
{
    Rigidbody rigid;
    Camera mainCam;

    public TextMeshPro playerNameText;
    public GameObject floatingInfo;

    private SceneScript sceneScript;

    private void Awake()
    {
        sceneScript = FindObjectOfType<SceneScript>();
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
            sceneScript.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
    }

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    public override void OnStartLocalPlayer()
    {
        sceneScript.playerScript = this;
        string name = "player" + Random.Range(100, 999);
        CmdSetupPlayer(name);
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        playerName = _name;
        sceneScript.statusText = $"{playerName} joined.";
    }

    bool isActive = false;
    public bool Active
    {
        set => isActive = value;
    }
    float moveSpeed = 10f;
    float characterRotatePow = 100f;
    float jumpPow = 10f;
    Vector3 mousePositionLastFrame = Vector3.zero;

    Vector2 horizontal_InputDirection;
    Vector3 characterMoveDirection;

    private void Start()
    {
        isActive = true;
        rigid = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    /// <summary>
    /// Active 체크 > 캐릭터 회전 > 캐릭터 이동 > 점프 체크
    /// </summary>
    void Update()
    {
        if (!isActive || !isLocalPlayer)
        {
            // CharacterMoveInput will be rejected
            return;
        }

        // Apply rotation inputKey
        //CharacterRotate();

        // Apply horizontal inputKey
        CharacterHorizontalMove();

        CharacterRotateToMoveDirection();

        // Apply JumpKey
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Ground check
            Physics.Raycast(new Ray(transform.position + Vector3.up * 0.1f, Vector3.down), out RaycastHit hit, 0.2f);
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                rigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// 마우스 오른쪽 클릭에 반응하여 캐릭터의 회전을 적용시킴.
    /// </summary>
    private void CharacterRotate()
    {
        // Check mouse button down and up for get character rotate start time and end time
        if (Input.GetMouseButtonDown(1))
        {
            mousePositionLastFrame = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mousePositionLastFrame = Vector3.zero;
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.mousePosition != mousePositionLastFrame)
            {
                Vector3 mouseDeltaPosition = Input.mousePosition - mousePositionLastFrame;
                // mouse right move > increase x == rotate character Y clockwise axis
                // mouse left move > decrease x == rotate character Y unclockwise axis
                // mouse up move > increase y == dont need yet
                // mouse down move > decrease y == dont need yet

                float yAxisRotateValue = mouseDeltaPosition.x * characterRotatePow * Time.deltaTime;
                transform.Rotate(new Vector3(0f, yAxisRotateValue, 0f), Space.World);
            }

            // after rotate is applied, update mouse position last frame;
            mousePositionLastFrame = Input.mousePosition;
        }
    }

    private void CharacterRotateToMoveDirection()
    {
        if (characterMoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(characterMoveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
    }

    /// <summary>
    /// 키보드의 입력을 체크하여 캐릭터를 이동시킴
    /// </summary>
    private void CharacterHorizontalMove()
    {
        horizontal_InputDirection = Vector2.zero;
        // Move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            horizontal_InputDirection.y++;
        }

        // Move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            horizontal_InputDirection.y--;
        }

        // Move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal_InputDirection.x++;
        }

        // Move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal_InputDirection.x--;
        }

        Vector3 camForwradDirection = mainCam.transform.forward;
        camForwradDirection.y = 0;
        Vector3 forwardMoveDirection = horizontal_InputDirection.y * camForwradDirection;

        Vector3 camRightDirection = mainCam.transform.right;
        camRightDirection.y = 0;
        Vector3 rightMoveDirection = horizontal_InputDirection.x * camRightDirection;

        characterMoveDirection = forwardMoveDirection + rightMoveDirection;
        Vector3 moveDistance = characterMoveDirection.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveDistance;
    }
}