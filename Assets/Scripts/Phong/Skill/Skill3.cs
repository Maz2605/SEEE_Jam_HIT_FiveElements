using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.OnPressR += UseSkill3;
    }

    private void OnDisable()
    {
        InputManager.OnPressR -= UseSkill3;
    }

    private void UseSkill3()
    {
        // Lam gi do voi player
        //....
    }
}
