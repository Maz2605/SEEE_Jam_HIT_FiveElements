using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skill1 : MonoBehaviour
{
    public GameObject skill1Prefab;      
    public GameObject skill1RangePrefab;
    public UISkill uiSkill;
    public float timeCoolDown = 10f;
    public Image lookImage;

    [SerializeField] private bool _isLook = false;
    private bool _isChoiceSkill = false;
    private bool _isCooldown = false; 
    private GameObject _rangeIndicator;
    private Camera _mainCam;
    [SerializeField] private LayerMask groundMask; 
    private Vector3 _lastValidPosition;

    private Coroutine _cooldownRoutine;

    public void SetLook(bool isLook)
    {
        _isLook = isLook;
    }
    
    private void Awake()
    {
        _mainCam = Camera.main; 
        if (_mainCam == null)
        {
            Debug.LogError("Main Camera not found!");
            enabled = false; 
        }
    }

    private void OnEnable()
    {
        InputManager.OnPressW += UseSkill1;         
        InputManager.OnLeftClick += HandleLeftClick;
        InputManager.OnMouseMove += HandleMouseMove;
        InputManager.OnRightClick += HandleRightClick;
    }

    private void OnDisable()
    {
        InputManager.OnPressW -= UseSkill1;
        InputManager.OnLeftClick -= HandleLeftClick;
        InputManager.OnMouseMove -= HandleMouseMove;
        InputManager.OnRightClick -= HandleRightClick;

        CancelSkill();
    }

    private void UseSkill1()
    {
        if (_isChoiceSkill || skill1RangePrefab == null || _isCooldown || _isLook) return;

        Vector3 spawnPos = GetMouseWorldPosition(Input.mousePosition);
        if (spawnPos != Vector3.zero)
        {
            _isChoiceSkill = true;
            _lastValidPosition = spawnPos;

            _rangeIndicator = Instantiate(skill1RangePrefab, spawnPos, Quaternion.identity);
            ScaleRangeIndicator();
        }
        else
        {
            Debug.LogWarning("Failed to raycast to Ground layer!");
        }
    }

    private void HandleMouseMove(Vector3 screenPos)
    {
        if (!_isChoiceSkill || _rangeIndicator == null) return;

        Vector3 worldPos = GetMouseWorldPosition(screenPos);
        if (worldPos != Vector3.zero)
        {
            _lastValidPosition = Vector3.Lerp(_lastValidPosition, worldPos, Time.deltaTime * 20f);
            _rangeIndicator.transform.position = _lastValidPosition;
        }
    }

    private void HandleLeftClick(Vector3 screenPos)
    {
        if (!_isChoiceSkill || _rangeIndicator == null) return;

        Vector3 castPos = GetMouseWorldPosition(screenPos);
        if (castPos != Vector3.zero)
        {
            CastSkill(castPos);
        }
        else
        {
            Debug.LogWarning("Cannot cast skill: Invalid position!");
        }
    }

    private void HandleRightClick()
    {
        if (!_isChoiceSkill) return;
        CancelSkill();
    }

    private void CastSkill(Vector3 pos)
    {
        if (skill1Prefab == null)
        {
            Debug.LogError("Skill1Prefab is not assigned!");
            CancelSkill();
            return;
        }

        Instantiate(skill1Prefab, pos, Quaternion.identity);

        // Gọi UI cooldown
        if (uiSkill != null)
            uiSkill.StartLoading(timeCoolDown, ResetCooldown);

        // Bắt đầu cooldown sau khi cast
        StartCooldown();

        CancelSkill();
    }

    private void CancelSkill()
    {
        _isChoiceSkill = false;
        if (_rangeIndicator != null)
        {
            Destroy(_rangeIndicator);
            _rangeIndicator = null;
        }
    }
    
    private Vector3 GetMouseWorldPosition(Vector3 screenPos)
    {
        Ray ray = _mainCam.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, groundMask);

        if (hit.collider != null)
        {
            return new Vector3(hit.point.x, hit.point.y, 0f);
        }

        return Vector3.zero;
    }

    private void ScaleRangeIndicator()
    {
        if (_rangeIndicator == null || skill1Prefab == null) return;

        Collider2D col = skill1Prefab.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("Skill1Prefab has no Collider2D!");
            return;
        }

        float radius = 1f;
        float scale = skill1Prefab.transform.lossyScale.x;

        if (col is CircleCollider2D circle)
            radius = circle.radius * scale;
        else if (col is CapsuleCollider2D capsule)
            radius = capsule.size.x * 0.5f * scale;
        else if (col is BoxCollider2D box)
            radius = Mathf.Max(box.size.x, box.size.y) * 0.5f * scale;

        _rangeIndicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
    }

    // ================= COOL DOWN =================
    private void StartCooldown()
    {
        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);

        _cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        _isCooldown = true;
        yield return new WaitForSeconds(timeCoolDown);
        ResetCooldown();
    }

    private void ResetCooldown()
    {
        _isCooldown = false;
        _cooldownRoutine = null;
    }
}
