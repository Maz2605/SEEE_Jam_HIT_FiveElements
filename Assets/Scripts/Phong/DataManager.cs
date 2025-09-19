using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Skill Damage Default")]
    [SerializeField] private float damageSkill1 = 50f;
    [SerializeField] private float damageSkillUltimate = 1000f;

    [Header("Skill Timer")]
    [SerializeField] private float timerSkill2 = 5f;
    [SerializeField] private float timerSkill3 = 5f;

    [Header("Skill 3 Buffs")]
    [SerializeField] private float buffRangeAttack = 1.5f;
    [SerializeField] private float buffSpeedAttack = 1.2f;

    [Header("Skill Current Level")]
    [SerializeField] private int currentLevelSkill1 = 1;
    [SerializeField] private int currentLevelSkill2 = 1;
    [SerializeField] private int currentLevelSkill3 = 1;
    [SerializeField] private int currentLevelSkillUltimate = 1;

    [Header("Extra Buffs")]
    [SerializeField] private int buffHealTower = 0;
    [SerializeField] private int buffIncreaseDuration = 0;
    [SerializeField] private int buffIncreaseMaxHealth = 0;
    [SerializeField] private int buffIncreasePowerSpeed = 0;
    [SerializeField] private int buffIncreaseSpeedAttack = 0;

    [Header("Player Attributes")]
    [SerializeField] private int coin = 0;
    [SerializeField] private float towerHealth = 500f;
    [SerializeField] private float powerDuration = 10f;

    // 🔑 Keys
    private const string DamageSkill1Key = "DamageSkill1";
    private const string DamageSkillUltimateKey = "DamageSkillUltimate";
    private const string TimerSkill2Key = "TimerSkill2";
    private const string TimerSkill3Key = "TimerSkill3";
    private const string BuffRangeAttackKey = "BuffRangeAttack";
    private const string BuffSpeedAttackKey = "BuffSpeedAttack";

    private const string CurrentLevelSkill1Key = "CurrentLevelSkill1";
    private const string CurrentLevelSkill2Key = "CurrentLevelSkill2";
    private const string CurrentLevelSkill3Key = "CurrentLevelSkill3";
    private const string CurrentLevelSkillUltimateKey = "CurrentLevelSkillUltimate";

    private const string BuffHealTowerKey = "BuffHealTower";
    private const string BuffIncreaseDurationKey = "BuffIncreaseDuration";
    private const string BuffIncreaseMaxHealthKey = "BuffIncreaseMaxHealth";
    private const string BuffIncreasePowerSpeedKey = "BuffIncreasePowerSpeed";
    private const string BuffIncreaseSpeedAttackKey = "BuffIncreaseSpeedAttack";

    private const string CoinKey = "PlayerCoin";
    private const string TowerHealthKey = "TowerHealth";
    private const string PowerDurationKey = "PowerDuration";

    // =========================
    // 🔹 PROPERTIES (auto save)
    // =========================
    public float DamageSkill1
    {
        get => damageSkill1;
        set { damageSkill1 = value; SaveDataSkill1(damageSkill1); }
    }

    public float DamageSkillUltimate
    {
        get => damageSkillUltimate;
        set { damageSkillUltimate = value; SaveDataSkillUltimate(damageSkillUltimate); }
    }

    public float TimerSkill2
    {
        get => timerSkill2;
        set { timerSkill2 = value; SaveTimerSkill2(timerSkill2); }
    }

    public float TimerSkill3
    {
        get => timerSkill3;
        set { timerSkill3 = value; SaveTimerSkill3(timerSkill3); }
    }

    public float BuffRangeAttack
    {
        get => buffRangeAttack;
        set { buffRangeAttack = value; SaveBuffRangeAttack(buffRangeAttack); }
    }

    public float BuffSpeedAttack
    {
        get => buffSpeedAttack;
        set { buffSpeedAttack = value; SaveBuffSpeedAttack(buffSpeedAttack); }
    }

    public int CurrentLevelSkill1
    {
        get => currentLevelSkill1;
        set { currentLevelSkill1 = value; SaveCurrentLevelSkill1(currentLevelSkill1); }
    }

    public int CurrentLevelSkill2
    {
        get => currentLevelSkill2;
        set { currentLevelSkill2 = value; SaveCurrentLevelSkill2(currentLevelSkill2); }
    }

    public int CurrentLevelSkill3
    {
        get => currentLevelSkill3;
        set { currentLevelSkill3 = value; SaveCurrentLevelSkill3(currentLevelSkill3); }
    }

    public int CurrentLevelSkillUltimate
    {
        get => currentLevelSkillUltimate;
        set { currentLevelSkillUltimate = value; SaveCurrentLevelSkillUltimate(currentLevelSkillUltimate); }
    }

    public int BuffHealTower
    {
        get => buffHealTower;
        set { buffHealTower = value; SaveBuffHealTower(buffHealTower); }
    }

    public int BuffIncreaseDuration
    {
        get => buffIncreaseDuration;
        set { buffIncreaseDuration = value; SaveBuffIncreaseDuration(buffIncreaseDuration); }
    }

    public int BuffIncreaseMaxHealth
    {
        get => buffIncreaseMaxHealth;
        set { buffIncreaseMaxHealth = value; SaveBuffIncreaseMaxHealth(buffIncreaseMaxHealth); }
    }

    public int BuffIncreasePowerSpeed
    {
        get => buffIncreasePowerSpeed;
        set { buffIncreasePowerSpeed = value; SaveBuffIncreasePowerSpeed(buffIncreasePowerSpeed); }
    }

    public int BuffIncreaseSpeedAttack
    {
        get => buffIncreaseSpeedAttack;
        set { buffIncreaseSpeedAttack = value; SaveBuffIncreaseSpeedAttack(buffIncreaseSpeedAttack); }
    }

    public int Coin
    {
        get => coin;
        set { coin = value; SaveCoin(coin); }
    }

    public float TowerHealth
    {
        get => towerHealth;
        set { towerHealth = value; SaveTowerHealth(towerHealth); }
    }

    public float PowerDuration
    {
        get => powerDuration;
        set { powerDuration = value; SavePowerDuration(powerDuration); }
    }

    private void Awake()
    {
        LoadAll();
    }

    // =========================
    // 🔹 SAVE (luôn PlayerPrefs.Save())
    // =========================
    public void SaveDataSkill1(float damage) { PlayerPrefs.SetFloat(DamageSkill1Key, damage); PlayerPrefs.Save(); }
    public void SaveDataSkillUltimate(float damage) { PlayerPrefs.SetFloat(DamageSkillUltimateKey, damage); PlayerPrefs.Save(); }
    public void SaveTimerSkill2(float timer) { PlayerPrefs.SetFloat(TimerSkill2Key, timer); PlayerPrefs.Save(); }
    public void SaveTimerSkill3(float timer) { PlayerPrefs.SetFloat(TimerSkill3Key, timer); PlayerPrefs.Save(); }
    public void SaveBuffRangeAttack(float value) { PlayerPrefs.SetFloat(BuffRangeAttackKey, value); PlayerPrefs.Save(); }
    public void SaveBuffSpeedAttack(float value) { PlayerPrefs.SetFloat(BuffSpeedAttackKey, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkill1(int value) { PlayerPrefs.SetInt(CurrentLevelSkill1Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkill2(int value) { PlayerPrefs.SetInt(CurrentLevelSkill2Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkill3(int value) { PlayerPrefs.SetInt(CurrentLevelSkill3Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkillUltimate(int value) { PlayerPrefs.SetInt(CurrentLevelSkillUltimateKey, value); PlayerPrefs.Save(); }
    public void SaveBuffHealTower(int value) { PlayerPrefs.SetInt(BuffHealTowerKey, value); PlayerPrefs.Save(); }
    public void SaveBuffIncreaseDuration(int value) { PlayerPrefs.SetInt(BuffIncreaseDurationKey, value); PlayerPrefs.Save(); }
    public void SaveBuffIncreaseMaxHealth(int value) { PlayerPrefs.SetInt(BuffIncreaseMaxHealthKey, value); PlayerPrefs.Save(); }
    public void SaveBuffIncreasePowerSpeed(int value) { PlayerPrefs.SetInt(BuffIncreasePowerSpeedKey, value); PlayerPrefs.Save(); }
    public void SaveBuffIncreaseSpeedAttack(int value) { PlayerPrefs.SetInt(BuffIncreaseSpeedAttackKey, value); PlayerPrefs.Save(); }
    public void SaveCoin(int value) { PlayerPrefs.SetInt(CoinKey, value); PlayerPrefs.Save(); }
    public void SaveTowerHealth(float value) { PlayerPrefs.SetFloat(TowerHealthKey, value); PlayerPrefs.Save(); }
    public void SavePowerDuration(float value) { PlayerPrefs.SetFloat(PowerDurationKey, value); PlayerPrefs.Save(); }

    // =========================
    // 🔹 SAVE ALL
    // =========================
    public void SaveAll()
    {
        SaveDataSkill1(damageSkill1);
        SaveDataSkillUltimate(damageSkillUltimate);
        SaveTimerSkill2(timerSkill2);
        SaveTimerSkill3(timerSkill3);
        SaveBuffRangeAttack(buffRangeAttack);
        SaveBuffSpeedAttack(buffSpeedAttack);

        SaveCurrentLevelSkill1(currentLevelSkill1);
        SaveCurrentLevelSkill2(currentLevelSkill2);
        SaveCurrentLevelSkill3(currentLevelSkill3);
        SaveCurrentLevelSkillUltimate(currentLevelSkillUltimate);

        SaveBuffHealTower(buffHealTower);
        SaveBuffIncreaseDuration(buffIncreaseDuration);
        SaveBuffIncreaseMaxHealth(buffIncreaseMaxHealth);
        SaveBuffIncreasePowerSpeed(buffIncreasePowerSpeed);
        SaveBuffIncreaseSpeedAttack(buffIncreaseSpeedAttack);

        SaveCoin(coin);
        SaveTowerHealth(towerHealth);
        SavePowerDuration(powerDuration);

        Debug.Log("✅ Tất cả dữ liệu đã được lưu!");
    }

    // =========================
    // 🔹 LOAD ALL
    // =========================
    public void LoadAll()
    {
        damageSkill1 = PlayerPrefs.GetFloat(DamageSkill1Key, damageSkill1);
        damageSkillUltimate = PlayerPrefs.GetFloat(DamageSkillUltimateKey, damageSkillUltimate);
        timerSkill2 = PlayerPrefs.GetFloat(TimerSkill2Key, timerSkill2);
        timerSkill3 = PlayerPrefs.GetFloat(TimerSkill3Key, timerSkill3);
        buffRangeAttack = PlayerPrefs.GetFloat(BuffRangeAttackKey, buffRangeAttack);
        buffSpeedAttack = PlayerPrefs.GetFloat(BuffSpeedAttackKey, buffSpeedAttack);

        currentLevelSkill1 = PlayerPrefs.GetInt(CurrentLevelSkill1Key, currentLevelSkill1);
        currentLevelSkill2 = PlayerPrefs.GetInt(CurrentLevelSkill2Key, currentLevelSkill2);
        currentLevelSkill3 = PlayerPrefs.GetInt(CurrentLevelSkill3Key, currentLevelSkill3);
        currentLevelSkillUltimate = PlayerPrefs.GetInt(CurrentLevelSkillUltimateKey, currentLevelSkillUltimate);

        buffHealTower = PlayerPrefs.GetInt(BuffHealTowerKey, buffHealTower);
        buffIncreaseDuration = PlayerPrefs.GetInt(BuffIncreaseDurationKey, buffIncreaseDuration);
        buffIncreaseMaxHealth = PlayerPrefs.GetInt(BuffIncreaseMaxHealthKey, buffIncreaseMaxHealth);
        buffIncreasePowerSpeed = PlayerPrefs.GetInt(BuffIncreasePowerSpeedKey, buffIncreasePowerSpeed);
        buffIncreaseSpeedAttack = PlayerPrefs.GetInt(BuffIncreaseSpeedAttackKey, buffIncreaseSpeedAttack);

        coin = PlayerPrefs.GetInt(CoinKey, coin);
        towerHealth = PlayerPrefs.GetFloat(TowerHealthKey, towerHealth);
        powerDuration = PlayerPrefs.GetFloat(PowerDurationKey, powerDuration);

        Debug.Log("✅ Tất cả dữ liệu đã được load!");
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();

        damageSkill1 = 50f;
        damageSkillUltimate = 1000f;

        timerSkill2 = 5f;
        timerSkill3 = 5f;

        buffRangeAttack = 1.5f;
        buffSpeedAttack = 1.2f;

        currentLevelSkill1 = 1;
        currentLevelSkill2 = 1;
        currentLevelSkill3 = 1;
        currentLevelSkillUltimate = 1;

        buffHealTower = 0;
        buffIncreaseDuration = 0;
        buffIncreaseMaxHealth = 0;
        buffIncreasePowerSpeed = 0;
        buffIncreaseSpeedAttack = 0;

        coin = 0;
        towerHealth = 500f;
        powerDuration = 10f;

        SaveAll();
        Debug.Log("🔄 Dữ liệu đã reset về mặc định!");
    }
}
