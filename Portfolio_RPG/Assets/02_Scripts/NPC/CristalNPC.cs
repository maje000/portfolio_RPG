using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalNPC : MonoBehaviour, INPC
{
    public void Communicate()
    {
        // UIManager�� ���� Dialog Ȱ��ȭ
        Debug.Log("CristalNPC���� ������ �����մϴ�.");
        UIManager.Instance.OpenDialog(new string[] { "Hello", "World", "pretty", "good", "feature" });
    }
}
