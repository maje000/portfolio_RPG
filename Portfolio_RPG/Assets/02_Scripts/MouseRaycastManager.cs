using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디스플레이 상 마우스 위치를 기준하여 마우스 왼쪽 클릭 시 레이케스트를 발사 및 처리
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
                        Debug.Log("NPC를 클릭하셨습니다. 앞으로의 상호작용을 개발해주세요");
                        hit.collider.GetComponent<INPC>().Communicate();
                    }
                }
            }
        }
    }
}
