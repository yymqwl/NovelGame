using UnityEngine;
using System.Collections;

namespace GameMain
{
	public interface FastEnumerable<T>
	{
		void Enumerate (FastList<T> output);
	}
}