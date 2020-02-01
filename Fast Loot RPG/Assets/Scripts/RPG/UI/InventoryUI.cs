using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class InventoryUI : MonoBehaviour {
        
        [SerializeField] ScrollRect inventoryScrollRectRef;
        [SerializeField] Scrollbar inventoryScrollbarRef;
        [SerializeField] Canvas inventoryCanvasRef;


        private static ScrollRect inventoryScrollRect;
        private static Scrollbar inventoryScrollbar;

        private void Start()
        {
            inventoryScrollRect = inventoryScrollRectRef;
            inventoryScrollbar = inventoryScrollbarRef;
            inventoryCanvasRef.worldCamera = MainCamera.Instance.GetComponent<Camera>();
        }

        public static void ChangeScrollRectContent(RectTransform rectTransform) => inventoryScrollRect.content = rectTransform;
        public static void ResetSlider() => inventoryScrollbar.value = 1;
    }
}
