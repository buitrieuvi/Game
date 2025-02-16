using Cysharp.Threading.Tasks;
using Game.Service;
using Game.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presenter
{
    public class PlayerInventory : Controller,
        IShowHide
    {
        [SerializeField] private PlayerInventoryView _view;

        public void Awake()
        {
            view = _view;
            input.Ui.Inventory.started += Input;
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

