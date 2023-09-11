using Infrastructure.Constants;
using Infrastructure.Services.TextureDrawService;
using UnityEngine;
using Zenject;

public class ShaderTextureDrawer : MonoBehaviour
{
    [SerializeField] private float _drawSize;
    [SerializeField] private Texture2D _drawTexture;

    private ITextureDrawService _textureDrawService;

    [Inject]
    public void Construct(ITextureDrawService textureDrawService) =>
        _textureDrawService = textureDrawService;

    private void OnTriggerEnter(Collider collider)
    {
        if (IsDrawable(collider))
            RaycastAndDraw();
    }

    private static bool IsDrawable(Collider collider) =>
        collider.tag == Tags.DRAWABLE_TAG;

    private void RaycastAndDraw()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit))
        {
            if (HitCollider(hit))
                _textureDrawService.DrawTexture(hit, _drawSize, _drawTexture);
        }
    }

    private static bool HitCollider(RaycastHit hit) =>
        hit.collider != null;
}