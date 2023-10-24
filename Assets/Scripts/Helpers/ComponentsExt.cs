using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace CoinRunner.Helpers
{
	public static class ComponentsExt
	{
		public static Option<T> MaybeComponent<T>(this Component self) =>
			self.gameObject.TryGetComponent<T>(out var component) ? component : None;
	}
}