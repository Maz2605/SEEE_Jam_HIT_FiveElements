using System;

public static class GameEventPhong
{

    public static Func<float> HealTower;
    public static Func<float> IncreaseDuration;
    public static Func<float> IncreaseMaxHeath;
    public static Func<float> IncreasePowerSpeed;
    public static Func<float> IncreaseSpeedAttack;
    public static Func<float> IncreaseBaseDamage;

    public static Func<bool,int> GetIsLookSkill;
    
}