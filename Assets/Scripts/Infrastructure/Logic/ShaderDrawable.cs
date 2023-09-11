using System;
using Infrastructure.Constants;
using Infrastructure.Services.TextureDrawService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class ShaderDrawable : MonoBehaviour
    {
        private ITextureDrawService _textureDrawService;

        [Inject]
        public void Construct(ITextureDrawService textureDrawService) =>
            _textureDrawService = textureDrawService;

        private void Start()
        {
            if (!HasShaderDrawableTag())
                ThrowMissingTagError();
            
            _textureDrawService.InitializeShaderDrawable(gameObject);
        }

        private void ThrowMissingTagError() =>
            throw new Exception($"Shader drawable object {name} dont have '{Tags.DRAWABLE_TAG}' tag");

        private bool HasShaderDrawableTag() =>
            tag == Tags.DRAWABLE_TAG;
    }
}