using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UIBase
{

    enum GameObjects
    {
        Inven_Item,
        Inven_Text
    }

    string _name;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.Inven_Text).GetComponent<Text>().text = _name;
        Get<GameObject>((int)GameObjects.Inven_Item).BindEvent((PointerEventData) => { Debug.Log($"æ∆¿Ã≈∆ ≈¨∏Ø! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
