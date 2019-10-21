//Spriteをunityのアニメーション使わずに切り替えるスクリプト


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{


    public Sprite[] sprits;
    public int oneFrameTimeInt;//1フレーム16msとしたとき次のスプライトに何フレームで切り替えるか

    private SpriteRenderer MainSpriteRenderer;
    private float oneFrameTime = 0.0f;
    private int nowSpriteIndex = 0;
    private float nowFrameCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        nowFrameCount = 0.0f;
        oneFrameTime = oneFrameTimeInt * 0.016f;
        if (oneFrameTime == 0.0f)
        {
            oneFrameTime = 0.016f * 5;
        }

        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprits.Length != 0)
        {
            nowFrameCount += Time.deltaTime;
            if (nowFrameCount >= oneFrameTime)
            {
                //スプライトがある時
                MainSpriteRenderer.sprite = sprits[nowSpriteIndex];//スプライト切り替え
                nowFrameCount -= oneFrameTime;

                nowSpriteIndex++;
                if (nowSpriteIndex > sprits.Length - 1)
                {
                    nowSpriteIndex = 0;
                }
            }
        }
        else
        {
            return;
        }
    }
}