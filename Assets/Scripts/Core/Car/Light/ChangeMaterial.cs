using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private string _targetMaterialName;

    private void Awake()
    {
        Change();
    }

    private void Change()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (var renderer in renderers)
        {
            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
            {
                if(renderer.sharedMaterials[i] == null) Debug.Log(renderer.gameObject);
                if (renderer.sharedMaterials[i].name == _targetMaterialName)
                {
                    var materials = renderer.sharedMaterials;
                    materials[i] = _material;
                    renderer.sharedMaterials = materials;
                }
            }
        }
    }
}
