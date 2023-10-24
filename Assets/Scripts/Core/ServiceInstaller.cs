using CoinRunner.Infrastructure;
using Game;
using UnityEngine;
using Zenject;

namespace CoinRunner.Core
{
	/// <summary>
	/// Service installer for Zenject.
	/// Bind all base types and services.
	/// </summary>
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField] Config config;
		[SerializeField] string gameSceneName, menuSceneName;
		
		public override void InstallBindings()
		{
			BindSceneLoader();
			Container.Bind<Config>().FromInstance(config);
			Container.Bind<System.Random>().FromInstance(new(Seed: 1));
		}

		void BindSceneLoader()
		{
			var sceneLoader = new SceneLoader(gameSceneName, menuSceneName);
			Container.Bind<ISceneLoader>().FromInstance(sceneLoader);
		}
	}
}