using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static Action<int> onclick;
    public static Action<int> onUpdatePoint;
    public static Action<int> onUpdateEggLvlUI;
    public static Action<int> onUpdatePointUI;
    public static Action<EggData, EggData> onUpdateEgg;
}
