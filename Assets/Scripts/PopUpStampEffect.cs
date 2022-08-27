using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpStampEffect : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine("PopUp");
    }

    private IEnumerator PopUp()
    {
        yield return new WaitForSeconds(0.01f);

        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
        _image.DOFade(0.9f, 0.3f);
        yield return new WaitForSeconds(0.3f);

        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        yield return new WaitForSeconds(1.2f);

        transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.6f);
        _image.DOFade(0.2f, 0.6f);

        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }
}
