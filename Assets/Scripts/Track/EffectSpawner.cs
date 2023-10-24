using CoinRunner.Effects;
using CoinRunner.Helpers;
using DG.Tweening;
using UnityEngine;

namespace CoinRunner.Track
{
	/// <summary>
	/// Layer for a pool of objects. Add logic to the appearance and hiding of the effects.
	/// Stores a pool of effects for optimization.
	/// </summary>
	class EffectSpawner
	{
		readonly Pool<Effect> pool;
		
		const float AppearanceTime = 1f;
		const float HidingTime = 0.2f;

		public EffectSpawner(Effect prefab)
		{
			pool = new(
				create: () => Object.Instantiate(prefab),
				reset: coin => coin.gameObject.SetActive(false)
			);
		}
		
		public Effect Create(Vector3 position)
		{
			var newEffect = pool.Borrow();
			
			newEffect.gameObject.SetActive(true);
			
			newEffect.transform.position = position;
			newEffect.transform.DOMove(position, AppearanceTime).SetEase(Ease.OutElastic);
			
			newEffect.transform.localScale = Vector3.zero + Vector3.down;
			newEffect.transform.DOScale(Vector3.one, AppearanceTime).SetEase(Ease.OutCubic);
			
			return newEffect;
		}

		public void Release(Effect effect)
		{
			effect.transform
				.DOScale(Vector3.zero, HidingTime).SetEase(Ease.InOutCubic)
				.OnComplete(() => pool.Release(effect));
		}
	}
}