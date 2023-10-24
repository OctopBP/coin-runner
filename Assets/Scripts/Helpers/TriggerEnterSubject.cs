using UniRx;
using UnityEngine;

namespace CoinRunner.Helpers
{
	/// <summary>
	/// Provides a convenient way to notify about trigger enter events.
	/// The <see cref="TriggerEnterSubject"/> object can be used to subscribe to these events.
	/// </summary>
	public class TriggerEnterSubject : MonoBehaviour
	{
		public readonly Subject<Collider> onTriggerEnter = new();
		void OnTriggerEnter(Collider other) => onTriggerEnter.OnNext(other);
	}
}
