using System;
using Game;
using UniRx;

namespace CoinRunner.Character
{
	/// <summary>
	/// Same as <see cref="RunState"/>, but with slower speed.
	/// Should return to <see cref="RunState"/> after duration of the effect ends.
	/// </summary>
	[GenConstructor]
	public partial class SlowWalkState: ICharacterState
	{
		readonly StateMachine stateMachine;
		readonly Character.Model character;
		readonly Config config;

		[GenConstructorIgnore] IDisposable flyTimer;
		
		public void OnEnter()
		{
			character.animation.SetState(CharacterAnimation.AnimationState.SlowRun);

			// Start timer and on end change state to default
			flyTimer = Observable
				.Timer(TimeSpan.FromSeconds(config._slowEffect._duration))
				.Subscribe(_ => stateMachine.EnterState<RunState>());
		}

		public void Update()
		{
			var speed = config._characterSpeed * config._slowEffect._speedMultiplier;
			character.Move(speed);
		}

		public void OnExit()
		{
			flyTimer.Dispose();
		}
	}
}