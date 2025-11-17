using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 0.01f;
    
    private Vector2 Direction { get; set; }

    private void Start() {
        InputManager.Instance.GetMap<PlayerMap>().directionChanged += DirectionChanged;
    }

    private void DirectionChanged(Vector2 direction) {
        Direction = direction;
    }

    private void Update() {
        transform.position += (Vector3)Direction * _speed;
    }
}
