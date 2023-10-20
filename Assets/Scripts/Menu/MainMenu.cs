using CoinRunner.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CoinRunner.Menu
{
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
			startButton.onClick.AddListener(StartGame);
		}

		void StartGame() => sceneLoader.LoadGameScene();
	}
}
