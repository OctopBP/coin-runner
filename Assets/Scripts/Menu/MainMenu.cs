using CoinRunner.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CoinRunner.Menu
{
	/// <summary> Main game menu. </summary>
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] Button startButton;

		ISceneLoader sceneLoader;
		
		[Inject]
		void Construct(ISceneLoader sceneLoader)
		{
			this.sceneLoader = sceneLoader;
		}
		
		void Start()
		{
			startButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(this);
		}

		void StartGame() => sceneLoader.LoadGameScene();
	}
}
