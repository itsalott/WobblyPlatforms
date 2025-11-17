using System;
using System.Numerics;
using UnityEngine.InputSystem;

//Noot noot
public class MovementMap : InputMap {
	protected const string DIRECTION_CHANGED = "Direction";
	
	public Action<Vector2> directionChanged;
	
	public MovementMap(InputActionAsset inputActions) : base(inputActions, "Movement") { }
	protected override void FindActions() {
		_map[DIRECTION_CHANGED].performed += OnDirectionChanged;
		_map[DIRECTION_CHANGED].canceled += OnDirectionChanged;
	}
	protected override void DisposeActions() {
		_map[DIRECTION_CHANGED].performed -= OnDirectionChanged;
		_map[DIRECTION_CHANGED].canceled -= OnDirectionChanged;
		_map[DIRECTION_CHANGED].Dispose();
		
		directionChanged = null;
	}
	

	private void OnDirectionChanged(InputAction.CallbackContext context) {
		if (context.canceled) {
			directionChanged?.Invoke(Vector2.Zero);
			return;
		}

		directionChanged?.Invoke(context.ReadValue<Vector2>());
	}
}
