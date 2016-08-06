using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace Utils
{
	public class ImageLoaderMng : MonoBehaviour
	{
		[SerializeField]
		private bool dummyCreateMode = false;

		private List<LanguageMode> loaderComponents = new List<LanguageMode>();

		void Awake()
		{
			loaderComponents = new List<LanguageMode>(GameObject.FindObjectsOfType<LanguageMode>());

			foreach (var loader in loaderComponents)
			{
				loader.dummyCreateMode = dummyCreateMode;
			}
		}
	}
}
