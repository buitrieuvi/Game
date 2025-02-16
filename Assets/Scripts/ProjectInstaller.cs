using DG.Tweening;
using Game.Model;
using Game.Presenter;
using Game.Service;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        //[SerializeField] private LayerService _layer;

        //[SerializeField] private GameObject player;

        [SerializeField] private GameObject _uiManager;
        [SerializeField] private GameObject _loading;

        //[SerializeField] private GameObject _menu;

        public override void InstallBindings()
        {
            //Debug.Log("Init Project Installer");
            DOTween.Init();
            Application.targetFrameRate = 144;

            //Container.Bind<IShowHide>().AsSingle();

            Container.Bind<InputService>().AsSingle().NonLazy();

            //Container.Bind<LayerService>().FromInstance(_layer).AsSingle();

            //Container.Bind<Inventory>().AsTransient();
            //Container.Bind<Inventory.Slot>().AsTransient();

            //Container.Bind<Player>().FromComponentInNewPrefab(player).AsSingle();

            //Container.Bind<Menu>().AsSingle();

            Container.Bind<UIManager>().FromComponentInNewPrefab(_uiManager).AsSingle().NonLazy();
            Container.Bind<Loading>().FromComponentInNewPrefab(_loading).AsSingle().NonLazy();
            
            //Container.Bind<Menu>().FromComponentInNewPrefab(_menu).AsSingle().NonLazy();

            //Container.Bind<Loading>().FromComponentInHierarchy().AsSingle();
        }
    }
}
