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
        private List<Cell2048> cells = new List<Cell2048>();

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
                        cells[i * size + j].Text.text = "0";
                        cells[i * size + j].Canv.alpha = 0f;
                    }
                    else
                    {
                        cells[i * size + j].Text.text = value.ToString();
                        cells[i * size + j].Canv.alpha = 1f;
                    }
                }
            }
        }

        public override void Show()
        {
            base.Show();

            model = new Model.Minigame2048(size);

            foreach (Transform c in Grid)
            {
                cells.Add(c.GetComponent<Cell2048>());
            }

            ReUpdate();

            input.Ui.Move.started += Move;
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
