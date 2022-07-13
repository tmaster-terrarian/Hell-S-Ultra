using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CSi.Utility
{
    public class Utility
    {
        public static float onePixel {get;} = 0.0625f;

        public static Vector3 RoundVector (Vector3 v, float snapValue)
	    {
		    return new Vector3
		    (
			    snapValue * Mathf.Round(v.x / snapValue),
			    snapValue * Mathf.Round(v.y / snapValue),
			    snapValue * Mathf.Round(v.z / snapValue)
		    );
	    }

		public static Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max)
		{
			return new Vector3
			(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y),
				Mathf.Clamp(value.z, min.z, max.z)
			);
		}

		public static Vector2 ClampVector2(Vector2 value, Vector2 min, Vector2 max)
		{
			return new Vector2
			(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y)
			);
		}
    }
}
