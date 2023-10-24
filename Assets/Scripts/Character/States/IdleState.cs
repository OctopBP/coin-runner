namespace CoinRunner.Character
{
	/// <summary> Idle state. Just standing on place. </summary>
	[GenConstructor]
	public partial class IdleState: ICharacterState
	{
		readonly Character.Model character;
		
		public void OnEnter()
		{
			character.animation.SetState(CharacterAnimation.AnimationState.Idle);
		}

		public void Update() { }
		public void OnExit() { }
	}
}