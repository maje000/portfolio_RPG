using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ȯ�� ������ ����
/// </summary>
public class GameEnvironmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Vector3.up * -35f;
    }
}
