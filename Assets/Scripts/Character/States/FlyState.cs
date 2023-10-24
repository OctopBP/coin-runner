using System;
using DG.Tweening;
using Game;
using UniRx;

namespace CoinRunner.Character
{
	/// <summary> In this state character fly while effect is active. </summary>
	[GenConstructor]
	public partial class FlyState: ICharacterState
	{
		readonly StateMachine stateMachine;
		readonly Character.Model character;
		readonly Config config;

		[GenConstructorIgnore] IDisposable flyTimer;
		
		public void OnEnter()
		{
			character.animation.SetState(CharacterAnimation.AnimationState.Fly);

			character.backing.transform.DOLocalMoveY(1.5f, 0.2f);
            
			// Start timer and change state to default on complete
			flyTimer = Observable
				.Timer(TimeSpan.FromSeconds(config._flyEffect._duration))
				.Subscribe(_ => stateMachine.EnterState<RunState>());
		}
		
		public void Update()
		{
			character.Move(speed: config._characterSpeed);
		}

		public void OnExit()
		{
			character.backing.transform.DOLocalMoveY(0, 0.2f);
			flyTimer.Dispose();
		}
	}
}