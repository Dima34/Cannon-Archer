using UnityEngine;

namespace Infrastructure.Services.TextureDrawService
{
    public interface ITextureDrawService
    {
        public void DrawTexture(RaycastHit hit, float drawSize, Texture drawTexture);
        void InitializeShaderDrawable(GameObject drawable);
    }
}