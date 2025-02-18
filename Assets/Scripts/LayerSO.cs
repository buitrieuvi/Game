using Game.Service;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LayerSO", menuName = "Installers/LayerSO")]
public class LayerSO : ScriptableObjectInstaller<LayerSO>
{
    [SerializeField] private LayerService _layer;
    public override void InstallBindings()
    {
        Container.Bind<LayerService>().FromInstance(_layer).AsSingle();
    }
}
