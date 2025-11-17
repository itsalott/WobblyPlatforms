using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager> {
    private InputActionAsset _inputActions;
    private Dictionary<Type, InputMap> _mappings;
    
    protected override void Awake() {
        base.Awake();

        _inputActions = Resources.Load<InputActionAsset>("Input/InputActions");
        _mappings = new Dictionary<Type, InputMap>();
        
        AddMaps();
    }

    private void AddMaps() {
        _mappings.Add(typeof(PlayerMap), new PlayerMap(_inputActions));
    }

    public TMap GetMap<TMap>() where TMap : InputMap {
        Type key = typeof(TMap);
        if (!_mappings.ContainsKey(key)) {
            throw new NotImplementedException($"Inputmap of type ({key}) has not been implemented yet!");
        }
        
        return _mappings[typeof(TMap)] as TMap;
    }
}
