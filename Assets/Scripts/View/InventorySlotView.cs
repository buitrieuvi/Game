using DG.Tweening;
using Game.Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.View
{
    public class InventorySlotView : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _icon;
        [SerializeField] private CanvasGroup _cvg;
        [SerializeField] private Image _rank;

        public Image Icon => _icon;
        public Image Rank => _rank;

        private Sequence _seqHover;

        [Inject] public Inventory.Slot Slot;

        public void Init(Inventory.Slot slot)
        {
            Slot = slot;

            if (Slot != null)
            {
                _text.text = Slot.Quantity.ToString();

                _btn.interactable = true;
                _cvg.alpha = 1f;
                return;
            }

            _text.text = "";
            _btn.interactable = false;
            _cvg.alpha = 0f;

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_btn.interactable) { return; }

            if (_seqHover == null)
            {
                _seqHover = DOTween.Sequence();
                _seqHover.SetAutoKill(false);

                _seqHover.Append(_btn.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.15f));
            }
            else 
            {
                _seqHover.PlayForward();
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_btn.interactable) { return; }

            _seqHover.PlayBackwards();
        }
    }
}
