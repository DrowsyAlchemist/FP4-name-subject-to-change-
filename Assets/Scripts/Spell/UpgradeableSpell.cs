using System;
using UnityEngine;

public class UpgradeableSpell : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private UpgradeableSpellData _data;

    public UpgradeableSpellData Data => _data;
    public int UpgrageLevel => PlayerPrefs.GetInt(_id, 0);

    public event Action Upgraded;

    public void Upgrade()
    {
        PlayerPrefs.SetInt(_id, UpgrageLevel + 1);
        PlayerPrefs.Save();
        Upgraded?.Invoke();
    }
}