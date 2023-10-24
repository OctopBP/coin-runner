using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinRunner.Helpers
{
	public static class EnumerableExt
	{
		/// <summary> Return random element from the list. </summary>
		public static T GetRandomElement<T>(this IEnumerable<T> list, Random random) => list.Any()
			? list.ElementAt(random.Next(list.Count()))
			: default;
	}
}