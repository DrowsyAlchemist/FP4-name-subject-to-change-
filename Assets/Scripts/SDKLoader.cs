using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class SDKLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        gameObject.SetActive(false);
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        gameObject.SetActive(false);
    }
}