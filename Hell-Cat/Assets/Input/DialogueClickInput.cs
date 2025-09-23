using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class DialogueClickInput : MonoBehaviour
{
private GameManager gameManager;
    private InputSystem_Actions inputActions;
    private bool isHoldingSkip = false;
    private float holdTime = 0f;
    [SerializeField] private float holdThreshold = 0.3f; 

    private float skipTimer = 0f;
    [SerializeField] private float skipInterval = 0.1f;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.NextDialogue.started += OnPressStart;
        inputActions.UI.NextDialogue.canceled += OnPressEnd;
    }

    private void OnDisable()
    {
        inputActions.UI.NextDialogue.started -= OnPressStart;
        inputActions.UI.NextDialogue.canceled -= OnPressEnd;
        inputActions.UI.Disable();
    }

    private void OnPressStart(InputAction.CallbackContext context)
    {
        holdTime = 0f;
        isHoldingSkip = false;
    }

    private void OnPressEnd(InputAction.CallbackContext context)
    {
        if (!isHoldingSkip)
        {
            gameManager.dialogueManager.OnClick();
        }

        isHoldingSkip = false;
        skipTimer = 0f;
    }

    private void Update()
    {
        if (inputActions.UI.NextDialogue.ReadValue<float>() > 0)
        {
            holdTime += Time.deltaTime;

            if (!isHoldingSkip && holdTime >= holdThreshold)
            {
                isHoldingSkip = true;
            }
        }

        if (isHoldingSkip)
        {
            skipTimer += Time.deltaTime;
            if (skipTimer >= skipInterval)
            {
                gameManager.dialogueManager.FastSkip();
                skipTimer = 0f;
            }
        }
    }
}
