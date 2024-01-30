using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDialog : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroupHandle;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI text;

    public string Text
    {
        set => text.text = value;
    }

    public void Show()
    {
        canvasGroupHandle.alpha = 1f;
        canvasGroupHandle.interactable = true;
        canvasGroupHandle.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroupHandle.alpha = 0f;
        canvasGroupHandle.interactable = false;
        canvasGroupHandle.blocksRaycasts = false;
        text.text = null;
    }
}
