using UnityEngine;
using System.Collections;

using System;

public class PointConstraint : MonoBehaviour
{
	protected Vector3 initPosition;
	public Vector3 InitPosition
	{
		get { return initPosition; }
	}

	[SerializeField]
	protected PointConstraint parentObject = null;
	[SerializeField]
	private bool attachParent = false;

	void Awake()
	{
		initPosition = (gameObject.transform as Transform).position;
		OnAwake();
	}

	protected virtual void OnAwake()
	{
    }

	void Update()
	{
		if (parentObject == null)
			return;

		if (attachParent)
		{
			gameObject.transform.position = parentObject.transform.position;
			gameObject.transform.rotation = parentObject.transform.rotation;
			return;
		}
	}
}
