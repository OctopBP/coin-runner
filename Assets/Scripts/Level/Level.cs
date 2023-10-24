using System;
using CoinRunner.Helpers;
using CoinRunner.Track;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CoinRunner.Level
{
	/// <summary>
	/// Represents the level in the game. Contains all references to components and objects.
	/// A <see cref="Model"/> class controls level setup and logic. It manage to update character and track.
	/// </summary>
	public class Level : MonoBehaviour
	{
		[SerializeField] Character.CharacterFactory characterFactory;
		[SerializeField] Button startButton;
		[SerializeField] Camera mainCamera;

		[SerializeField] TriggerEnterSubject finishTrigger;

		[SerializeField] TrackBuilder trackBuilder;
		
		public class Model
		{
			readonly Level backing;
			readonly Character.Character.Model characterModel;
			readonly TrackBuilder.Model trackBuilderModel;
			public readonly IObservable<Unit> onFinish;

			public Model(Level backing)
			{
				this.backing = backing;

				characterModel = backing.characterFactory.CreateCharacter();
				trackBuilderModel = backing.trackBuilder.CreateTrackBuilder();

				onFinish = backing.finishTrigger.onTriggerEnter.AsUnitObservable();
				onFinish.Subscribe(_ => characterModel.Stop()).AddTo(backing);
				
				backing.mainCamera.transform.SetParent(characterModel.backing.transform);
				
				backing.startButton.gameObject.SetActive(true);
				backing.startButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(backing);
				
				// Subscribe to update in every frame.
				Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(backing);
			}

			void StartGame()
			{
				characterModel.Start();
				trackBuilderModel.Build();
				backing.startButton.gameObject.SetActive(false);
			}

			void Update() => characterModel.Update();
		}
	}
}