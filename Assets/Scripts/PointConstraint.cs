using UnityEngine;
using System;
using System.Collections;
using Utils;

namespace Utils
{
	public class PointConstraint : ConstraintBase
	{
		[SerializeField]
		protected bool attachParent = false;

		void Update()
		{
			if (parentObject == null)
				return;

			if (attachParent)
			{
				gameObject.transform.position = parentObject.transform.position;
			}
			else
			{
				gameObject.transform.position = parentObject.transform.position - parentObject.InitPosition + InitPosition;
			}
		}
	}
}
