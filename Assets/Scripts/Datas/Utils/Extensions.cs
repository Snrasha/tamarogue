using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Datas.Utils
{
    public static class Extensions 
    {
		public static float Pow(this float f, float p)
		{
			return Mathf.Pow(f, p);
		}

		public static float PowIfLessThanOne(this float f, float p)
		{
			if (!(f < 1f))
			{
				return f;
			}
			return Mathf.Pow(f, p);
		}

		public static bool IsZero(this float f)
		{
			return Mathf.Abs(f) < 1E-05f;
		}

		public static bool IsNotZero(this float f)
		{
			return Mathf.Abs(f) > 1E-05f;
		}
	}
}