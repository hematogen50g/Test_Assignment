using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EnemyInstaller", menuName = "Installers/EnemyInstaller")]
public class EnemyInstaller : ScriptableObjectInstaller<EnemyInstaller>
{
    [SerializeField]
    EnemyTemplate[] enemyTypes;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<IEnemyTemplate[]>().FromInstance(enemyTypes).AsSingle();
    }
}