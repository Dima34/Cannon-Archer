using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.TextureDrawService
{
    public class TextureDrawService : ITextureDrawService
    {
        private Texture2D _whiteMap;
        private Vector2 _stored;
        private Dictionary<GameObject, RenderTexture> _drawables = new Dictionary<GameObject, RenderTexture>();

        private const int DRAW_RESOLUTION = 512;
        private const string DRAWTEXTURE_NAME = "_PaintMap";

        public TextureDrawService() =>
            CreateWhiteMap();

        public void InitializeShaderDrawable(GameObject drawable) =>
            RegisterAndInitializeDrawable(drawable);

        public void RegisterAndInitializeDrawable(GameObject gameObject)
        {
            RegisterDrawable(gameObject);
            InitializeDrawable(gameObject);
        }

        private void RegisterDrawable(GameObject gameObject) =>
            _drawables.Add(gameObject, GetBlankRenderTexture());

        private void InitializeDrawable(GameObject gameObject)
        {
            Renderer r = gameObject.transform.GetComponent<Renderer>();
            r.material.SetTexture(DRAWTEXTURE_NAME, _drawables[gameObject]);
        }

        public void DrawTexture(RaycastHit hit, float drawSize, Texture drawTexture)
        {
            RenderTexture renderTexture = _drawables[hit.collider.gameObject];
            SetRenderTextureForDrawing(renderTexture);
            SaveTransformationMatrix();
            SetupPixelMatrix();

            Vector2 drawCoords = GetUVDrawCoords(hit);
            float brushPositionCorrection = drawSize / 2;
            float drawX = drawCoords.x - brushPositionCorrection;
            float drawY = renderTexture.height - drawCoords.y - brushPositionCorrection;

            Rect drawRect = new Rect(drawX, drawY, drawSize, drawSize);
            Graphics.DrawTexture(drawRect, drawTexture);

            RestoreTransformationMatrix();
            RemoveDrawingRenderTexture();
        }

        private static Vector2 GetUVDrawCoords(RaycastHit hit)
        {
            Vector2 pixelUV = hit.lightmapCoord;
            pixelUV.y *= DRAW_RESOLUTION;
            pixelUV.x *= DRAW_RESOLUTION;
            return pixelUV;
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

        private RenderTexture GetBlankRenderTexture()
        {
            RenderTexture rt = new RenderTexture(DRAW_RESOLUTION, DRAW_RESOLUTION, 32);
            Graphics.Blit(_whiteMap, rt);
            return rt;
        }

        private void CreateWhiteMap()
        {
            _whiteMap = new Texture2D(1, 1);
            _whiteMap.SetPixel(0, 0, Color.white);
            _whiteMap.Apply();
        }
    }
}