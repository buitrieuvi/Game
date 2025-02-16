using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Service;
using Game.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Presenter
{
    public class Menu : Controller, 
        IShowHide
    {
        [SerializeField] private MenuView _view;

        public void Awake()
        {
            ui.Menu = this;
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
    }
}

