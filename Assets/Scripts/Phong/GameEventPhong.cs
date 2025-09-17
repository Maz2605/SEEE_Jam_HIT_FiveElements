using System;

public static class GameEventPhong
{

    public static Action HealTower;
    public static Action IncreaseDuration;
    public static Action IncreaseMaxHeath;
    public static Action IncreasePowerSpeed;
    public static Action IncreaseSpeedAttack;
    public static Action IncreaseBaseDamage;

    public static Func<bool,int> GetIsLookSkill;
    
}