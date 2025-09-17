using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Skill Damage Default")]
    [SerializeField] private float damageSkill1 = 10f;
    [SerializeField] private float damageSkill2 = 20f;
    [SerializeField] private float damageSkillUltimate = 100f;

    [Header("Skill Timer")]
    [SerializeField] private float timerSkill2 = 5f;

    [Header("Skill 3 Buffs")]
    [SerializeField] private float buffRangeAttack = 1.5f;
    [SerializeField] private float buffSpeedAttack = 1.2f;

    [Header("Player Data")]
    [SerializeField] private int coin = 0;

    // 🔑 Key lưu PlayerPrefs
    private const string DamageSkill1Key = "DamageSkill1";
    private const string DamageSkill2Key = "DamageSkill2";
    private const string DamageSkillUltimateKey = "DamageSkillUltimate";
    private const string TimerSkill2Key = "TimerSkill2";
    private const string BuffRangeAttackKey = "BuffRangeAttack";
    private const string BuffSpeedAttackKey = "BuffSpeedAttack";
    private const string CoinKey = "PlayerCoin";

    // =========================
    // 🔹 PROPERTY
    // =========================
    public float DamageSkill1
    {
        get => damageSkill1;
        set { damageSkill1 = value; SaveDataSkill1(damageSkill1); }
    }

    public float DamageSkill2
    {
        get => damageSkill2;
        set { damageSkill2 = value; SaveDataDamageSkill2(damageSkill2); }
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

    public int Coin
    {
        get => coin;
        set { coin = value; SaveCoin(coin); }
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

    public void SaveDataDamageSkill2(float damage) =>
        PlayerPrefs.SetFloat(DamageSkill2Key, damage);

    public void SaveDataSkillUltimate(float damage) =>
        PlayerPrefs.SetFloat(DamageSkillUltimateKey, damage);

    public void SaveTimerSkill2(float timer) =>
        PlayerPrefs.SetFloat(TimerSkill2Key, timer);

    public void SaveBuffRangeAttack(float value) =>
        PlayerPrefs.SetFloat(BuffRangeAttackKey, value);

    public void SaveBuffSpeedAttack(float value) =>
        PlayerPrefs.SetFloat(BuffSpeedAttackKey, value);

    public void SaveCoin(int value) =>
        PlayerPrefs.SetInt(CoinKey, value);

    // =========================
    // 🔹 SAVE ALL
    // =========================
    public void SaveAll()
    {
        PlayerPrefs.SetFloat(DamageSkill1Key, damageSkill1);
        PlayerPrefs.SetFloat(DamageSkill2Key, damageSkill2);
        PlayerPrefs.SetFloat(DamageSkillUltimateKey, damageSkillUltimate);
        PlayerPrefs.SetFloat(TimerSkill2Key, timerSkill2);
        PlayerPrefs.SetFloat(BuffRangeAttackKey, buffRangeAttack);
        PlayerPrefs.SetFloat(BuffSpeedAttackKey, buffSpeedAttack);
        PlayerPrefs.SetInt(CoinKey, coin);

        PlayerPrefs.Save();
        Debug.Log("✅ Tất cả dữ liệu đã được lưu!");
    }

    // =========================
    // 🔹 LOAD ALL
    // =========================
    public void LoadAll()
    {
        damageSkill1 = PlayerPrefs.GetFloat(DamageSkill1Key, damageSkill1);
        damageSkill2 = PlayerPrefs.GetFloat(DamageSkill2Key, damageSkill2);
        damageSkillUltimate = PlayerPrefs.GetFloat(DamageSkillUltimateKey, damageSkillUltimate);
        timerSkill2 = PlayerPrefs.GetFloat(TimerSkill2Key, timerSkill2);
        buffRangeAttack = PlayerPrefs.GetFloat(BuffRangeAttackKey, buffRangeAttack);
        buffSpeedAttack = PlayerPrefs.GetFloat(BuffSpeedAttackKey, buffSpeedAttack);
        coin = PlayerPrefs.GetInt(CoinKey, coin);

        Debug.Log("✅ Tất cả dữ liệu đã được load!");
    }
}
