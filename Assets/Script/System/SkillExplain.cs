using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillExplain : MonoBehaviour
{
    string Up_Button = "<sprite=\"Buttons\" index=0>";
    string Down_Button = "<sprite=\"Buttons\" index=1>";
    string Left_Button = "<sprite=\"Buttons\" index=2>";
    string Right_Button = "<sprite=\"Buttons\" index=3>";
    string Sword_Button = "<sprite=\"Buttons\" index=4>";
    string Gun_Button = "<sprite=\"Buttons\" index=5>";
    string Magic_Button = "<sprite=\"Buttons\" index=6>";
    string Jump_Button = "<sprite=\"Buttons\" index=7>";
    string Summon_Button = "<sprite=\"Buttons\" index=8>";
    string Potion_Button = "<sprite=\"Buttons\" index=9>";


    public static Dictionary<string, string> skillText;
    GameObject skill;

    // Start is called before the first frame update
    void Start()
    {
        skillText = new Dictionary<string, string>();
        IniDictionary();

        skill = GameObject.Find("Canvas").transform.GetChild(0).Find("Skill").gameObject;
    }

    void IniDictionary()
    {
        //剣スキル
        skillText.Add("Hamma", "ナロードヌィ・セリュ\n\n地上で\n" + Sword_Button + "" + Sword_Button + "少し待ち" + Sword_Button + "\n\n\n人民ノ象徴タル鎌ヲ通常攻撃ニ追加ス。鎌ハ農民ノ善キ友也。");
        skillText.Add("Sickle", "ナロードヌィ・モロト\n\n地上で\n" + Sword_Button + "" + Sword_Button + "少し待ち" + Sword_Button + "" + Sword_Button + "\n\n\n人ノの象徴タル槌ヲ通常攻撃ニ追加ス。槌ハ労働者ノ友人也");
        skillText.Add("HighSlash", "ハイスラッシュ\n\n地上、空中で\n" + Up_Button + "+" + Sword_Button + "\n\n\n敵を切り上げ、敵と自分を空中へ誘う。空中攻撃の起点としよう。");
        skillText.Add("ThrowSword", "マークエネミー\n\n地上で\n" + Left_Button + "or" + Right_Button + "+" + Sword_Button + "\n\n\n魔印を付与した剣を前方に投げつけ、剣に触れた敵の目の前に瞬時に移動する。");
        skillText.Add("Iai", "スラッシュインパクト\n\n地上で\n" + Down_Button + "+" + Sword_Button + "\n\n\n刀身に魔力を込めた一撃。その斬撃は漆黒に染まる。");
        skillText.Add("AirRaid", "エアレイド\n\n空中で\n" + Left_Button + "or" + Right_Button + "+" + Sword_Button + "\n\n\n空中で一気に距離を詰めて剣を突き刺す。");
        skillText.Add("Slashing", "溜め斬り\n\n地上で\n" + Sword_Button + "長押し後、離す\n\n\n力を溜めることで強力な斬撃を放つ。");
        //銃スキル
        skillText.Add("Fannel", "追尾弾\n\n地上、空中で\n" + Up_Button + "+" + Gun_Button + "\n\n\n自動で敵を狙う弾を数発召喚、数秒のち発射。地形上当てられない敵は狙わない。");
        skillText.Add("RailGun", "レールガン\n\n地上で\n" + Left_Button + "or" + Right_Button + "+" + Gun_Button + "\n\n\n魔力を極限まで圧縮し、一気に撃ち出す。その速度は光速に等しい。");
        skillText.Add("Midareuti", "パニックバレット\n\n地上で\n" + Down_Button + "+" + Gun_Button + "\n\n\n四方八方囲まれたあなた。そんなときはこれ!!とにかく目についた敵を撃って撃って撃ちまくれ!!");
        skillText.Add("ShotGun", "散弾攻撃\n\n空中で\n" + Left_Button + "or" + Right_Button + "or" + Down_Button + "+" + Gun_Button + "\n\n\n空中ヨリ特殊銃ノ散弾ヲ発射シ、広範囲ニ攻撃ヲセシメル。");
        skillText.Add("MachineGun", "マシンガン\n\n地上で\n" + Gun_Button + "長押し\n\n\n帝国式最新型魔力機関銃。その圧倒的弾幕は誰一人寄せ付けない。ただし、燃費は悪い。");
        //魔法スキル
        skillText.Add("WaterMasic", "ハイドロスクリュー\n\n地上、空中で\n" + Up_Button + "+" + Magic_Button + "\n\n\n多量の水を渦巻き状に放射する。術者が水に濡れることはない。");
        skillText.Add("FireTower", "火柱\n\n地上で\n" + Left_Button + "or" + Right_Button + "+" + Magic_Button + "\n\n\n地面から火柱を生やし、敵を打ち上げる。あつそう。");
        skillText.Add("Tyoson", "人民革命塔\n\n地上で\n" + Down_Button + "+" + Magic_Button + "\n\n\n大地ヨリ北ノ将軍様ノ攻撃方程式ニ基ヅキ革命塔放出ヲ行フ。");
        skillText.Add("Lightning-Strike", "ライトニングストライク\n\n空中で\n" + Down_Button + "+" + Magic_Button + "\n\n\nビリビリする");
        skillText.Add("BlackHole", "ダイソン\n\n地上で\n" + Magic_Button + "長押し\n\n\n吸引力の変わらないただ一つのブラックホール。敵とゲームの不具合を吸い寄せる。");
        //移動スキル
        skillText.Add("ALanding", "エアランディング\n\n空中で\n素早く" + Down_Button + Down_Button + "\n\n\n空中から地面に瞬間移動する");
        skillText.Add("ADrift", "エアドリフト\n\n空中で\n素早く" + Right_Button + Right_Button + "or" + Left_Button + Left_Button + "\n\n\n風魔法の反動で勢いよく距離を詰める");
        //召喚スキル
        skillText.Add("SummonTaka", "サモンホーク\n\n地上、空中で\n" + Up_Button + "+" + Summon_Button + "\n\n\n鷹を召喚する。一定時間2倍の速さで走ることができる");
        skillText.Add("SummonYosei", "サモンフェアリー\n\n地上、空中で\n" + Down_Button + "+" + Summon_Button + "\n\n\n妖精を召喚する。一定時間徐々に回復する");
        skillText.Add("SummonSaboten", "サモンカクタス\n\n地上、空中で\n" + Left_Button + "or" + Right_Button + "+" + Summon_Button + "\n\n\nサボテンを召喚する。陽気な歌で地面からサボテンを急成長させ敵を攻撃");
        skillText.Add("SummonOokami", "サモンウルフ\n\n地上、空中で\n" + Summon_Button + "\n\n\n狼を召喚する。猛烈な突進攻撃で敵を攻撃");
        //特殊スキル
        skillText.Add("Potion", "敵強化ポーション\n\n\n" + Potion_Button + "\n\n\n敵を強化させるが、必ずアイテムを落とすようになるポーションを投げつける。");

    }

    public void SetText(string buttonSelected)
    {
        GetComponent<TextMeshProUGUI>().text = skillText[buttonSelected];
        
        //GetComponent<Text>().text = GameObject.Find("MainSystem").GetComponent<SkillLearned>().GetSkillActive(buttonSelected).ToString();
        //GetComponent<Text>().text = skillText[buttonSelected];
    }
}
