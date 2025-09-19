using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Skill Damage Default")]
    [SerializeField] private float damageSkill1 = 10f;
    [SerializeField] private float damageSkillUltimate = 100f;

    [Header("Skill Timer")]
    [SerializeField] private float timerSkill2 = 5f;

    [Header("Skill 3 Buffs")]
    [SerializeField] private float buffRangeAttack = 1.5f;
    [SerializeField] private float buffSpeedAttack = 1.2f;

    [Header("Extra Buffs")]
    [SerializeField] private int buffHealTower = 5;
    [SerializeField] private int buffIncreaseDuration = 2;
    [SerializeField] private int buffIncreaseMaxHealth = 5;
    [SerializeField] private int buffIncreasePowerSpeed = 1;
    [SerializeField] private int buffIncreaseSpeedAttack = 1;

    [Header("Player Attributes")]
    [SerializeField] private int coin = 0;
    [SerializeField] private float towerHealth = 500f;
    [SerializeField] private float powerDuration = 10f;

    // 🔑 Key lưu PlayerPrefs
    private const string DamageSkill1Key = "DamageSkill1";
    private const string DamageSkillUltimateKey = "DamageSkillUltimate";
    private const string TimerSkill2Key = "TimerSkill2";
    private const string BuffRangeAttackKey = "BuffRangeAttack";
    private const string BuffSpeedAttackKey = "BuffSpeedAttack";
    private const string BuffHealTowerKey = "BuffHealTower";
    private const string BuffIncreaseDurationKey = "BuffIncreaseDuration";
    private const string BuffIncreaseMaxHealthKey = "BuffIncreaseMaxHealth";
    private const string BuffIncreasePowerSpeedKey = "BuffIncreasePowerSpeed";
    private const string BuffIncreaseSpeedAttackKey = "BuffIncreaseSpeedAttack";
    private const string CoinKey = "PlayerCoin";
    private const string TowerHealthKey = "TowerHealth";
    private const string PowerDurationKey = "PowerDuration";

    // =========================
    // 🔹 PROPERTY
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
        DontDestroyOnLoad(gameObject);
        LoadAll();
    }

    // =========================
    // 🔹 SAVE từng biến
    // =========================
    public void SaveDataSkill1(float damage) =>
        PlayerPrefs.SetFloat(DamageSkill1Key, damage);

    public void SaveDataSkillUltimate(float damage) =>
        PlayerPrefs.SetFloat(DamageSkillUltimateKey, damage);

    public void SaveTimerSkill2(float timer) =>
        PlayerPrefs.SetFloat(TimerSkill2Key, timer);

    public void SaveBuffRangeAttack(float value) =>
        PlayerPrefs.SetFloat(BuffRangeAttackKey, value);

    public void SaveBuffSpeedAttack(float value) =>
        PlayerPrefs.SetFloat(BuffSpeedAttackKey, value);

    public void SaveBuffHealTower(float value) =>
        PlayerPrefs.SetFloat(BuffHealTowerKey, value);

    public void SaveBuffIncreaseDuration(float value) =>
        PlayerPrefs.SetFloat(BuffIncreaseDurationKey, value);

    public void SaveBuffIncreaseMaxHealth(float value) =>
        PlayerPrefs.SetFloat(BuffIncreaseMaxHealthKey, value);

    public void SaveBuffIncreasePowerSpeed(float value) =>
        PlayerPrefs.SetFloat(BuffIncreasePowerSpeedKey, value);

    public void SaveBuffIncreaseSpeedAttack(float value) =>
        PlayerPrefs.SetFloat(BuffIncreaseSpeedAttackKey, value);

    public void SaveCoin(int value) =>
        PlayerPrefs.SetInt(CoinKey, value);

    public void SaveTowerHealth(float value) =>
        PlayerPrefs.SetFloat(TowerHealthKey, value);

    public void SavePowerDuration(float value) =>
        PlayerPrefs.SetFloat(PowerDurationKey, value);

    // =========================
    // 🔹 SAVE ALL
    // =========================
    public void SaveAll()
    {
        PlayerPrefs.SetFloat(DamageSkill1Key, damageSkill1);
        PlayerPrefs.SetFloat(DamageSkillUltimateKey, damageSkillUltimate);
        PlayerPrefs.SetFloat(TimerSkill2Key, timerSkill2);
        PlayerPrefs.SetFloat(BuffRangeAttackKey, buffRangeAttack);
        PlayerPrefs.SetFloat(BuffSpeedAttackKey, buffSpeedAttack);
        PlayerPrefs.SetFloat(BuffHealTowerKey, buffHealTower);
        PlayerPrefs.SetFloat(BuffIncreaseDurationKey, buffIncreaseDuration);
        PlayerPrefs.SetFloat(BuffIncreaseMaxHealthKey, buffIncreaseMaxHealth);
        PlayerPrefs.SetFloat(BuffIncreasePowerSpeedKey, buffIncreasePowerSpeed);
        PlayerPrefs.SetFloat(BuffIncreaseSpeedAttackKey, buffIncreaseSpeedAttack);
        PlayerPrefs.SetInt(CoinKey, coin);
        PlayerPrefs.SetFloat(TowerHealthKey, towerHealth);
        PlayerPrefs.SetFloat(PowerDurationKey, powerDuration);

        PlayerPrefs.Save();
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
        buffRangeAttack = PlayerPrefs.GetFloat(BuffRangeAttackKey, buffRangeAttack);
        buffSpeedAttack = PlayerPrefs.GetFloat(BuffSpeedAttackKey, buffSpeedAttack);
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
}
