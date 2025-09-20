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
    [SerializeField] private float buffHealTower = 100f;
    [SerializeField] private float buffIncreasePowerSpeed = 1.1f;

    [Header("Skill Current Level")]
    [SerializeField] private int currentLevelSkill1 = 1;
    [SerializeField] private int currentLevelSkill2 = 1;
    [SerializeField] private int currentLevelSkill3 = 1;
    [SerializeField] private int currentLevelSkillUltimate = 1;

    [Header("Player Attributes")]
    [SerializeField] private int coin = 0;
    [SerializeField] private float towerHealth = 500f;
    [SerializeField] private float powerDuration = 10f;
    [SerializeField] private float attackRange = 5f;   // ✅ mới
    [SerializeField] private float bulletSpeed = 10f;  // ✅ mới

    [Header("Audio Settings")]
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;

    [Header("Skill Upgrade Prices")]
    [SerializeField] private int priceSkill1 = 100;
    [SerializeField] private int priceSkill2 = 150;
    [SerializeField] private int priceSkill3 = 200;

    [Header("Buff Prices")]
    [SerializeField] private int priceBuffIncreaseDuration = 300;
    [SerializeField] private int priceBuffIncreaseMaxHealth = 400;

    [Header("Skin Prices")]
    [SerializeField] private int priceSkin1 = 500;
    [SerializeField] private int priceSkin2 = 750;
    [SerializeField] private int priceSkin3 = 1000;

    // 🔑 Keys
    private const string DamageSkill1Key = "DamageSkill1";
    private const string DamageSkillUltimateKey = "DamageSkillUltimate";
    private const string TimerSkill2Key = "TimerSkill2";
    private const string TimerSkill3Key = "TimerSkill3";
    private const string BuffRangeAttackKey = "BuffRangeAttack";
    private const string BuffSpeedAttackKey = "BuffSpeedAttack";
    private const string BuffHealTowerKey = "BuffHealTower";
    private const string BuffIncreasePowerSpeedKey = "BuffIncreasePowerSpeed";

    private const string CurrentLevelSkill1Key = "CurrentLevelSkill1";
    private const string CurrentLevelSkill2Key = "CurrentLevelSkill2";
    private const string CurrentLevelSkill3Key = "CurrentLevelSkill3";
    private const string CurrentLevelSkillUltimateKey = "CurrentLevelSkillUltimate";

    private const string CoinKey = "PlayerCoin";
    private const string TowerHealthKey = "TowerHealth";
    private const string PowerDurationKey = "PowerDuration";
    private const string AttackRangeKey = "AttackRange";   // ✅ mới
    private const string BulletSpeedKey = "BulletSpeed";   // ✅ mới

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private const string PriceSkill1Key = "PriceSkill1";
    private const string PriceSkill2Key = "PriceSkill2";
    private const string PriceSkill3Key = "PriceSkill3";
    private const string PriceBuffIncreaseDurationKey = "PriceBuffIncreaseDuration";
    private const string PriceBuffIncreaseMaxHealthKey = "PriceBuffIncreaseMaxHealth";
    private const string PriceSkin1Key = "PriceSkin1";
    private const string PriceSkin2Key = "PriceSkin2";
    private const string PriceSkin3Key = "PriceSkin3";

    // =========================
    // 🔹 PROPERTIES
    // =========================
    public float DamageSkill1 { get => damageSkill1; set { damageSkill1 = value; SaveDataSkill1(damageSkill1); } }
    public float DamageSkillUltimate { get => damageSkillUltimate; set { damageSkillUltimate = value; SaveDataSkillUltimate(damageSkillUltimate); } }
    public float TimerSkill2 { get => timerSkill2; set { timerSkill2 = value; SaveTimerSkill2(timerSkill2); } }
    public float TimerSkill3 { get => timerSkill3; set { timerSkill3 = value; SaveTimerSkill3(timerSkill3); } }
    public float BuffRangeAttack { get => buffRangeAttack; set { buffRangeAttack = value; SaveBuffRangeAttack(buffRangeAttack); } }
    public float BuffSpeedAttack { get => buffSpeedAttack; set { buffSpeedAttack = value; SaveBuffSpeedAttack(buffSpeedAttack); } }
    public float BuffHealTower { get => buffHealTower; set { buffHealTower = value; SaveBuffHealTower(buffHealTower); } }
    public float BuffIncreasePowerSpeed { get => buffIncreasePowerSpeed; set { buffIncreasePowerSpeed = value; SaveBuffIncreasePowerSpeed(buffIncreasePowerSpeed); } }

    public int CurrentLevelSkill1 { get => currentLevelSkill1; set { currentLevelSkill1 = value; SaveCurrentLevelSkill1(currentLevelSkill1); } }
    public int CurrentLevelSkill2 { get => currentLevelSkill2; set { currentLevelSkill2 = value; SaveCurrentLevelSkill2(currentLevelSkill2); } }
    public int CurrentLevelSkill3 { get => currentLevelSkill3; set { currentLevelSkill3 = value; SaveCurrentLevelSkill3(currentLevelSkill3); } }
    public int CurrentLevelSkillUltimate { get => currentLevelSkillUltimate; set { currentLevelSkillUltimate = value; SaveCurrentLevelSkillUltimate(currentLevelSkillUltimate); } }

    public int Coin { get => coin; set { coin = value; SaveCoin(coin); } }
    public float TowerHealth { get => towerHealth; set { towerHealth = value; SaveTowerHealth(towerHealth); } }
    public float PowerDuration { get => powerDuration; set { powerDuration = value; SavePowerDuration(powerDuration); } }

    public float AttackRange { get => attackRange; set { attackRange = value; SaveAttackRange(attackRange); } }   // ✅ mới
    public float BulletSpeed { get => bulletSpeed; set { bulletSpeed = value; SaveBulletSpeed(bulletSpeed); } } // ✅ mới

    public float MusicVolume { get => musicVolume; set { musicVolume = value; SaveMusicVolume(musicVolume); } }
    public float SfxVolume { get => sfxVolume; set { sfxVolume = value; SaveSfxVolume(sfxVolume); } }

    public int PriceSkill1 { get => priceSkill1; set { priceSkill1 = value; SavePriceSkill1(priceSkill1); } }
    public int PriceSkill2 { get => priceSkill2; set { priceSkill2 = value; SavePriceSkill2(priceSkill2); } }
    public int PriceSkill3 { get => priceSkill3; set { priceSkill3 = value; SavePriceSkill3(priceSkill3); } }
    public int PriceBuffIncreaseDuration { get => priceBuffIncreaseDuration; set { priceBuffIncreaseDuration = value; SavePriceBuffIncreaseDuration(priceBuffIncreaseDuration); } }
    public int PriceBuffIncreaseMaxHealth { get => priceBuffIncreaseMaxHealth; set { priceBuffIncreaseMaxHealth = value; SavePriceBuffIncreaseMaxHealth(priceBuffIncreaseMaxHealth); } }
    public int PriceSkin1 { get => priceSkin1; set { priceSkin1 = value; SavePriceSkin1(priceSkin1); } }
    public int PriceSkin2 { get => priceSkin2; set { priceSkin2 = value; SavePriceSkin2(priceSkin2); } }
    public int PriceSkin3 { get => priceSkin3; set { priceSkin3 = value; SavePriceSkin3(priceSkin3); } }

    private void Awake()
    {
        LoadAll();
    }

    // =========================
    // 🔹 SAVE
    // =========================
    public void SaveDataSkill1(float damage) { PlayerPrefs.SetFloat(DamageSkill1Key, damage); PlayerPrefs.Save(); }
    public void SaveDataSkillUltimate(float damage) { PlayerPrefs.SetFloat(DamageSkillUltimateKey, damage); PlayerPrefs.Save(); }
    public void SaveTimerSkill2(float timer) { PlayerPrefs.SetFloat(TimerSkill2Key, timer); PlayerPrefs.Save(); }
    public void SaveTimerSkill3(float timer) { PlayerPrefs.SetFloat(TimerSkill3Key, timer); PlayerPrefs.Save(); }
    public void SaveBuffRangeAttack(float value) { PlayerPrefs.SetFloat(BuffRangeAttackKey, value); PlayerPrefs.Save(); }
    public void SaveBuffSpeedAttack(float value) { PlayerPrefs.SetFloat(BuffSpeedAttackKey, value); PlayerPrefs.Save(); }
    public void SaveBuffHealTower(float value) { PlayerPrefs.SetFloat(BuffHealTowerKey, value); PlayerPrefs.Save(); }
    public void SaveBuffIncreasePowerSpeed(float value) { PlayerPrefs.SetFloat(BuffIncreasePowerSpeedKey, value); PlayerPrefs.Save(); }

    public void SaveCurrentLevelSkill1(int value) { PlayerPrefs.SetInt(CurrentLevelSkill1Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkill2(int value) { PlayerPrefs.SetInt(CurrentLevelSkill2Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkill3(int value) { PlayerPrefs.SetInt(CurrentLevelSkill3Key, value); PlayerPrefs.Save(); }
    public void SaveCurrentLevelSkillUltimate(int value) { PlayerPrefs.SetInt(CurrentLevelSkillUltimateKey, value); PlayerPrefs.Save(); }

    public void SaveCoin(int value) { PlayerPrefs.SetInt(CoinKey, value); PlayerPrefs.Save(); }
    public void SaveTowerHealth(float value) { PlayerPrefs.SetFloat(TowerHealthKey, value); PlayerPrefs.Save(); }
    public void SavePowerDuration(float value) { PlayerPrefs.SetFloat(PowerDurationKey, value); PlayerPrefs.Save(); }
    public void SaveAttackRange(float value) { PlayerPrefs.SetFloat(AttackRangeKey, value); PlayerPrefs.Save(); }   // ✅ mới
    public void SaveBulletSpeed(float value) { PlayerPrefs.SetFloat(BulletSpeedKey, value); PlayerPrefs.Save(); } // ✅ mới

    public void SaveMusicVolume(float value) { PlayerPrefs.SetFloat(MusicVolumeKey, value); PlayerPrefs.Save(); }
    public void SaveSfxVolume(float value) { PlayerPrefs.SetFloat(SfxVolumeKey, value); PlayerPrefs.Save(); }

    public void SavePriceSkill1(int value) { PlayerPrefs.SetInt(PriceSkill1Key, value); PlayerPrefs.Save(); }
    public void SavePriceSkill2(int value) { PlayerPrefs.SetInt(PriceSkill2Key, value); PlayerPrefs.Save(); }
    public void SavePriceSkill3(int value) { PlayerPrefs.SetInt(PriceSkill3Key, value); PlayerPrefs.Save(); }
    public void SavePriceBuffIncreaseDuration(int value) { PlayerPrefs.SetInt(PriceBuffIncreaseDurationKey, value); PlayerPrefs.Save(); }
    public void SavePriceBuffIncreaseMaxHealth(int value) { PlayerPrefs.SetInt(PriceBuffIncreaseMaxHealthKey, value); PlayerPrefs.Save(); }
    public void SavePriceSkin1(int value) { PlayerPrefs.SetInt(PriceSkin1Key, value); PlayerPrefs.Save(); }
    public void SavePriceSkin2(int value) { PlayerPrefs.SetInt(PriceSkin2Key, value); PlayerPrefs.Save(); }
    public void SavePriceSkin3(int value) { PlayerPrefs.SetInt(PriceSkin3Key, value); PlayerPrefs.Save(); }

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
        SaveBuffHealTower(buffHealTower);
        SaveBuffIncreasePowerSpeed(buffIncreasePowerSpeed);

        SaveCurrentLevelSkill1(currentLevelSkill1);
        SaveCurrentLevelSkill2(currentLevelSkill2);
        SaveCurrentLevelSkill3(currentLevelSkill3);
        SaveCurrentLevelSkillUltimate(currentLevelSkillUltimate);

        SaveCoin(coin);
        SaveTowerHealth(towerHealth);
        SavePowerDuration(powerDuration);
        SaveAttackRange(attackRange);   // ✅ mới
        SaveBulletSpeed(bulletSpeed);   // ✅ mới

        SaveMusicVolume(musicVolume);
        SaveSfxVolume(sfxVolume);

        SavePriceSkill1(priceSkill1);
        SavePriceSkill2(priceSkill2);
        SavePriceSkill3(priceSkill3);

        SavePriceBuffIncreaseDuration(priceBuffIncreaseDuration);
        SavePriceBuffIncreaseMaxHealth(priceBuffIncreaseMaxHealth);

        SavePriceSkin1(priceSkin1);
        SavePriceSkin2(priceSkin2);
        SavePriceSkin3(priceSkin3);

        Debug.Log("✅ Tất cả dữ liệu + giá đã được lưu!");
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
        buffHealTower = PlayerPrefs.GetFloat(BuffHealTowerKey, buffHealTower);
        buffIncreasePowerSpeed = PlayerPrefs.GetFloat(BuffIncreasePowerSpeedKey, buffIncreasePowerSpeed);

        currentLevelSkill1 = PlayerPrefs.GetInt(CurrentLevelSkill1Key, currentLevelSkill1);
        currentLevelSkill2 = PlayerPrefs.GetInt(CurrentLevelSkill2Key, currentLevelSkill2);
        currentLevelSkill3 = PlayerPrefs.GetInt(CurrentLevelSkill3Key, currentLevelSkill3);
        currentLevelSkillUltimate = PlayerPrefs.GetInt(CurrentLevelSkillUltimateKey, currentLevelSkillUltimate);

        coin = PlayerPrefs.GetInt(CoinKey, coin);
        towerHealth = PlayerPrefs.GetFloat(TowerHealthKey, towerHealth);
        powerDuration = PlayerPrefs.GetFloat(PowerDurationKey, powerDuration);
        attackRange = PlayerPrefs.GetFloat(AttackRangeKey, attackRange);   // ✅ mới
        bulletSpeed = PlayerPrefs.GetFloat(BulletSpeedKey, bulletSpeed);   // ✅ mới

        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolume);
        sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, sfxVolume);

        priceSkill1 = PlayerPrefs.GetInt(PriceSkill1Key, priceSkill1);
        priceSkill2 = PlayerPrefs.GetInt(PriceSkill2Key, priceSkill2);
        priceSkill3 = PlayerPrefs.GetInt(PriceSkill3Key, priceSkill3);

        priceBuffIncreaseDuration = PlayerPrefs.GetInt(PriceBuffIncreaseDurationKey, priceBuffIncreaseDuration);
        priceBuffIncreaseMaxHealth = PlayerPrefs.GetInt(PriceBuffIncreaseMaxHealthKey, priceBuffIncreaseMaxHealth);

        priceSkin1 = PlayerPrefs.GetInt(PriceSkin1Key, priceSkin1);
        priceSkin2 = PlayerPrefs.GetInt(PriceSkin2Key, priceSkin2);
        priceSkin3 = PlayerPrefs.GetInt(PriceSkin3Key, priceSkin3);

        Debug.Log("✅ Tất cả dữ liệu + giá đã được load!");
    }

    // =========================
    // 🔹 RESET ALL
    // =========================
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();

        damageSkill1 = 50f;
        damageSkillUltimate = 1000f;
        timerSkill2 = 5f;
        timerSkill3 = 5f;
        buffRangeAttack = 1.5f;
        buffSpeedAttack = 1.2f;
        buffHealTower = 100f;
        buffIncreasePowerSpeed = 1.1f;

        currentLevelSkill1 = 1;
        currentLevelSkill2 = 1;
        currentLevelSkill3 = 1;
        currentLevelSkillUltimate = 1;

        coin = 1000;
        towerHealth = 500f;
        powerDuration = 10f;
        attackRange = 5f;   // ✅ default
        bulletSpeed = 10f;  // ✅ default

        musicVolume = 1f;
        sfxVolume = 1f;

        priceSkill1 = 100;
        priceSkill2 = 150;
        priceSkill3 = 200;

        priceBuffIncreaseDuration = 300;
        priceBuffIncreaseMaxHealth = 400;

        priceSkin1 = 500;
        priceSkin2 = 750;
        priceSkin3 = 1000;

        SaveAll();
        Debug.Log("🔄 Dữ liệu + giá đã reset về mặc định!");
    }
}
