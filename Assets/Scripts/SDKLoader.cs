using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using System;

public class SDKLoader : MonoBehaviour
{
    public event Action SDKInitialized;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        gameObject.SetActive(false);
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        gameObject.SetActive(false);
        SDKInitialized?.Invoke();
    }
}