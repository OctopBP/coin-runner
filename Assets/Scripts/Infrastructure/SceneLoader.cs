using UnityEngine.SceneManagement;

namespace CoinRunner.Infrastructure
{
	[GenConstructor]
	public partial class SceneLoader: ISceneLoader
	{
		readonly string gameSceneName, menuSceneName;
		
		public void LoadGameScene() => LoadSceneByName(gameSceneName);
		public void LoadMenuScene() => LoadSceneByName(menuSceneName);

		static void LoadSceneByName(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}