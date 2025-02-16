using Game.Presenter;
using Game.Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.View
{
    public class UIManager : MonoBehaviour
    {
        public UiState Ui { get; private set; }

        [Inject] private readonly InputService _input;
        [Inject] private readonly Loading _loading;

        public Menu Menu { get; set; }
        public PlayerInventory PlayerInventory { get; set; }
        public AppleTree AppleTree { get; set; }

        public Stack<Controller> Controller { get; private set; } = new Stack<Controller>();
        public Controller Ctrl { get; set; }

        public void Awake()
        {
            Ui = new UiState(this);
            Ui.ChangeState(Ui.Menu);

            Debug.Log($"Init UiState");

            _input.Ui.Interac.started += InputInterac;
            _input.Ui.ESC.started += InputExit;
        }

        private void InputInterac(InputAction.CallbackContext context)
        {
            Ctrl?.Input(context);
        }

        private void InputExit(InputAction.CallbackContext context)
        {

            if (Controller.Count == 0)
            {
                Menu.Input(context);
                return;
            }
            Controller c = Controller.Peek();
            c.Input(context);
        }

        public void Push(Controller v)
        {
            if (Controller.Count == 0)
            {
                Controller.Push(v);
                v.Show();
                return;
            }

            if (v.gameObject.tag == "Menu")
            {
                Controller.Pop().Hide();
                return;
            }

            if (Controller.Peek() == v)
            {
                Controller.Pop().Hide();
                return;
            }
        }


    }
    public class UiState : StateMachine
    {
        public UIManager App { get; }

        public UiMenu Menu { get; }

        public UiState(UIManager app)
        {
            App = app;

            Menu = new UiMenu(this);
        }
    }
    public class UiStates : IState
    {
        protected UiState app;

        public UiStates(UiState app)
        {
            this.app = app;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void OntriggerEnter(Collider coll)
        {

        }

        public virtual void OntriggerExit(Collider coll)
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Update()
        {

        }
    }
    public class UiMenu : UiStates
    {
        public UiMenu(UiState game) : base(game)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

