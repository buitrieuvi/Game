using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public class GameSetting 
    {
        public Mouse MouseSetting;

        public GameSetting() 
        {
            MouseSetting = new Mouse();
            Debug.Log("Init GameSetting");
        }

        [Serializable]
        public class Mouse 
        {
            public Vector2 sensitivityMouse = new Vector2(17f, 17f);

            public void SetSensitivityMouse(Vector2 newSensitivityMouse) 
            {
                sensitivityMouse = newSensitivityMouse;
            }

            public Mouse() { }
        }
    }
}
