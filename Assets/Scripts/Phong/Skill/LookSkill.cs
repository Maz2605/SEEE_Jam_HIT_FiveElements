using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookSkill : Singleton<LookSkill>
{
    public Image lookSkill2Iamge;
    public Image lookSkill3Iamge;
    public Image lookUltimateIamge;

    public void UnlookSkill2()
    {
        lookSkill2Iamge.enabled = false;
        // Huy khoa Skill 1
        
    }

    public void LookSkill2()
    {
        
    }
}
