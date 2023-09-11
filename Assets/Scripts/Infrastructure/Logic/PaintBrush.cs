using System;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [SerializeField] private float _drawSize;
    [SerializeField] private Texture2D _drawTexture;

    private Texture2D _whiteMap;
    private Vector2 _stored;
    private static Dictionary<Collider, RenderTexture> _drawTextures = new Dictionary<Collider, RenderTexture>();

    private const int DRAW_RESOLUTION = 512;
    private const string DRAWTEXTURE_NAME = "_PaintMap";

    void Start()
    {
        CreateNewWhiteMap();
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastAndDraw();
    }

    private void RaycastAndDraw()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit))
        {
            if (HitCollider(hit))
                CreateTextureIfNotExistAndDraw(hit);
        }
    }

    private static bool HitCollider(RaycastHit hit) =>
        hit.collider != null;

    private void CreateTextureIfNotExistAndDraw(RaycastHit hit)
    {
        Collider coll = hit.collider;
        
        if (HasntDrawTexture(coll))
            CreateBlankDrawTexture(coll);

        DrawTexture(_drawTextures[coll], GetUVDrawCoords(hit));
    }

    private static Vector2 GetUVDrawCoords(RaycastHit hit)
    {
        Vector2 pixelUV = hit.lightmapCoord;
        pixelUV.y *= DRAW_RESOLUTION;
        pixelUV.x *= DRAW_RESOLUTION;
        return pixelUV;
    }

    private static bool HasntDrawTexture(Collider coll) =>
        !_drawTextures.ContainsKey(coll);

    void DrawTexture(RenderTexture renderTexture, Vector2 drawCoords)
    {
        SetRenderTextureForDrawing(renderTexture);
        SaveTransformationMatrix();
        SetupPixelMatrix();

        float brushPositionCorrection = _drawSize /2;
        float drawX = drawCoords.x - brushPositionCorrection;
        float drawY = renderTexture.height - drawCoords.y - brushPositionCorrection;
        
        Rect drawRect = new Rect(drawX, drawY, _drawSize, _drawSize);
        Graphics.DrawTexture(drawRect, _drawTexture);
        
        RestoreTransformationMatrix();
        RemoveDrawingRenderTexture();
    }

    private static void SetRenderTextureForDrawing(RenderTexture renderTexture) =>
        RenderTexture.active = renderTexture;

    private static void SaveTransformationMatrix() =>
        GL.PushMatrix();

    private static void SetupPixelMatrix() =>
        GL.LoadPixelMatrix(0, DRAW_RESOLUTION, DRAW_RESOLUTION, 0);

    private static void RemoveDrawingRenderTexture() =>
        RenderTexture.active = null;

    private static void RestoreTransformationMatrix() =>
        GL.PopMatrix();

    private void CreateBlankDrawTexture(Collider coll)
    {
        _drawTextures.Add(coll, GetBlankRenderTexture());

        Renderer rend = coll.transform.GetComponent<Renderer>();
        rend.material.SetTexture(DRAWTEXTURE_NAME, _drawTextures[coll]);
    }

    RenderTexture GetBlankRenderTexture()
    {
        RenderTexture rt = new RenderTexture(DRAW_RESOLUTION, DRAW_RESOLUTION, 32);
        Graphics.Blit(_whiteMap, rt);
        return rt;
    }

    void CreateNewWhiteMap()
    {
        _whiteMap = new Texture2D(1, 1);
        _whiteMap.SetPixel(0, 0, Color.white);
        _whiteMap.Apply();
    }
}