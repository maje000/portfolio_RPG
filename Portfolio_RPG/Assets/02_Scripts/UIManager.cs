using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance = null;
    public UIDialog uiDialog;
    public MouseRaycastManager mouseRaycastManager;
    public CharacterMoveInput characterMoveInput;
    Coroutine coDialogHandle;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject uiManagerGameObject = new GameObject("UIManager");
                    instance = uiManagerGameObject.AddComponent<UIManager>();
                }
            }


            return instance;
        }
    }

    private bool InputActive
    {
        set
        {
            mouseRaycastManager.Active = value;
            characterMoveInput.Active = value;
        }
    }

    public void OpenDialog(string[] scripts)
    {
        int scriptCount = scripts.Length;

        if (scriptCount > 0 && coDialogHandle == null)
        {
            coDialogHandle = StartCoroutine(coDialogProcess(scripts));
        }
    }

    IEnumerator coDialogProcess(string[] scripts)
    {
        InputActive = false;
        uiDialog.Show();
        for (int i = 0; i < scripts.Length; i++)
        {
            Debug.Log(scripts[i]);
            uiDialog.Text = scripts[i];
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForEndOfFrame();
        }

        InputActive = true;
        uiDialog.Hide();

        yield return coDialogHandle = null;
    }
}
