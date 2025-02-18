using System;
using UnityEngine;

namespace Game.Model
{
    public class AppleTree 
    {

    }

    [Serializable]
    public class Item
    {
        public enum Rank 
        {
            None,
            S,
            A,
            B
        }

        public string Id;
        public string Name;
        public Rank RankItem;
        public Sprite Icon;
    }
}
