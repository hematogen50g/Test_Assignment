using UnityEngine;
using Zenject;
using UnityEditor;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
{
    [SerializeField]
    private GameConfig gameConfig;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameConfig>().FromInstance(gameConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<Fortress>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<Waypoint>().FromComponentsInHierarchy().AsTransient();
        Container.BindInterfacesAndSelfTo<EnemyController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<UIController>().FromComponentInHierarchy().AsSingle();

    }
}