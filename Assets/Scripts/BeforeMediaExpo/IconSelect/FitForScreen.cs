using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>UI.Image��Canvas�Ƀt�B�b�g������</summary>
public class FitForScreen : MonoBehaviour
{

    public enum ExpandMethod
    {
        Fill,    // �O�ڂ���
        Fit,     // ���ڂ���
        Stretch, // �L�k������
    }

    [SerializeField] private Image Image = default;
    [SerializeField] public ExpandMethod Method = default;
    private RectTransform CanvasRect;
    private Vector2 LastCanvasSize;
    private RectTransform ImageRect;
    private Vector2 ImageSize;
    private ExpandMethod LastMethod;

    /// <summary>������</summary>
    private void Awake()
    {
        var canvas = GetComponent<Canvas>();
        CanvasRect = canvas?.GetComponent<RectTransform>();
        if (CanvasRect)
        {
            ImageRect = Image.GetComponent<RectTransform>();
            ImageRect.anchorMin = ImageRect.anchorMax = ImageRect.pivot = new Vector2(0.5f, 0.5f);
            ImageRect.localScale = Vector3.one;
            ImageRect.localScale = new Vector3(1f / ImageRect.lossyScale.x, 1f / ImageRect.lossyScale.y, 1f / ImageRect.lossyScale.z) * canvas.scaleFactor;
            ImageRect.rotation = Quaternion.identity;
            ImageRect.localPosition = Vector3.zero;
            var sprite = Image.sprite;
            ImageSize = sprite.bounds.size * sprite.pixelsPerUnit;
            LastMethod = Method;
        }
    }

    /// <summary>�쓮</summary>
    private void Update()
    {
        if (CanvasRect && (LastCanvasSize != CanvasRect.sizeDelta || LastMethod != Method))
        {
            LastCanvasSize = CanvasRect.sizeDelta;
            var narrow = (((float)LastCanvasSize.y / LastCanvasSize.x) <= (ImageSize.y / ImageSize.x));
            if (Method == ExpandMethod.Stretch)
            {
                ImageRect.sizeDelta = LastCanvasSize;
            }
            else
            {
                ImageRect.sizeDelta = ((Method == ExpandMethod.Fill && narrow) || (Method == ExpandMethod.Fit && !narrow)) ?
                    new Vector2(LastCanvasSize.x, ImageSize.y * LastCanvasSize.x / ImageSize.x) :
                    new Vector2(ImageSize.x * LastCanvasSize.y / ImageSize.y, LastCanvasSize.y);
            }
            LastMethod = Method;
        }
    }

}