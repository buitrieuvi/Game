using Game.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presenter
{
    [Serializable]
    public class GachaPresenter : MonoBehaviour 
    {
        public Gacha Gacha = new Gacha();

        public void Awake()
        {
            Gacha.GetItemSO();
            Gacha.GetItemSO();
            Gacha.GetItemSO();
            Gacha.GetItemSO();
            Gacha.GetItemSO();
        }

        public void Start()
        {
            Gacha.ToJson();
        }
    }
}
