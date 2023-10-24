using System;
using Game;
using UniRx;

namespace CoinRunner.Character
{
	/// <summary>
	/// Same as <see cref="RunState"/>, but with boosted speed.
	/// Should return to <see cref="RunState"/> after duration of the effect ends.
	/// </summary>
	[GenConstructor]
	public partial class BoostedRunState: ICharacterState
	{
		readonly StateMachine stateMachine;
		readonly Character.Model character;
		readonly Config config;

		[GenConstructorIgnore] IDisposable flyTimer;
		
		public void OnEnter()
		{
			character.animation.SetState(CharacterAnimation.AnimationState.FastRun);

			// Start timer and change state to default on complete
			flyTimer = Observable
				.Timer(TimeSpan.FromSeconds(config._speedBoostEffect._duration))
				.Subscribe(_ => stateMachine.EnterState<RunState>());
		}
		
		public void Update()
		{
			var speed = config._characterSpeed * config._speedBoostEffect._speedMultiplier;
			character.Move(speed);
		}
		
		public void OnExit()
		{
			flyTimer.Dispose();
		}
	}
}