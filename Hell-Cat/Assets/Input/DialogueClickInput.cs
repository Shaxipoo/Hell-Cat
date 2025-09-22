using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueClickInput : MonoBehaviour
{
    private GameManager gameManager;
    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        gameManager = this.gameObject.GetComponent<GameManager>();
    }

    void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.NextDialogue.performed += OnSubmit;
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        gameManager.dialogueManager.OnClick();
        Debug.Log("click");
    }
}
