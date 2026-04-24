using System;
using UnityEngine;

public class SetDisplacePos : MonoBehaviour {
    [SerializeField] private Renderer _rnd;
    
    // Create copy of material, so the used position is only set on this object instance of the material
    [SerializeField] private bool _copyMaterial;

    [SerializeField] private Transform _followTransform;

    private void Awake() {
        if (_copyMaterial) {
            string name = _rnd.material.name;
            
            _rnd.material = new Material(_rnd.material);
            _rnd.material.name = name + "_UniqueCopy";
        }
    }

    private void Update() {
        _rnd.material.SetVector("_DisplacePos", _followTransform.position);
    }
}
