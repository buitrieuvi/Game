using Cysharp.Threading.Tasks;
using Game.Model;
using Game.Service;
using Game.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using static Game.Model.Inventory;

namespace Game.Presenter
{
    public abstract class InventoryBase : Controller
    {
        public abstract void UpdateInventory(Slot slot);
        public abstract void UpdateInventoryUI();
    }

    public class PlayerInventory : InventoryBase,
        IShowHide
    {
        [SerializeField] private PlayerInventoryView _view;
        [Inject] private Inventory _inventory;

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

        public override void UpdateInventory(Slot slot)
        {
            _inventory.UpdateInventory(slot);
        }

        public override void UpdateInventoryUI()
        {
            _view.UpdateUI(_inventory);
        }
    }
}

