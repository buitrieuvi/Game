using Game.Model;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingSO", menuName = "Installers/GameSettingSO")]
public class GameSettingSO : ScriptableObjectInstaller<GameSettingSO>
{
    [SerializeField] private GameSetting _gameSetting;

    public override void InstallBindings()
    {
        Container.Bind<GameSetting>().FromInstance(_gameSetting).AsSingle().NonLazy();
    }
}