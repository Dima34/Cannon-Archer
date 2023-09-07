using Infrastructure.Constants;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private DiContainer _container;
        private RectTransform _rootCanvasRect;
        private GameObject _waitingHud;
        private GameObject _endgameHud;

        public UIFactory(DiContainer container)
        {
            _container = container;

            CreateCanvas();
        }

        private void CreateCanvas()
        {
            GameObject rootCanvas = _container.InstantiatePrefabResource(ResourcePaths.ROOT_CANVAS);
            _rootCanvasRect = rootCanvas.GetComponent<RectTransform>();
        }

        public void CreateHUD()
        {
            GameObject hud = _container.InstantiatePrefabResource(ResourcePaths.ROOT_CANVAS, _rootCanvasRect);
        }
    }
}