using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라가 항상 마우스의 뒤를 따라가게 함
/// </summary>
public class CharacterChaser : MonoBehaviour
{
    public Transform target;
    [SerializeField] Vector3 distance = new Vector3(0f, 5f, 15f);

    /// <summary>
    /// 타켓의 래퍼런스 확인 > 타겟을 중심으로 목적지 계산 > 현재 위치와 목적지를 보간하여 현재 위치 갱신
    /// </summary>
    void Update()
    {
        if (target != null)
        {
            // 문제: 포지션을 바로 바꾸어 줬을 때, 화면이 끊기는 듯한 느낌을 받음
            // 원인 분석: 포지션을 바로 대입하는 방법은 아무래도 변화값이 일정하지 못하여 발생하는 것으로 보임
            // 대책: Vector3.Lerp를 이용하여 자연스러운 변화값을 만들어 이동시키면 시각적으로 끊김 현상이 적은 것으로 보임
            //transform.position = target.position - target.forward * distance.z + Vector3.up * distance.y;

            Vector3 destination = target.position - target.forward * distance.z + Vector3.up * distance.y;
            transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
            transform.LookAt(target);
        }
    }
}
