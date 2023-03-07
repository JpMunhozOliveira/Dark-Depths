using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public static class MenuControllerKeys
{
    public const string IDlocale = "LocaleKey";
}

public class MenuController : MonoBehaviour
{
    [Header("SoundMenu")]
    public Slider musicValue;
    public Slider soundValue;

    private void Start()
    {
        //int IDlocale = PlayerPrefs.GetInt(MenuControllerKeys.IDlocale);
        //ChangeLocale(IDlocale);
    }

    private bool activeLocale = false;
    /*public void ChangeLocale(int localeID)
    {
        if (activeLocale)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int _localeID)
    {
        activeLocale = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale=LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt(MenuControllerKeys.IDlocale, _localeID);
        activeLocale = false;
    }*/

    public void GoToScene(string scene)
    {
        SceneLoader.Instance.LoadSceneAsync(scene);
    }

    public void PlaySoundMenu(AudioClip clip)
    {
        AudioController.Instance.PlaySound(clip);
    }

    public void NewGameReset()
    {
        GameManager.Instance.ResetInfo();
    }
}
