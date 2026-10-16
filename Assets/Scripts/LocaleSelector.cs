using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocaleSelector : MonoBehaviour
{
    private bool isInitialized = false;

    private void Start()
    {
        // Espera a que se inicialice el sistema de localización
        LocalizationSettings.InitializationOperation.Completed += OnLocalizationInitialized;
    }

    private void OnLocalizationInitialized(AsyncOperationHandle<LocalizationSettings> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
            isInitialized = true;
        else
            Debug.LogError("Localization initialization failed.");
    }

    public void SetEnglish()
    {
        SetLocale("en");
    }

    public void SetSpanish()
    {
        SetLocale("es");
    }

    private void SetLocale(string localeCode)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Localization is not initialized yet.");
            return;
        }

        // Busca el idioma solicitado y lo establece como idioma activo
        List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (Locale locale in locales)
        {
            if (locale.Identifier.Code == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log("Language changed to: " + localeCode);
                return;
            }
        }

        Debug.LogWarning("Locale not found: " + localeCode);
    }

    private void OnDestroy()
    {
        LocalizationSettings.InitializationOperation.Completed -= OnLocalizationInitialized;
    }
}