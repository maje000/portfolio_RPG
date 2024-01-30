using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� �׻� ���콺�� �ڸ� ���󰡰� ��
/// </summary>
public class CharacterChaser : MonoBehaviour
{
    public Transform target;
    [SerializeField] Vector3 distance = new Vector3(0f, 5f, 15f);

    /// <summary>
    /// Ÿ���� ���۷��� Ȯ�� > Ÿ���� �߽����� ������ ��� > ���� ��ġ�� �������� �����Ͽ� ���� ��ġ ����
    /// </summary>
    void Update()
    {
        if (target != null)
        {
            // ����: �������� �ٷ� �ٲپ� ���� ��, ȭ���� ����� ���� ������ ����
            // ���� �м�: �������� �ٷ� �����ϴ� ����� �ƹ����� ��ȭ���� �������� ���Ͽ� �߻��ϴ� ������ ����
            // ��å: Vector3.Lerp�� �̿��Ͽ� �ڿ������� ��ȭ���� ����� �̵���Ű�� �ð������� ���� ������ ���� ������ ����
            //transform.position = target.position - target.forward * distance.z + Vector3.up * distance.y;

            Vector3 destination = target.position - target.forward * distance.z + Vector3.up * distance.y;
            transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
            transform.LookAt(target);
        }
    }
}
