using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueClickInput : MonoBehaviour
{
    private GameManager gameManager;
    private InputSystem_Actions inputActions;
private bool isHoldingSkip;   // 标记是否正在长按

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        gameManager = GetComponent<GameManager>();
    }

    void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.NextDialogue.performed += OnSubmit;
        inputActions.UI.NextDialogue.canceled += OnRelease; // 松开时取消长按
    }

    void OnDisable()
    {
        inputActions.UI.NextDialogue.performed -= OnSubmit;
        inputActions.UI.NextDialogue.canceled -= OnRelease;
        inputActions.UI.Disable();
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        // 单次点击 → 正常对话点击
        gameManager.dialogueManager.OnClick();
        Debug.Log("click");

        // 进入长按状态
        isHoldingSkip = true;
    }

    private void OnRelease(InputAction.CallbackContext context)
    {
        // 松开键 → 停止长按
        isHoldingSkip = false;
    }

    void Update()
    {
        if (isHoldingSkip)
        {
            // 持续长按时 → 快速跳过
            gameManager.dialogueManager.FastSkip();
        }
    }
}
