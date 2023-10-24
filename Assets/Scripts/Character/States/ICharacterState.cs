using CoinRunner.Infrastructure;

namespace CoinRunner.Character
{
	public interface ICharacterState : IState
	{
		void Update();
	}
}
