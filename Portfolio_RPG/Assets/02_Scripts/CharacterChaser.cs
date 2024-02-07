using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라가 항상 마우스의 뒤를 따라가게 함
/// </summary>
public class CharacterChaser : MonoBehaviour
{
    public Transform _target;
    [SerializeField] Vector3 distance = new Vector3(0f, 5f, 15f);
    [SerializeField] float fixedAngle = 30f;

    /// <summary>
    /// 타켓의 래퍼런스 확인 > 타겟을 중심으로 목적지 계산 > 현재 위치와 목적지를 보간하여 현재 위치 갱신
    /// </summary>
    void Update()
    {
        if (_target != null)
        {
            FollowCharacterTopView(_target);
        }
    }

    /// <summary>
    /// 캐릭터의 뒤통수를 바라보며 따라감
    /// </summary>
    private void FollowCharacterBackHead(Transform target)
    {
        // 문제: 포지션을 바로 바꾸어 줬을 때, 화면이 끊기는 듯한 느낌을 받음
        // 원인 분석: 포지션을 바로 대입하는 방법은 아무래도 변화값이 일정하지 못하여 발생하는 것으로 보임
        // 대책: Vector3.Lerp를 이용하여 자연스러운 변화값을 만들어 이동시키면 시각적으로 끊김 현상이 적은 것으로 보임
        //transform.position = target.position - target.forward * distance.z + Vector3.up * distance.y;

        Vector3 destination = target.position - target.forward * distance.z + Vector3.up * distance.y;
        transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
        transform.LookAt(target);
    }

    /// <summary>
    /// 캐릭터의 위에서 캐릭터를 바라봄
    /// </summary>
    /// <param name="target"></param>
    private void FollowCharacterTopView(Transform target)
    {
        /*
         고정된 화면에서 캐릭터의 좌표만 따라감
        그러면 이격 거리 + 각도만 조정하면 될듯?
        캐릭터의 포지션 + 이격 거리 = 좌표
        x축 회전을 통해서 캐릭터 바라보기
         */

        Vector3 destination = target.position + Vector3.back * distance.z + Vector3.up * distance.y;
        transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
        transform.rotation = Quaternion.Euler(fixedAngle, 0f, 0f);
    }
}
