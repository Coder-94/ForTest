using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    public enum CameraMode
    {
        QuarterView
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Test
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

}