using System;
using System.Collections.Generic;
using System.Web.WebPages;
using System.Linq;

namespace bootstrapgenerator.Code
{
	public static class RazorExtensions
	{
		public static HelperResult List<T>(this IEnumerable<T> items, Func<T, HelperResult> template)
		{
			return new HelperResult(writer =>
			{
				foreach (var item in items)
				{
					template(item).WriteTo(writer);
				}
			});
		}


		public static HelperResult List<T>(this IEnumerable<T> items, int step, int count, Func<T, HelperResult> template)
		{
			return new HelperResult(writer =>
										{
											var arr = items.ToArray();
											for (int i = step * count; i < step * count + count; i++)
											{
												if (i >= arr.Length ) return;
												template(arr[i]).WriteTo(writer);
											}
										});
		}
	}
}