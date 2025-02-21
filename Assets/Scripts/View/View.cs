using DG.Tweening;
using Game.Presenter;
using Game.Service;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.View
{
    public abstract class View : MonoBehaviour
    {
        protected Canvas canvas;
        public bool IsShow;

        public virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        public virtual void Show() 
        {
            IsShow = true;
            canvas.enabled = true;
        }

        public virtual void Hide()
        {
            IsShow = false;
            canvas.enabled = false;
        }

        public virtual void TriggerEnter() { }
        public virtual void TriggerExit() { }

        public void AnimateTextName(ref Sequence sequence, TextMeshProUGUI textName)
        {
            if (sequence == null)
            {
                sequence = DOTween.Sequence();
                sequence.SetAutoKill(false);
                sequence.SetLoops(-1, LoopType.Yoyo);

                sequence.Append(textName.DOFade(0.9f, 1.5f));
            }
            else
            {
                sequence.Restart();
            }
        }
    }
}
