using Game;

namespace CoinRunner.Character
{
	/// <summary> Character running forward with speed from <see cref="Config"/>. </summary>
	[GenConstructor]
	public partial class RunState: ICharacterState
	{
		readonly Character.Model character;
		readonly Config config;
		
		public void OnEnter()
		{
			character.animation.SetState(CharacterAnimation.AnimationState.Run);
		}
	
		public void Update()
		{
			character.Move(speed: config._characterSpeed);
		}
		
		public void OnExit() { }
	}
}