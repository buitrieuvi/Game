using Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameItemDataSO", menuName = "Installers/GameItemDataSO")]
public class GameItemDataSO : ScriptableObjectInstaller<GameItemDataSO>
{
    [SerializeField] private GameItemDatas _item;
    public override void InstallBindings()
    {
        Container.Bind<GameItemDatas>().FromInstance(_item).AsSingle();
    }

    [Serializable]
    public class GameItemDatas
    {
        public List<ItemSO> ItemSO;
        public List<RankColorSO> RankColorSO;

        public ItemSO GetItemSO(string id)
        {
            return ItemSO.FirstOrDefault(x => x.Item.Id == id);
        }

        public Color GetRankColor(Item.Rank id)
        {
            return RankColorSO.FirstOrDefault(x => x.RankColor.Rank == id).RankColor.Color;
        }
    }
}