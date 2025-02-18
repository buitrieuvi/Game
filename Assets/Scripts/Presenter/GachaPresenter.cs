using Game.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presenter
{
    [Serializable]
    public class GachaPresenter : MonoBehaviour 
    {
        [SerializeField] private Gacha _gacha = new Gacha();

        public ItemSO GachaItem()
        {
            return _gacha.GetItemSO();
        }
    }
}
