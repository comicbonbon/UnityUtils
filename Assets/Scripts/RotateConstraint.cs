using UnityEngine;
using System.Collections;
using Utils;

namespace Utils
{
	public class RotateConstraint : ConstraintBase
	{
		void Update()
		{
			if (parentObject == null)
				return;

			gameObject.transform.rotation = parentObject.transform.rotation;
		}
	}
}