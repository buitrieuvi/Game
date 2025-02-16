using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public abstract class ButtonBase : MonoBehaviour 
    {
        public Button Btn { get; set; }
        public CanvasGroup Cvg { get; set; }
        public RectTransform Rect { get; set; }

        public virtual void Awake()
        {
            Btn = GetComponent<Button>();
            Cvg = GetComponent<CanvasGroup>();
            Rect = GetComponent<RectTransform>();
        }
    }
}
