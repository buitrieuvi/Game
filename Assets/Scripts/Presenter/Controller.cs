using Cysharp.Threading.Tasks;
using Game.Service;
using Game.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Presenter
{
    public abstract class Controller : MonoBehaviour,
        IShowHide,
        ITrigger
    {
        [Inject] public readonly Player player;
        [Inject] public readonly InputService input;
        [Inject] public readonly UIManager ui;
        [Inject] public readonly Loading load;


        [SerializeField] protected float delayInput;
        [SerializeField] protected bool canInput = true;

        protected View.View view;

        public async virtual void Show()
        {
            player.playerMovement.IsFreezing(true);
            load.LoadNow();
            await UniTask.Delay((int)(load.view.TimeLoadingFadeIn * 1000f));
            view.Show();
            input.CursorOn();
        }

        public async virtual void Hide()
        {
            input.CursorOff();
            load.LoadNow();
            await UniTask.Delay((int)(load.view.TimeLoadingFadeIn * 1000f));
            view.Hide();
            await UniTask.Delay((int)(load.view.TimeLoadingFadeOut * 1000f));
            player.playerMovement.IsFreezing(false);
        }

        public virtual void TriggerEnter()
        {
            ui.Ctrl = this;
            view.TriggerEnter();
        }

        public virtual void TriggerExit()
        {
            ui.Ctrl = null;
            view.TriggerExit();
        }

        public virtual async void Input(InputAction.CallbackContext context) 
        {
            if (!canInput) 
            {
                return;
            }

            canInput = false;
            ui.Push(this);
            await UniTask.Delay((int)(delayInput * 1000f)).ContinueWith(() => { canInput = true; });
        }
    }
}
