using Game.Model;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item Item;
}