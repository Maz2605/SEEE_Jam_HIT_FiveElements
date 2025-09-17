using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action OnPressE;
    public static event Action OnPressW;
    public static event Action OnPressR;
    public static event Action OnPress1;
    public static event Action OnPress2;
    public static event Action OnPress3;
    public static event Action OnPressSpace;
    public static event Action<Vector3> OnMouseMove;
    public static event Action<Vector3> OnLeftClick;
    public static event Action OnRightClick;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[InputManager] Pressed E");
            OnPressE?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("[InputManager] Pressed W");
            OnPressW?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("[InputManager] Pressed R");
            OnPressR?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("[InputManager] Pressed 1");
            OnPress1?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("[InputManager] Pressed 2");
            OnPress2?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("[InputManager] Pressed 3");
            OnPress3?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[InputManager] Pressed Space");
            OnPressSpace?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick?.Invoke(Input.mousePosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick?.Invoke();
        }

        // Luôn gửi sự kiện chuột di chuyển
        OnMouseMove?.Invoke(Input.mousePosition);
    }
}