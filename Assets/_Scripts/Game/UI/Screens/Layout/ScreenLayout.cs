using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Provider
{
    public class ScreenLayout : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private readonly List<BaseScreen> _screens = new();

        public Canvas Canvas => _canvas;
        public ScreenLayoutType LayoutType { get; private set; }
        
        public List<BaseScreen> Screens => _screens;
        
        public void Setup(ScreenLayoutType layoutType, int canvasSortingOrder)
        {
            _canvas.sortingOrder = canvasSortingOrder;
            LayoutType = layoutType;
        }
        
        public void AddScreen(BaseScreen screen)
        {
            if (_screens.Contains(screen))
                return;
            
            _screens.Add(screen);
        }

        public void RemoveScreen(BaseScreen screen)
        {
            if (_screens.Contains(screen) == false)
                return;
            
            _screens.Remove(screen);
        }

        public void DestroyLayout()
        {
            ClearScreens();
            Destroy(gameObject);
        }

        private void ClearScreens()
        {
            foreach (var screen in _screens) 
                Destroy(screen);
            
            _screens.Clear();
        }

        private void OnDestroy()
        {
            ClearScreens();
        }
    }
}