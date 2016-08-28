using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace Utils
{
	public enum Lang
	{
		JPN,
		ENG
	}

	public class LanguageMode : ImageLoader
	{
		public override string ParentAbsPath
		{
			get
			{
				return Mode.ToString() + "/" + base.ParentAbsPath;
			}
		}

		private Lang mode = Lang.JPN;
		public Lang Mode
		{
			get { return mode; }
			set
			{
				mode = value;
				LoadSprite();
			}
		}
	}
}