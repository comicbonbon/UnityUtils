using UnityEngine;
using System.Collections;
using Utils;

namespace Utils
{
	public class ConstraintBase : MonoBehaviour
	{
		protected Vector3 initPosition;
		public Vector3 InitPosition
		{
			get { return initPosition; }
		}

		protected Quaternion initRotation;
		public Quaternion InitRotation
		{
			get { return initRotation; }
		}

		protected Vector3 initScale;
		public Vector3 InitScale
		{
			get { return initScale; }
		}

		[SerializeField]
		protected ConstraintBase parentObject = null;

		void Awake()
		{
			initPosition = (gameObject.transform as Transform).position;
			initRotation = (gameObject.transform as Transform).rotation;
			initScale = (gameObject.transform as Transform).localScale;
			OnAwake();
		}

		protected virtual void OnAwake()
		{
		}
	}
}
