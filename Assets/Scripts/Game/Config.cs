using System;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "Config", menuName = "Game/Config", order = 1)]
	public partial class Config: ScriptableObject
	{
		[Header("Character")]
		[SerializeField, PublicAccessor] float characterSpeed;
		
		[Header("Effects")]
		[SerializeField, PublicAccessor] SpeedMultiplayerEffect slowEffect, speedBoostEffect;
		[SerializeField, PublicAccessor] FlyEffect flyEffect;
		
		[Header("Coin Factory")]
		[SerializeField, PublicAccessor] float spaceBetweenEffects;
		[SerializeField, PublicAccessor] float startOffset;
		[SerializeField, PublicAccessor] float spawnDelay;
		[SerializeField, PublicAccessor] int effectsInField;
		[SerializeField, PublicAccessor] int maxEffectsCount;
		
		[Serializable]
		public partial class SpeedMultiplayerEffect
		{
			[SerializeField, PublicAccessor] float duration;
			[SerializeField, PublicAccessor] float speedMultiplier;
		}
		
		[Serializable]
		public partial class FlyEffect
		{
			[SerializeField, PublicAccessor] float duration;
		}
	}
}