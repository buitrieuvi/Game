using DG.Tweening;
using Game.Presenter;
using Game.Service;
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
    }
}
