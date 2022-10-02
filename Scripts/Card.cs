using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    FireRate,
    Accuracy,
    Speed
};

[CreateAssetMenu(fileName = "Card", menuName = "NewScriptableObject/Card")]
public class Card : ScriptableObject
{
    public string m_CardName;
    public string m_StatDesc;
    public Sprite m_CardSprite;

    public StatType m_StatType;
    public float m_StatChange;
}
