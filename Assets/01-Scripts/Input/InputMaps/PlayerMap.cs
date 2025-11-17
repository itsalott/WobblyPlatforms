using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMap : InputMap {
	protected const string DIRECTION_KEY = "Direction";
	
	public Action<Vector2> directionChanged;
	
	public PlayerMap(InputActionAsset inputActions) : base(inputActions, "Player") { }
	protected override void FindActions() {
		_map[DIRECTION_KEY].performed += OnDirChanged;
		_map[DIRECTION_KEY].canceled += OnDirChanged;
	}
	protected override void DisposeActions() {
		_map[DIRECTION_KEY].performed -= OnDirChanged;
		_map[DIRECTION_KEY].canceled -= OnDirChanged;
		_map[DIRECTION_KEY].Dispose();
		
		directionChanged = null;
	}
	
	private void OnDirChanged(InputAction.CallbackContext context) {
		if (context.canceled) {
			directionChanged?.Invoke(Vector2.zero);
			return;
		}

		directionChanged?.Invoke(context.ReadValue<Vector2>());
	}
}
