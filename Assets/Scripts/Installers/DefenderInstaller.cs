using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DefenderInstaller", menuName = "Installers/DefenderInstaller")]
public class DefenderInstaller : ScriptableObjectInstaller<DefenderInstaller>
{
    [SerializeField]
    private DefenderData defenderData;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DefenderData>().FromInstance(defenderData).AsSingle();
        //Container.BindInterfacesAndSelfTo<Defender[]>().FromComponentInHierarchy().AsTransient();
    }
}