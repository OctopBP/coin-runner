using CoinRunner.Effects;
using CoinRunner.Helpers;
using UniRx;
using UnityEngine;

namespace CoinRunner.Character
{
	/// <summary>
	/// Character view. All references from unity are stored here.
	/// <br/>
	/// The entire logic of the character is processed in the <see cref="Model"/>.
	/// It gets the <see cref="Character"/> as a dependency and also has access to all references from it.
	/// </summary>
	public class Character: MonoBehaviour
	{
		[SerializeField] Animator animator;

		readonly Subject<Collider> onTriggerEnter = new();
		void OnTriggerEnter(Collider other)
		{
			onTriggerEnter.OnNext(other);
		}

		public class Model
		{
			public readonly Character backing;
			readonly StateMachine stateMachine;
			public readonly CharacterAnimation animation;

			public Model(Character backing, Game.Config config)
			{
				this.backing = backing;
				stateMachine = new(this, config);
				animation = new(backing.animator);
				
				SetupCollisionsStrategy();

				stateMachine.EnterState<IdleState>();
				
				return;

				// Handles collisions with effects and applies them to the stateMachine.
				void SetupCollisionsStrategy()
				{
					backing.onTriggerEnter.Subscribe(collider => collider
						.MaybeComponent<Effect>()
						.IfSome(effect =>
						{
							switch (effect.type)
							{
								case EffectType.FlyEffect: stateMachine.EnterState<FlyState>(); break;
								case EffectType.SlowEffect: stateMachine.EnterState<SlowWalkState>(); break;
								case EffectType.BoostEffect: stateMachine.EnterState<BoostedRunState>(); break;
							}
							
							effect.Collect();
						})
					);
				}
			}

			public void Start() => stateMachine.EnterState<RunState>();
			public void Stop() => stateMachine.EnterState<IdleState>();

			public void Update() => stateMachine.Update();
			
			public void Move(float speed) => backing.transform.position += Vector3.forward * Time.deltaTime * speed;
		}
	}
}