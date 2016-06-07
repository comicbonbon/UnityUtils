using UnityEngine;
using System.Collections;

using System;

public class AnimBase : MonoBehaviour
{
	protected Vector2 initPosition;
	public Vector2 InitPosition
	{
		get { return initPosition; }
	}

	[SerializeField]
	protected CanvasGroup group = null;

	[SerializeField]
	protected AnimBase parentObject = null;

	public Action PositionCompleteEvent = null;
	public Action FadeCompleteEvent = null;

	void Awake()
	{
		initPosition = (gameObject.transform as RectTransform).anchoredPosition;
		OnAwake();
	}

	protected virtual void OnAwake()
	{
	}

	void Update()
	{
		if (parentObject == null)
			return;

		var parentTrans = parentObject.transform as RectTransform;
		var dPos = parentObject.InitPosition - parentTrans.anchoredPosition;

		(gameObject.transform as RectTransform).anchoredPosition = initPosition - dPos;
	}

	public virtual void Initialize()
	{
		(this.gameObject.transform as RectTransform).anchoredPosition = initPosition;
	}

	public void Animation(float time, Vector2 target)
	{
		iTween.tweens.Clear();

		iTween.ValueTo(this.gameObject, iTween.Hash(
			"from", (this.gameObject.transform as RectTransform).anchoredPosition,
			"to", target,
			"time", time,
			"easetype", "easeOutCubic",
			"onupdatetarget", this.gameObject,
			"onupdate", "OnUpdateAnimation",
			"oncomplete", "OnPositionCompleteProcess"));
	}

	private void OnUpdateAnimation(Vector2 pos)
	{
		(this.gameObject.transform as RectTransform).anchoredPosition = pos;
	}

	public void FadeOut(float time)
	{
		if (group == null)
			return;

		iTween.ValueTo(this.gameObject, iTween.Hash(
			"from", group.alpha,
			"to", 0f,
			"time", time,
			"easetype", "easeOutCubic",
			"onupdatetarget", this.gameObject,
			"onupdate", "OnFadeAnimation",
			"oncomplete", "OnFadeCompleteProcess"));
	}

	public void FadeIn(float time)
	{
		if (group == null)
			return;

		iTween.ValueTo(this.gameObject, iTween.Hash(
			"from", group.alpha,
			"to", 1f,
			"time", time,
			"easetype", "easeOutCubic",
			"onupdatetarget", this.gameObject,
			"onupdate", "OnFadeAnimation",
			"oncomplete", "OnFadeCompleteProcess"));
	}

	private void OnFadeAnimation(float alpha)
	{
		if (group == null)
			return;

		group.alpha = alpha;
	}

	private void OnPositionCompleteProcess()
	{
		if (PositionCompleteEvent != null)
		{
			PositionCompleteEvent();
		}
	}

	private void OnFadeCompleteProcess()
	{
		if (FadeCompleteEvent != null)
		{
			FadeCompleteEvent();
		}
	}
}
