using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : UIBase
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}
