using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalNPC : MonoBehaviour, INPC
{
    public void Communicate()
    {
        // UIManager를 통한 Dialog 활성화
        Debug.Log("CristalNPC와의 소통을 시작합니다.");
        UIManager.Instance.OpenDialog(new string[] { "Hello", "World", "pretty", "good", "feature" });
    }
}
