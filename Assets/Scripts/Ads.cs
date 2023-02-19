using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

public class Ads : MonoBehaviour
{
    public void ShowVideo()
    {
        VideoAd.Show();
    }

    public void StickOn()
    {
        StickyAd.Show();
    }

    public void StickOff()
    {
        StickyAd.Hide();
    }

    public void ShowInter()
    {
        InterstitialAd.Show();
    }
}