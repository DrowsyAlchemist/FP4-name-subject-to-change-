using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Spell _spell;

    public void Render(Spell spell)
    {
        _spell = spell;

    }
}
