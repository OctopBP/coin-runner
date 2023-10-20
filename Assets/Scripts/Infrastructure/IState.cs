namespace CoinRunner.Infrastructure
{
	public interface IState
	{
		void OnEnter();
		void OnExit();
	}
}