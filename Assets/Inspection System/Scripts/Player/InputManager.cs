using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static InputManager _instance;
    private bool lockMovementInput = false;
    private bool lockMouseInput = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ── Lock controls ──────────────────────────────────────────────

    public void LockMovementInput(bool locked) {
        lockMovementInput = locked;
    }

    public void LockMouseInput(bool locked) {
        lockMouseInput = locked;
    }

    // ── Inputs ─────────────────────────────────────────────────────

    public Vector2 GetMovementInput()
    {
        if (lockMovementInput) return Vector2.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return new Vector2(x, y);
    }

    public Vector2 GetMouseMovementInput()
    {
        if (lockMouseInput) return Vector2.zero;

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y") * -1f;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns true the frame the interact key is pressed.
    /// Defined here so any system can listen without owning a KeyCode.
    /// </summary>
    public bool GetInteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}