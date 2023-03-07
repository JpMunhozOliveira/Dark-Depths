using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class PanelStart : MonoBehaviour
{
    [SerializeField] 
    Text levelCount;
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    [Range(0.01f, 3f)]
    private float fadeTime = 2f;
    void Start()
    {
        canvasGroup.gameObject.SetActive(true);
        levelCount.text = "Level " + GameManager.Instance.Level + " - " + GameManager.Instance.World;
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        float start = 1;
        float end = 0;
        float speed = end - start / fadeTime;

        canvasGroup.alpha = start;
        while (canvasGroup.alpha > end)
        {
            canvasGroup.alpha += speed * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = end;
        canvasGroup.gameObject.SetActive(false);
    }

}
