using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventBao : Singleton<GameEventBao>
{
    public static Func<PlayerController> GetPlayer;
}
