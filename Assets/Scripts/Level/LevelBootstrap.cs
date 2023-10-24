using System;
using CoinRunner.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace CoinRunner.Level
{
	/// <summary> Inits and starts the levels. </summary>
	public class LevelBootstrap : MonoBehaviour
	{
		[SerializeField] Level level;
		
		ISceneLoader sceneLoader;

		[Inject]
		void Construct(ISceneLoader sceneLoader)
		{
			this.sceneLoader = sceneLoader;
		}

		void Start()
		{
			SetupLevel();
		}

		void SetupLevel()
		{
			var levelModel = new Level.Model(level);
			
			// On level finish wait for 1 second and load menu scene.
			levelModel.onFinish.Subscribe(_ =>
				Observable
					.Timer(TimeSpan.FromSeconds(1))
					.Subscribe(_ => sceneLoader.LoadMenuScene())
					.AddTo(this)
			);
		}
	}
}