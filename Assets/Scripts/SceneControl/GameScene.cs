using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Test;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Stat> dict = Managers.data.StatDict;

    }

    public override void Clear()
    {

    }



}
