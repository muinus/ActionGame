using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillLearned : MonoBehaviour
{
    public static Dictionary<string, bool> skillTable
        = new Dictionary<string, bool>()
        {
            //剣スキル
            {"Hamma", false},
            {"Sickle", false},
            {"HighSlash", false},
            {"ThrowSword", false},
            {"Iai", false},
            {"AirRaid", false},
            {"Slashing", false},
            //銃スキル
            {"Fannel", false},
            {"RailGun", false},
            {"Midareuti", false},
            {"ShotGun", false},
            {"MachineGun", false},
            //魔法スキル
            {"WaterMasic", false},
            {"FireTower", false},
            {"Tyoson", false},
            {"Lightning-Strike", false},
            {"BlackHole", false},
            //移動スキル
            {"ALanding", false},
            {"ADrift", false},
            //召喚スキル
            {"Summon1", false},
            {"Summon2", false},
            {"Summon3", false},
            {"Summon4", false},
            //ポーション
            {"Potion", false}
        };

    GameObject skill;

    bool islearned;

    string buttonSelected="none";
    // Start is called before the first frame update
    void Start()
    {
        skill = GameObject.Find("Canvas").transform.Find("Skill").gameObject;
        islearned = false;
    }

    public void DebugD()
    {
        foreach ( KeyValuePair<string,bool> skill in skillTable)
            Debug.Log(skill.Key+":"+skill.Value);
    }

    public void BottunSelected(string buttonSelected)
    {
        this.buttonSelected = buttonSelected;
    }

    public void SetSkillActive()
    {
        if (!islearned)
        {
            skillTable[buttonSelected] = true;
            islearned = true;
        }
    }

    public static bool GetSkillActive(string skillName)
    {
        return skillTable[skillName];
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Stage5");
    }
}
