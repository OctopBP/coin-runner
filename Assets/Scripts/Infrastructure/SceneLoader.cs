using UnityEngine.SceneManagement;

namespace CoinRunner.Infrastructure
{
	/// <summary> Load scenes by its names. </summary>
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