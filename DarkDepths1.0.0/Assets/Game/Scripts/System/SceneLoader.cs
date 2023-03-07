using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    [SerializeField]
    private CanvasGroup loadingOverlay;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    [Range(0.01f, 3f)]
    private float fadeTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(PerfomLoadSceneAsync(sceneName));
    }

    private IEnumerator PerfomLoadSceneAsync(string sceneName)
    {
        //fade In
        yield return StartCoroutine(FadeIn());

        var operation = SceneManager.LoadSceneAsync(sceneName);
        while(operation.isDone == false)
        {
            loadingSlider.value = operation.progress;
            yield return null;
        }

        //fade Out
        yield return StartCoroutine(FadeOut());
        loadingSlider.value = 0f;
    }

    private IEnumerator FadeIn()
    {
        float start = 0;
        float end = 1;
        float speed = end - start / fadeTime;

        loadingOverlay.gameObject.SetActive(true);
        loadingOverlay.alpha = start;
        while (loadingOverlay.alpha < end)
        {
            loadingOverlay.alpha += speed * Time.deltaTime;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }
    private IEnumerator FadeOut()
    {
        float start = 1;
        float end = 0;
        float speed = end - start / fadeTime;

        loadingOverlay.alpha = start;
        while (loadingOverlay.alpha > end)
        {
            loadingOverlay.alpha += speed * Time.deltaTime;
            yield return null;
        }
        loadingOverlay.alpha = end;
        loadingOverlay.gameObject.SetActive(false);
    }
}