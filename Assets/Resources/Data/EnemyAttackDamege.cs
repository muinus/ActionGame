using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "CharacterStatusData_E", menuName = "Data/CharacterStatusData_E")]
    // ↑このメニューから作れるようにする
    public class EnemyAttackDamege : ScriptableObject
    {
        // OneLine と Reorderable 属性はアセット導入時のみ有効
        //[OneLine, Reorderable, Header("Character Settings (Name, Hp, Atk, Def, Element, Image)")]
        public List<AttackDamage_E> AttackDataList = new List<AttackDamage_E>();
    }


    // キャラクターごとのステータス
    [System.Serializable]
    public class AttackDamage_E
    {
        [SerializeField] string EnemyName;      // 敵名前
        public string EName => EnemyName;

        [SerializeField] string ActtionName;      // 攻撃名前
        public string AName => ActtionName;

        [SerializeField] int atk;                   // 攻撃力
        public int Atk => atk;

        [SerializeField] Vector2 force;                   // 攻撃力
        public Vector2 Force => force;

        // [SerializeField] ELEMENTAL elemental;       // 属性（四大元素）
        // public ELEMENTAL Element => elemental;
    }

