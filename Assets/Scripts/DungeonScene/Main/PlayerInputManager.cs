using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    public PlayerInput playerInput;
    //Action Maps
    private string actionMapPlayerControls = "Gameplay";
    private string actionMapMenuControls = "UI";
    private string currentControlScheme;
    public Vector2 rawInputMovement;

    public bool isRotating;

    public bool isWaiting;
    



    public int speed = 3;

    public void Init()
    {
        currentControlScheme = playerInput.currentControlScheme;
     //   BindControls();

    }

    //public void BindControls()
    //{

    //    InputActionMap inputActionMap = playerInput.currentActionMap;
    //    //    Started - The button press has been initiated.
    //    //    Performed - The button press has been successfully performed. Runs immediately after the started phase.
    //    //Canceled - The button has been released.
    //    //inputActionMap.FindAction("Move").performed += OnMovement;
    //    inputActionMap.FindAction("Move").started += OnMovement;
    //    inputActionMap.FindAction("Move").canceled += OnMovement;

    //    inputActionMap.FindAction("SpeedPlus").canceled += OnSpeedPlus;
    //    inputActionMap.FindAction("SpeedLess").canceled += OnSpeedLess;
    //    inputActionMap.FindAction("Esc").canceled += OnOpenMenu;

    //    playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    //    inputActionMap = playerInput.currentActionMap;
    //    Debug.Log(playerInput.currentActionMap.name);
    //    inputActionMap.FindAction("Esc").canceled += OnOpenMenu;

    //    playerInput.SwitchCurrentActionMap(actionMapPlayerControls);



    //}
    public void OnMovement(InputAction.CallbackContext value)
    {

        Vector2 inputMovement = value.ReadValue<Vector2>();
        //   Debug.Log("Value1 " + inputMovement);

        rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
    }
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);

        if (context.canceled)
        {
            TogglePauseState();

            
        }
        // Engine.Instance.ToggleMenu();
        //  Engine.Instance.isPaused = true;
    }
    public void TogglePauseState()
    {
        GameManager.GetInstance().isPausedMenu = !GameManager.GetInstance().isPausedMenu;
        bool toggle = GameManager.GetInstance().isPausedMenu;

        SetInputActiveState(toggle);

        SwitchFocusedPlayerControlScheme(toggle);

        if (toggle)
        {
            Engine.Instance.menu.Open();
        }
        else
        {
            Engine.Instance.menu.Close();

        }
       // UpdateUIMenu();

    }
    void SwitchFocusedPlayerControlScheme(bool toggle)
    {
        switch (toggle)
        {
            case true:
                EnablePauseMenuControls();
                break;

            case false:
                EnableGameplayControls();
                break;
        }
    }
    public void OnWait(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().IsPaused())
        {
            return;
        }

        if (context.started)
        {
            isWaiting = true;
        }
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().IsPaused())
        {
            return;
        }
        if (context.started)
        {
            isRotating = true;
        }

        if (context.canceled)
        {
            isRotating = false;
        }
    }

    public void OnSpeedLess(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().IsPaused())
        {
            return;
        }

        if (context.canceled)
        {
            speed--;
            if (speed < 1)
            {
                speed = 1;
            }
        }
    }

    public void OnSpeedPlus(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().IsPaused())
        {
            return;
        }
        if (context.canceled)
        {
            speed++;
            if (speed > 6)
            {
                speed = 6;
            }
        }


        //    Debug.Log(context.phase);

        //if (context.performed)
        //{
        //    Debug.Log("OnSpeedPlus performed");
        //}
        //else if (context.started)
        //{
        //    Debug.Log("OnSpeedPlus started");
        //}
        //else if (context.canceled)
        //{
        //    Debug.Log("OnSpeedPlus canceled");
        //}
    }
    public void SetInputActiveState(bool gameIsPaused)
    {
        switch (gameIsPaused)
        {
            case true:
                playerInput.DeactivateInput();
                break;

            case false:
                playerInput.ActivateInput();
                break;
        }
    }

    public void EnableGameplayControls()
    {
       // playerInput.actions.FindActionMap(actionMapMenuControls).Disable();
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
        //  SetInputActiveState(true);
    }

    public void EnablePauseMenuControls()
    {

          playerInput.SwitchCurrentActionMap(actionMapMenuControls);
        //   SetInputActiveState(false);
      //  playerInput.actions.FindActionMap(actionMapMenuControls).Enable();

        //  Debug.Log(playerInput.currentActionMap);
        //  playerInput.actions.FindActionMap("NameOfMyMap").Disable();
    }
    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }
    public void OnControlsChanged()
    {

        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;

            RemoveAllBindingOverrides();
        }
    }
}
