using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� �׻� ���콺�� �ڸ� ���󰡰� ��
/// </summary>
public class CharacterChaser : MonoBehaviour
{
    public Transform _target;
    [SerializeField] Vector3 distance = new Vector3(0f, 5f, 15f);
    [SerializeField] float fixedAngle = 30f;

    /// <summary>
    /// Ÿ���� ���۷��� Ȯ�� > Ÿ���� �߽����� ������ ��� > ���� ��ġ�� �������� �����Ͽ� ���� ��ġ ����
    /// </summary>
    void Update()
    {
        if (_target != null)
        {
            FollowCharacterTopView(_target);
        }
    }

    /// <summary>
    /// ĳ������ ������� �ٶ󺸸� ����
    /// </summary>
    private void FollowCharacterBackHead(Transform target)
    {
        // ����: �������� �ٷ� �ٲپ� ���� ��, ȭ���� ����� ���� ������ ����
        // ���� �м�: �������� �ٷ� �����ϴ� ����� �ƹ����� ��ȭ���� �������� ���Ͽ� �߻��ϴ� ������ ����
        // ��å: Vector3.Lerp�� �̿��Ͽ� �ڿ������� ��ȭ���� ����� �̵���Ű�� �ð������� ���� ������ ���� ������ ����
        //transform.position = target.position - target.forward * distance.z + Vector3.up * distance.y;

        Vector3 destination = target.position - target.forward * distance.z + Vector3.up * distance.y;
        transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
        transform.LookAt(target);
    }

    /// <summary>
    /// ĳ������ ������ ĳ���͸� �ٶ�
    /// </summary>
    /// <param name="target"></param>
    private void FollowCharacterTopView(Transform target)
    {
        /*
         ������ ȭ�鿡�� ĳ������ ��ǥ�� ����
        �׷��� �̰� �Ÿ� + ������ �����ϸ� �ɵ�?
        ĳ������ ������ + �̰� �Ÿ� = ��ǥ
        x�� ȸ���� ���ؼ� ĳ���� �ٶ󺸱�
         */

        Vector3 destination = target.position + Vector3.back * distance.z + Vector3.up * distance.y;
        transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
        transform.rotation = Quaternion.Euler(fixedAngle, 0f, 0f);
    }
}
