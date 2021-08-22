using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] WallsManager wallsManager;
    [SerializeField] private GameObject _ballPrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.Bind<WallsManager>().FromInstance(wallsManager).AsSingle();
        Container.Bind<PlayerBall>().FromComponentInNewPrefab(_ballPrefab).AsSingle();
    }
}