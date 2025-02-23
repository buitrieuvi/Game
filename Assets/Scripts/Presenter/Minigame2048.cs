using Cysharp.Threading.Tasks;
using Game.Service;
using Game.View;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Presenter
{
    public class Minigame2048 : Controller,
        IShowHide,
        ITrigger
    {
        [SerializeField] private Minigame2048View _view;
        private int size = 4;
        private Model.Minigame2048 model;

        public Transform Grid;
        private List<Cell2048View> cells = new List<Cell2048View>();

        [SerializeField] private Vector2 dir;

        private void Move(InputAction.CallbackContext context)
        {
            dir = context.ReadValue<Vector2>();

            model.Move(dir);
            ReUpdate();
        }

        public void Awake()
        {
            view = _view;

            _view.BtnReStart.onClick.AddListener(ReStart);

            foreach (Transform c in Grid)
            {
                cells.Add(c.GetComponent<Cell2048View>());
            }

            ReStart();
        }

        public void ReStart() 
        {
            model = new Model.Minigame2048(size);
            Gen22();
            Gen22();
            ReUpdate();
        }

        public void ReUpdate() 
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = model.Map[i, j];
                    if (value == 0)
                    {
                        if (cells[i * size + j].Text.text != "0") 
                        {
                            cells[i * size + j].AnimateGenHide();
                        }
                        cells[i * size + j].Text.text = "0";

                    }
                    else
                    {

                        if (cells[i * size + j].Text.text != value.ToString())
                        {
                            cells[i * size + j].AnimateGenShow();
                        }
                        cells[i * size + j].Text.text = value.ToString();
                    }
                }
            }
        }

        public override void Show()
        {
            ReUpdate();

            base.Show();
            input.Ui.Move.started += Move;
        }

        public void Gen22() 
        {
            model.Gen22();
            cells[model.GenIndex].AnimateGenShow();
        }

        public override void Hide()
        {
            base.Hide();
            input.Ui.Move.started -= Move;
        }

        public override void TriggerEnter()
        {
            base.TriggerEnter();
        }

        public override void TriggerExit()
        {
            base.TriggerExit();
        }
    }
}
