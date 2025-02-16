using UnityEngine;

namespace Game.View
{
    public class ButtonOptionMenu : ButtonBase
    {
        public override void Awake()
        {
            base.Awake();

            Cvg.alpha = 0f;
            Rect.anchoredPosition = new Vector2(-400f, Rect.anchoredPosition.y);
        }
    }
}
