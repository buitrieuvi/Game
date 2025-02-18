using Game.Model;
using UnityEngine;
using Zenject;

namespace Game.Model 
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public Item Item;
    }
}
