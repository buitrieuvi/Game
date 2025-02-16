using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Service;
using Game.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presenter
{
    public class AppleTree : Controller, 
        IShowHide,
        ITrigger
    {
        [SerializeField] private AppleTreeView _view;

        public void Awake()
        {
            view = _view;
        }

        public override void Input(InputAction.CallbackContext context)
        {
            base.Input(context);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public override void TriggerEnter()
        {
            base.TriggerEnter();
            _view.TriggerEnter();
        }

        public override void TriggerExit()
        {
            base.TriggerExit();
            _view.TriggerExit();
        }
    }
}
