using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.NetCore.Extensions
{
	public static class EnumerableExtension
	{
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if(source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				var elementValue = keySelector(element);
				if (seenKeys.Add(elementValue))
				{
					yield return element;
				}
			}
		}
	}
}
