using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBall : MonoBehaviour
{
    public Shader _drawShader;

    private RenderTexture _splatmap;
    private Material _snowMaterial, _drawMaterial;
    private RaycastHit _hit;
    public SnowBall snowBall;

    // Start is called before the first frame update
    void Start()
    {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        _snowMaterial = GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);
    }

    // Update is called once per frame
    void Update()
    {
        if (snowBall.GetScale().x > 0.3f)
        {
            if (Physics.Raycast(snowBall.transform.position, -Vector3.up , out _hit))
            {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
