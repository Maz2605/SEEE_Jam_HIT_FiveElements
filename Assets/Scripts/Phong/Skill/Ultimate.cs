using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    public GameObject ultimatePrefab;      
    public float timeCoolDown = 3f;
    public UISkill uiSkill;
    public Image lookImage;

    [SerializeField] private bool _isLook = true;
    [SerializeField] private float _damage;
    private bool _isCooldown = false;
    private Camera _mainCam;
    [SerializeField] private LayerMask groundMask; 
    
    public void SetLook(bool isLook)
    {
        _isLook = isLook;
    }

    private void Awake()
    {
        _mainCam = Camera.main; // Cache camera in Awake
        if (_mainCam == null)
        {
            Debug.LogError("Main Camera not found!");
            enabled = false; // Disable script if no camera
        }
    }

    private void OnEnable()
    {
        InputManager.OnPressSpace += UseUltimate;
    }

    private void OnDisable()
    {
        InputManager.OnPressSpace -= UseUltimate;
    }

    private void UseUltimate()
    {
        if (ultimatePrefab == null || _isCooldown || _isLook) return;

        Vector3 spawnPos = GetMouseWorldPosition(Input.mousePosition);
        if (spawnPos != Vector3.zero)
        {
            CastSkill(spawnPos);
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

    private void CastSkill(Vector3 pos)
    {
        if (ultimatePrefab == null)
        {
            Debug.LogError("UltimatePrefab is not assigned!");
            return;
        }

        Instantiate(ultimatePrefab, Vector3.zero, Quaternion.identity);

        // Gọi UI cooldown
        if (uiSkill != null)
            uiSkill.StartLoading(timeCoolDown, ResetCooldown);
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

        return Vector3.zero; // Invalid position if no hit
    }
}
