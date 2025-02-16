using DG.Tweening;
using Game.Model;
using Game.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class PlayerInventoryView : View, 
        IShowHide
    {
        [SerializeField] private List<InventorySlotView> _listSlot;
        [SerializeField] private RectTransform _content;

        [SerializeField] private float _nextTimeInsert;

        private Sequence seqInventory;

        public override void Awake()
        {
            base.Awake();
            foreach (Transform slot in _content.transform) 
            {
                _listSlot.Add(slot.GetComponent<InventorySlotView>());
            }
        }

        public override void Show()
        {
            base.Show();
            AnimateInventory(ref seqInventory);
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void UpdateUI(Inventory inventory)
        {
            int count = inventory.Items.Count;
            int index = 0;

            for (int i = 0; i < count; i++)
            {
                _listSlot[i].Init(inventory.Items[i]);
                index++;
            }

            while (true)
            {
                if (_listSlot[index].Slot.Id != "")
                {
                    _listSlot[index].Init(null);
                    index++;
                }
                else
                {
                    break;
                }
            }
        }

        public void AnimateInventory(ref Sequence sequence)
        {
            float time = 0f;

            if (sequence == null)
            {
                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);

                foreach (InventorySlotView slot in _listSlot)
                {
                    slot.transform.localScale = new Vector3(0f, 0f, 0f);
                    Tween tween = slot.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
                    sequence.Insert(time, tween);
                    time += _nextTimeInsert;
                }
            }
            else
            {
                sequence.Restart();
            }
        }
    }
}
