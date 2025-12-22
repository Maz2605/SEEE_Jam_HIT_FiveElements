using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookSkill : Singleton<LookSkill>
{
    public Image lookSkill2Iamge;
    public Image lookSkill3Iamge;
    public Image lookUltimateIamge;

    public Skill2 skill2;
    public Skill3 skill3;
    public Ultimate ultimate;

    private void OnEnable()
    {
        GameEventPhong.LookSkill2 += LookSkill2;
        GameEventPhong.LookSkill3 += LookSkill3;
        GameEventPhong.LookUltimate += LookUltimate;

        GameEventPhong.UnLookSkill2 += UnlookSkill2;
        GameEventPhong.UnLookSkill3 += UnlookSkill3;
        GameEventPhong.UnLookUltimate += UnlookUltimate;
    }

    private void OnDisable()
    {
        GameEventPhong.LookSkill2 -= LookSkill2;
        GameEventPhong.LookSkill3 -= LookSkill3;
        GameEventPhong.LookUltimate -= LookUltimate;

        GameEventPhong.UnLookSkill2 -= UnlookSkill2;
        GameEventPhong.UnLookSkill3 -= UnlookSkill3;
        GameEventPhong.UnLookUltimate -= UnlookUltimate;
    }
    
    public void UnlookSkill2()
    {
        lookSkill2Iamge.gameObject.SetActive(false);
        skill2.SetLook(false);
    }

    public void UnlookSkill3()
    {
        UnlookSkill2();
        lookSkill3Iamge.gameObject.SetActive(false);
        skill3.SetLook(false);
    }

    public void UnlookUltimate()
    {
        UnlookSkill3();
        lookUltimateIamge.gameObject.SetActive(false);
        ultimate.SetLook(false);
    }

    public void LookSkill2()
    {
        LookSkill3();
        lookSkill2Iamge.gameObject.SetActive(true);
        skill2.SetLook(true);
        skill2.CancelSkill();
    }

    public void LookSkill3()
    {
        LookUltimate();
        lookSkill3Iamge.gameObject.SetActive(true);
        skill3.SetLook(true);
    }

    public void LookUltimate()
    {
        lookUltimateIamge.gameObject.SetActive(true);
        ultimate.SetLook(true);
    }
}


