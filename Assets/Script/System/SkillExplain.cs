using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillExplain : MonoBehaviour
{
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
        skillText.Add("Hamma", "Hamma");
        skillText.Add("Sickle", "Sickle");
        skillText.Add("HighSlash", "HighSlash");
        skillText.Add("ThrowSword", "ThrowSword");
        skillText.Add("Iai", "Iai");
        skillText.Add("AirRaid", "AirRaid");
        skillText.Add("Slashing", "Slashing");
        //銃スキル
        skillText.Add("Fannel", "Fannel");
        skillText.Add("RailGun", "RailGun");
        skillText.Add("Midareuti", "Midareuti");
        skillText.Add("ShotGun", "ShotGun");
        skillText.Add("MachineGun", "MachineGun");
        //魔法スキル
        skillText.Add("WaterMasic", "WaterMasic");
        skillText.Add("FireTower", "FireTower");
        skillText.Add("Tyoson", "Tyoson");
        skillText.Add("Lightning-Strike", "Lightning-Strike");
        skillText.Add("BlackHole", "BlackHole");
        //移動スキル
        skillText.Add("ALanding", "ALanding");
        skillText.Add("ADrift", "ADrift");
        //召喚スキル
        skillText.Add("SummonOokami", "Summon1");
        skillText.Add("SummonTaka", "Summon2");
        skillText.Add("SummonYosei", "Summon3");
        skillText.Add("SummonSaboten", "Summon4");
        //特殊スキル
        skillText.Add("Potion", "Potion");

    }

    public void SetText(string buttonSelected)
    {
        GetComponent<Text>().text = skillText[buttonSelected] + 
            ":" + SkillLearned.GetSkillActive(buttonSelected).ToString();
        
        //GetComponent<Text>().text = GameObject.Find("MainSystem").GetComponent<SkillLearned>().GetSkillActive(buttonSelected).ToString();
        //GetComponent<Text>().text = skillText[buttonSelected];
    }
}
