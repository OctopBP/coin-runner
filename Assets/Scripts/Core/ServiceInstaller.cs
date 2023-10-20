using CoinRunner.Infrastructure;
using Game;
using UnityEngine;
using Zenject;

namespace CoinRunner.Core
{
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField] string gameSceneName, menuSceneName;
		
		public override void InstallBindings()
		{
			BindSceneLoader();
		}

		void BindSceneLoader()
		{
			var sceneLoader = new SceneLoader(gameSceneName, menuSceneName);
			Container.Bind<ISceneLoader>().FromInstance(sceneLoader);
		}
	}
}