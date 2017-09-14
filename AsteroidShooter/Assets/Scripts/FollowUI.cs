using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _parallax = 5f;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update ()
    {
        UpdateBackGround();
    }

    private void UpdateBackGround()
    {
        var backgroundMaterial = _meshRenderer.material;
        var newBackgroundOffset = backgroundMaterial.mainTextureOffset;

        newBackgroundOffset.x = transform.position.x / transform.localScale.x / _parallax;
        newBackgroundOffset.y = transform.position.y / transform.localScale.y / _parallax;
        backgroundMaterial.mainTextureOffset = newBackgroundOffset;
    }
}
