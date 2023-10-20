using CoinRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace CoinRunner.Core
{
    public class Bootstrap : MonoBehaviour
    {
        ISceneLoader sceneLoader;
		
        [Inject]
        void Construct(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }
		
        void Start()
        {
            LoadMenu();
        }
        
        void LoadMenu() => sceneLoader.LoadMenuScene();
    }
}