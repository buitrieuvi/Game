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

        private GachaPresenter _gacha;

        public void Awake()
        {
            view = _view;
            _gacha = GetComponent<GachaPresenter>();

            _view.BtnGet.onClick.AddListener(GetApple);
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

        public void GetApple()
        {
            InventoryBase inventory = GetComponentInChildren<InventoryBase>();
            if (inventory == null) return;

            ItemSO item = _gacha.Gacha.GetItemSO();
            inventory.UpdateInventory(new Model.Inventory.Slot(item.Item.Id, 1));

            inventory.UpdateInventoryUI();
        }
    }
}
