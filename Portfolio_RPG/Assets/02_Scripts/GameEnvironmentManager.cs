using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임의 환경 변수를 조정
/// </summary>
public class GameEnvironmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Vector3.up * -35f;
    }
}
