using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public GameObject skill2Prefab;      
    public GameObject skill2RangePrefab;

    private bool _isChoiceSkill = false;
    private GameObject _rangeIndicator;
    private Camera _mainCam;
    [SerializeField] private LayerMask groundMask; 
    private Vector3 _lastValidPosition;
    

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
        InputManager.OnPressW += UseSkill2;         
        InputManager.OnLeftClick += HandleLeftClick;
        InputManager.OnMouseMove += HandleMouseMove;
        InputManager.OnRightClick += HandleRightClick;
    }

    private void OnDisable()
    {
        InputManager.OnPressW -= UseSkill2;
        InputManager.OnLeftClick -= HandleLeftClick;
        InputManager.OnMouseMove -= HandleMouseMove;
        InputManager.OnRightClick -= HandleRightClick;

        // Cleanup range indicator if still active
        CancelSkill();
    }

    private void UseSkill2()
    {
        if (_isChoiceSkill || skill2RangePrefab == null) return;

        Vector3 spawnPos = GetMouseWorldPosition(Input.mousePosition);
        if (spawnPos != Vector3.zero)
        {
            _isChoiceSkill = true;
            _lastValidPosition = spawnPos;

            _rangeIndicator = Instantiate(
                skill2RangePrefab,
                spawnPos,
                Quaternion.identity
            );
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
            // Smoothly move the indicator to reduce jitter
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
        if (skill2Prefab == null)
        {
            Debug.LogError("Skill1Prefab is not assigned!");
            CancelSkill();
            return;
        }

        Instantiate(skill2Prefab, pos, Quaternion.identity);
        CancelSkill(); // Cleanup after casting
        
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
        if (_rangeIndicator == null || skill2Prefab == null) return;

        Collider2D col = skill2Prefab.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("Skill1Prefab has no Collider2D!");
            return;
        }

        float radius = 1f;
        float scale = skill2Prefab.transform.lossyScale.x;

        if (col is CircleCollider2D circle)
            radius = circle.radius * scale;
        else if (col is CapsuleCollider2D capsule)
            radius = capsule.size.x * 0.5f * scale;
        else if (col is BoxCollider2D box)
            radius = Mathf.Max(box.size.x, box.size.y) * 0.5f * scale;

        _rangeIndicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
    }
}
