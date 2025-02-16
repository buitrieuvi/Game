using Game.Model;
using Game.Presenter;
using Game.Service;
using Game.View;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Inventory>().AsTransient();
        Container.Bind<Inventory.Slot>().AsTransient();

        Container.Bind<Menu>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}

//public override void InstallBindings()
//{

//    //Container.Bind<PlayerInventoryView>().FromComponentInNewPrefab(_inventory).AsTransient();
//    //Container.BindFactory<string, int, InventorySlotView, InventorySlotView.Factory>()
//    //    .FromMonoPoolableMemoryPool(x =>
//    //            x.WithInitialSize(9)
//    //                .FromComponentInNewPrefab(_inventorySlot)
//    //                .UnderTransformGroup("Content")
//    //        );
//}