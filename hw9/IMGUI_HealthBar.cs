using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI_HealthBar : MonoBehaviour {

    public float curHealth;     //当前血量
    public float nextHealth;    //变化后的血量

    private Rect HealthBar;           //血条显示区域
    private Rect AddHealthButton;     //加血按钮
    private Rect MinusHealthButton;   //减血按钮
     

    // Use this for initialization
    void Start()
    {
        HealthBar = new Rect(300, 100, 200, 20);
        AddHealthButton = new Rect(520, 100, 40, 20);
        MinusHealthButton = new Rect(240, 100, 40, 20);

        nextHealth = curHealth = 0.0f;
    }

    void OnGUI()
    {
        if (GUI.Button(AddHealthButton, "加血"))
        {
            nextHealth = nextHealth + 0.1f > 1.0f ? 1.0f : nextHealth + 0.1f;
        }
        if (GUI.Button(MinusHealthButton, "减血"))
        {
            nextHealth = nextHealth - 0.1f < 0.0f ? 0.0f : nextHealth - 0.1f;
        }

        //插值计算health值，以实现血条值平滑变化
        curHealth = Mathf.Lerp(curHealth, nextHealth, 0.05f);

        // 用水平滚动条的宽度作为血条的显示值
        GUI.HorizontalScrollbar(HealthBar, 0.0f, curHealth, 0.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
