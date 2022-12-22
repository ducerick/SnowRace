using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader SnowFallShader;
    private Material _snowFallMat;
    private MeshRenderer _meshRenderer;
    [Range(0.001f, 0.1f)]
    public float _flakeAmount;
    [Range(0f, 1f)]
    public float _flakeOpacity;


    // Start is called before the first frame update
    void Start()
    {
        _snowFallMat = new Material(SnowFallShader);
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            _snowFallMat.SetFloat("_FlakeAmount", _flakeAmount);
            _snowFallMat.SetFloat("_FlakeOpacity", _flakeOpacity);
            RenderTexture snow = (RenderTexture)_meshRenderer.material.GetTexture("_Splat");
            RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(snow, temp, _snowFallMat);
            Graphics.Blit(temp, snow);
            _meshRenderer.material.SetTexture("_Splat", snow);
            RenderTexture.ReleaseTemporary(temp);
    }
}
