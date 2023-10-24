using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.SomeHelp;

namespace CoinRunner.Character
{
	/// <summary> Character state machine. </summary>
	public class StateMachine : IDisposable
	{
		readonly Game.Config config;
		readonly Dictionary<Type, ICharacterState> states = new();
		
		Option<ICharacterState> maybeCurrentState;
		
		public StateMachine(Character.Model character, Game.Config config) {
			InitStates();
			return;

			// Create cached states
			void InitStates()
			{
				AddState(new IdleState(character));
				AddState(new RunState(character, config));
				AddState(new FlyState(this, character, config));
				AddState(new SlowWalkState(this, character, config));
				AddState(new BoostedRunState(this, character, config));
				
				return;

				void AddState<TState>(TState state) where TState : ICharacterState =>
					states.Add(typeof(TState), state);
			}
		}
		
		public void EnterState<TState>() where TState: ICharacterState
		{
			var newState = states[typeof(TState)];
			EnterState(newState);
		}
		
		public void EnterState(ICharacterState newState)
		{
			maybeCurrentState.IfSome(currentState => currentState.OnExit());
			maybeCurrentState = newState.ToSome();
			newState.OnEnter();
		}

		public void Update() => maybeCurrentState.IfSome(currentState => currentState.Update());

		public void Dispose() => maybeCurrentState.IfSome(currentState => currentState.OnExit());
	}
}