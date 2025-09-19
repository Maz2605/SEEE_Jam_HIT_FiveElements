using System;
using UnityEngine;

public static class GameEventPhong
{

    public static Action HealTower;
    public static Action IncreaseDuration;
    public static Action IncreaseMaxHeath;
    public static Action IncreasePowerSpeed;
    public static Action IncreaseSpeedAttack;
    public static Action IncreaseBaseDamage;
    
    public static Action HandleIncreaseMaxHeath;
    public static Action HandleIncreaseDuration;

    public static Func<bool,int> GetIsLookSkill;

    public static Action AppearStore;

    public static Action UpgradeSkill1;
    public static Action UpgradeSkill2;
    public static Action UpgradeSkill3;
    public static Action UpgradeSkillUltimate;

    public static Action LookSkill2;
    public static Action LookSkill3;
    public static Action LookUltimate;
    
    public static Action UnLookSkill2;
    public static Action UnLookSkill3;
    public static Action UnLookUltimate;
    
    [Header("Shop")]
    public static Action BuyBuffIncreaseMaxHeath;
    public static Action BuyBuffIncreaseDuration;

}