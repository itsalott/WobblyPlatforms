using UnityEngine.InputSystem;

public abstract class InputMap {
    protected InputActionMap _map;
    public InputMap(InputActionAsset inputActions, string name) {
        _map = inputActions.FindActionMap(name, true);
        
        FindActions();
    }

    ~InputMap() {
        DisposeActions();
    }

    protected abstract void FindActions();
    protected abstract void DisposeActions();
}
