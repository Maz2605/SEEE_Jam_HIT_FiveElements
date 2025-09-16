using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    public GameObject ultimatePrefab;      
    public GameObject ultimateRangePrefab;
    public float timeCoolDown = 3f;
    public UISkill uiSkill;
    public Image lookImage;

    [SerializeField] private bool _isLook = true;
    [SerializeField] private float _damage;
    private bool _isChoiceSkill = false;
    private bool _isCooldown = false;
    private GameObject _rangeIndicator;
    private Camera _mainCam;
    [SerializeField] private LayerMask groundMask; 
    private Vector3 _lastValidPosition; 
    
    public void SetLook(bool isLook)
    {
        _isLook = isLook;
    }

    private void Awake()
    {
        _mainCam = Camera.main; // Cache camera in Awake to avoid repeated Camera.main calls
        if (_mainCam == null)
        {
            Debug.LogError("Main Camera not found!");
            enabled = false; // Disable script if no camera
        }
    }

    private void OnEnable()
    {
        InputManager.OnPressSpace += UseUltimate;
        InputManager.OnLeftClick += HandleLeftClick;
        InputManager.OnMouseMove += HandleMouseMove;
        InputManager.OnRightClick += HandleRightClick;
    }

    private void OnDisable()
    {
        InputManager.OnPressSpace -= UseUltimate;
        InputManager.OnLeftClick -= HandleLeftClick;
        InputManager.OnMouseMove -= HandleMouseMove;
        InputManager.OnRightClick -= HandleRightClick;

        // Cleanup range indicator if still active
        CancelSkill();
    }

    private void UseUltimate()
    {
        if (_isChoiceSkill || ultimateRangePrefab == null || _isCooldown || _isLook) return;
        
        

        Vector3 spawnPos = GetMouseWorldPosition(Input.mousePosition);
        if (spawnPos != Vector3.zero)
        {
            _isChoiceSkill = true;
            _lastValidPosition = spawnPos;

            _rangeIndicator = Instantiate(
                ultimateRangePrefab,
                spawnPos,
                Quaternion.identity
            );
            ScaleRangeIndicator();
            
            // Bắt đầu cooldown
            StartCooldown();
        }
        else
        {
            Debug.LogWarning("Failed to raycast to Ground layer!");
        }
    }
    
    // ================= COOL DOWN =================
    private void StartCooldown()
    {
        _isCooldown = true;
        Invoke(nameof(ResetCooldown), timeCoolDown);
    }

    private void ResetCooldown()
    {
        _isCooldown = false;
    }

    private void HandleMouseMove(Vector3 screenPos)
    {
        if (!_isChoiceSkill || _rangeIndicator == null) return;

        Vector3 worldPos = GetMouseWorldPosition(screenPos);
        if (worldPos != Vector3.zero)
        {
            _lastValidPosition = Vector3.Lerp(_lastValidPosition, worldPos, Time.deltaTime * 50f);
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
            Debug.LogWarning("Cannot cast ultimate: Invalid position!");
        }
    }

    private void HandleRightClick()
    {
        if (!_isChoiceSkill) return;
        CancelSkill();
    }

    private void CastSkill(Vector3 pos)
    {
        if (ultimatePrefab == null)
        {
            Debug.LogError("UltimatePrefab is not assigned!");
            CancelSkill();
            return;
        }

        Instantiate(ultimatePrefab, pos, Quaternion.identity);
        // Gọi UI cooldown
        if (uiSkill != null)
            uiSkill.StartLoading(timeCoolDown, ResetCooldown);

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
        // Create a ray from the camera through the mouse position
        Ray ray = _mainCam.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, groundMask);

        if (hit.collider != null)
        {
            return new Vector3(hit.point.x, hit.point.y, 0f); // Ensure Z is 0 for 2D
        }

        return Vector3.zero; // Return invalid position if no hit
    }

    private void ScaleRangeIndicator()
    {
        if (_rangeIndicator == null || ultimatePrefab == null) return;

        Collider2D col = ultimatePrefab.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("UltimatePrefab has no Collider2D!");
            return;
        }

        float radius = 1f;
        float scale = ultimatePrefab.transform.lossyScale.x;

        if (col is CircleCollider2D circle)
            radius = circle.radius * scale;
        else if (col is CapsuleCollider2D capsule)
            radius = capsule.size.x * 0.5f * scale;
        else if (col is BoxCollider2D box)
            radius = Mathf.Max(box.size.x, box.size.y) * 0.5f * scale;

        _rangeIndicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
    }
}