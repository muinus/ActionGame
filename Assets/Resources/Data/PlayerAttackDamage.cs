using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using OneLine;                      // インスペクター上でフィールドを一行にまとめて表示するアセット
//using SubjectNerd.Utilities;        // インスペクター上でリストの行を並べ替え可能にするアセット


/// <summary>
/// キャラクターステータスのリスト（ScriptableObject） 
/// </summary>

[CreateAssetMenu(fileName = "CharacterStatusData", menuName = "Data/CharacterStatus")]
// ↑このメニューから作れるようにする
public class PlayerAttackDamage : ScriptableObject
{
    // OneLine と Reorderable 属性はアセット導入時のみ有効
    //[OneLine, Reorderable, Header("Character Settings (Name, Hp, Atk, Def, Element, Image)")]
    public List<AttackDamage> AttackDataList = new List<AttackDamage>();
}


// キャラクターごとのステータス
[System.Serializable]
public class AttackDamage
{
    [SerializeField] string ActtionName;      // 名前
    public string Name => ActtionName;

    [SerializeField] int atk;                   // 攻撃力
    public int Atk => atk;

    [SerializeField] Vector2 force;                   // 攻撃力
    public Vector2 Force => force;

    // [SerializeField] ELEMENTAL elemental;       // 属性（四大元素）
    // public ELEMENTAL Element => elemental;
}


public enum ELEMENTAL   // 属性（四大元素）
{
    FIRE,
    WATER,
    EARTH,
    AIR,
}