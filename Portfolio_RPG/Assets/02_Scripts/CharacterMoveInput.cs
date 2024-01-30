using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �Է°��� �����Ͽ� ĳ���͸� �̵�
/// </summary>
public class CharacterMoveInput : MonoBehaviour
{
    Rigidbody rigid;

    bool isActive = false;
    public bool Active
    {
        set => isActive = value;
    }
    float moveSpeed = 10f;
    float characterRotatePow = 100f;
    float jumpPow = 10f;
    Vector3 mousePositionLastFrame = Vector3.zero;

    private void Start()
    {
        isActive = true;
        rigid = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Active üũ > ĳ���� ȸ�� > ĳ���� �̵� > ���� üũ
    /// </summary>
    void Update()
    {
        if (!isActive)
        {
            // CharacterMoveInput will be rejected
            return;
        }

        // Apply rotation inputKey
        CharacterRotate();

        // Apply horizontal inputKey
        CharacterHorizontalMove();

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
    /// ���콺 ������ Ŭ���� �����Ͽ� ĳ������ ȸ���� �����Ŵ.
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

    /// <summary>
    /// Ű������ �Է��� üũ�Ͽ� ĳ���͸� �̵���Ŵ
    /// </summary>
    private void CharacterHorizontalMove()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 forwardMoveDistance = transform.forward * moveSpeed * Time.deltaTime;
            transform.position += forwardMoveDistance;
        }

        // Move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 backMoveDistance = transform.forward * moveSpeed * Time.deltaTime;
            transform.position -= backMoveDistance;
        }

        // Move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rightMoveDistance = transform.right * moveSpeed * Time.deltaTime;
            transform.position += rightMoveDistance;
        }

        // Move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 leftMoveDistance = transform.right * moveSpeed * Time.deltaTime;
            transform.position -= leftMoveDistance;
        }
    }
}
