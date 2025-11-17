using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager> {
    private InputActionAsset _inputActions;
    
    protected override void Awake() {
        base.Awake();

        _inputActions = Resources.Load<InputActionAsset>("Input/InputActions");
        
    }
}
