using Game.View;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Service
{
    public class InputService
    {
        public InputSystem_Actions Actions { get; private set; }
        public InputSystem_Actions.PlayerActions Player { get; private set; }
        public InputSystem_Actions.UIActions Ui { get; private set; }

        //[Inject] private readonly UIManager _views;

        public InputService()
        {
            Actions = new InputSystem_Actions();

            Player = Actions.Player;
            Ui = Actions.UI;

            Actions.Enable();

            Debug.Log("Init: Input");

            //Ui.ESC.started += InputESC;
            //Ui.Interac.started += InputInterac;
        }

        //private void InputInterac(InputAction.CallbackContext context)
        //{
        //    if (_views.Ctrl == null) { return; }

        //    _views.Push(_views.Ctrl);
        //}

        //private void InputESC(InputAction.CallbackContext context)
        //{
        //    if (_views.Controller.Count == 0) 
        //    {
        //        _views.Push(_views.Menu);
        //        return; 
        //    }

        //    _views.Push(_views.Controller.Peek());
        //}

        public void CursorOn()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CursorOff()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Activate(string key)
        {
            switch (key)
            {
                case "playerMovement":
                    Player.Enable();
                    break;
                case "Controller":
                    Ui.Enable();
                    break;
                default:
                    Actions.Enable();
                    break;
            }

        }

        public void Deactivate(string key)
        {
            switch (key)
            {
                case "playerMovement":
                    Player.Disable();
                    break;
                case "Controller":
                    Ui.Disable();
                    break;
                default:
                    Actions.Disable();
                    break;
            }

        }


    }
}