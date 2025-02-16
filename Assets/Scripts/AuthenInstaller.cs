using Game.Presenter;
using Zenject;

public class AuthenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Menu>().FromComponentInHierarchy().AsSingle();
    }
}