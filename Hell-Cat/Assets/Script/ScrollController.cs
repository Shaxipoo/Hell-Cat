using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollController : MonoBehaviour
{
    public InputActionAsset inputActions;

    public float UpY = 0f;      // 摄像机最上方位置
    public float DownY = -30f;  // 摄像机最下方位置
    public float scrollStep = 5f; // 每次滚轮移动的单位
    public float duration = 0.3f; // 平滑时间

    private float targetY;
    private Vector3 startPos;
    private float elapsed;
    private bool isMoving = false;

    private InputAction scrollAction;

    private void Awake()
    {
        // 获取 Dialogue ActionMap 下的 ScrollWheel Action
        scrollAction = inputActions.FindActionMap("Dialogue").FindAction("Scroll");
        scrollAction.performed += ctx => OnScroll(ctx);
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Dialogue").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Dialogue").Disable();
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();

        // 向上滚轮
        if (scrollValue > 0f)
        {
            StartMove(Mathf.Clamp(targetY + scrollStep, DownY, UpY));
        }
        // 向下滚轮
        else if (scrollValue < 0f)
        {
            StartMove(Mathf.Clamp(targetY - scrollStep, DownY, UpY));
        }
    }

    private void StartMove(float newY)
    {
        startPos = transform.position;
        targetY = newY;
        elapsed = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(startPos.y, targetY, t);
            transform.position = pos;

            if (t >= 1f)
                isMoving = false;
        }
    }

}
