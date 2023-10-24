using UniRx;
using UnityEngine;

namespace CoinRunner.Character
{
	/// <summary> Responsible for switching character animations. </summary>
	[GenConstructor]
	public partial class CharacterAnimation
	{
		public enum AnimationState
		{
			/// <summary> Default state. Character just standing and doing some alive staff. </summary>
			Idle,
			/// <summary> Character just running forward. </summary>
			Run,
			/// <summary> Character are flying in the air. How? He have a superpower! </summary>
			Fly,
			/// <summary> Character running forward but much slower than <see cref="Run"/>. </summary>
			SlowRun,
			/// <summary> Character running forward but faster than <see cref="Run"/>. </summary>
			FastRun
		}
		
		readonly ReactiveProperty<AnimationState> animationStateRx = new(AnimationState.Idle);

		public CharacterAnimation(Animator animator)
		{
			animationStateRx.Subscribe(newState => animator.SetTrigger(newState.ToString()));
		}
		
		public void SetState(AnimationState animationState) => animationStateRx.Value = animationState;
	}
}