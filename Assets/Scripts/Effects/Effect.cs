using UniRx;
using UnityEngine;

namespace CoinRunner.Effects
{
	/// <summary>
	/// Base effect type.
	/// Can be collected and contains subject to notify that it has been collected.
	/// </summary>
	public abstract class Effect : MonoBehaviour
	{
		public abstract EffectType type { get; }
		public readonly Subject<Effect> onCollect = new();

		public void Collect() => onCollect.OnNext(this);
	}
}