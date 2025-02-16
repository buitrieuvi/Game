using Game.Presenter;
using Game.View;
using UnityEngine.EventSystems;
using Zenject;

namespace Game
{
    public class BtnExit : ButtonBase, IPointerClickHandler
    {
        [Inject] private UIManager _ui;

        public override void Awake()
        {
            base.Awake();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ui.Push(_ui.Controller.Peek());
        }
    }
}
