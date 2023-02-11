﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//获得主控制器
public class SSDirector : System.Object
{
    private static SSDirector _instance;
    public ISceneController CurrentSenceController { get; set; }
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
}
