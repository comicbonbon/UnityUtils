using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace Utils
{
	public class Logger : MonoBehaviour
	{
		static public Logger Instance { get; private set; }

		[SerializeField]
		private Text logArea;
		[SerializeField]
		private int lineLimit = 15;

		public bool enableDebug = true;

		void Awake()
		{
			if (Instance == null) Instance = this;
		}

		void Start()
		{
			logArea.text = "";
		}

		void FixedUpdate()
		{
			var log = logArea.text;
			var items = new List<string>(log.Split('\n'));

			if (items.Count >= lineLimit)
			{
				var logs = items.GetRange(0, lineLimit);
				logArea.text = string.Join("\n", logs.ToArray());
			}
		}

		public void Log(object message)
		{
			if (enableDebug)
				logArea.text = message.ToString() + "\n" + logArea.text;
		}
	}
}