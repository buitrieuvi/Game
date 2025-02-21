using Game.Model;
using Game.Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Presenter
{
    public class Minigame2048 : MonoBehaviour 
    {
        [Inject] private InputService input;

        private int size = 4;
        private MiniGame2048 model;

        public Transform Grid;
        private List<Cell2048> cells = new List<Cell2048>();

        [SerializeField] private Vector2 dir;

        public void Awake()
        {
            model = new MiniGame2048(size);

            foreach (Transform c in Grid)
            {
                cells.Add(c.GetComponent<Cell2048>());
            }

            ReUpdate();

            input.Player.Move.started += Move;
        }

        private void Move(InputAction.CallbackContext context)
        {
            dir = context.ReadValue<Vector2>();

            model.Move(dir);
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
                        cells[i * size + j].Text.text = "0";
                        cells[i * size + j].Canv.enabled = false;
                    }
                    else
                    {
                        cells[i * size + j].Text.text = value.ToString();
                        cells[i * size + j].Canv.enabled = true;
                    }
                }
            }
        }
    }
}
