using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���÷��� �� ���콺 ��ġ�� �����Ͽ� ���콺 ���� Ŭ�� �� �����ɽ�Ʈ�� �߻� �� ó��
/// </summary>
public class MouseRaycastManager : MonoBehaviour
{
    bool isActive = false;
    public bool Active
    {
        set => isActive = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            //MouseRaycastManager will be rejected
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Ground" || hit.collider.tag == "Player")
                {

                }
                else
                {
                    Debug.Log(hit.collider.name);

                    if (hit.collider.tag == "NPC")
                    {
                        Debug.Log("NPC�� Ŭ���ϼ̽��ϴ�. �������� ��ȣ�ۿ��� �������ּ���");
                        hit.collider.GetComponent<INPC>().Communicate();
                    }
                }
            }
        }
    }
}
