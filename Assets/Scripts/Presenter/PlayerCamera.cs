using Game.Model;
using Game.Service;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;


namespace Game.Presenter
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private List<CinemachineCamera> _cams;

        [Inject] private readonly GameSetting gameSetting;

        public void Awake()
        {
            SetSensitivityMouse(gameSetting.MouseSetting.sensitivityMouse);
        }

        public void SetLook(bool b) 
        {
            if (b) { SetSensitivityMouse(gameSetting.MouseSetting.sensitivityMouse); }
            else { SetSensitivityMouse(Vector2.zero); }
        }
        public void SetSensitivityMouse(Vector2 newSensitivityMouse)
        {
            _cams.ForEach(x => {
                var look = x.GetComponent<CinemachineInputAxisController>();

                look.Controllers.ForEach(x => {
                    if (x.Name == "Look X (Pan)")
                    {
                        x.Input.Gain = newSensitivityMouse.x;
                    }
                    if (x.Name == "Look Y (Tilt)")
                    {
                        x.Input.Gain = -newSensitivityMouse.y;
                    }
                });
            });
        }
    }
}

