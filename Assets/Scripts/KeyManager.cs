using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace Utils
{
	using KeyHash = Hashtable;
	using KeyTable = Dictionary<string, List<List<KeyCode>>>;
	using ActionTable = Dictionary<Dictionary<string, List<List<KeyCode>>>, List<UnityAction>>;

	/// <summary>
	/// キーボードの入力管理
	/// ※同値の別インスタンスをキーとして使用すると別キーとして登録される
	/// </summary>
	public class KeyManager : MonoBehaviour
	{
		public static string Shift = "LeftShift|RightShift,";
		public static string Control = "LeftControl|RightControl,";

		ActionTable actionTable = new ActionTable();

		public void AddKeyEvent(KeyTable table, UnityAction action)
		{
			if(!actionTable.ContainsKey(table))
			{
				actionTable.Add(table, new List<UnityAction>());
			}

			actionTable[table].Add(action);
		}

		void Update()
		{
			foreach(var key in actionTable.Keys)
			{
				if (!KeyTableResult(key))
					continue;

				foreach(var action in actionTable[key])
				{
					action();
				}
			}
		}

		private bool KeyTableResult(KeyTable table)
		{
			bool result = true;

			foreach(var key in table.Keys)
			{
				var typeFlg = true;
				switch (key)
				{
					case "on":
						foreach(var codes in table[key])
						{
							var codeFlg = false;
							foreach (var code in codes)
							{
								codeFlg |= Input.GetKey(code);
							}

							typeFlg &= codeFlg;
						}
                        break;
					case "off":
						foreach (var codes in table[key])
						{
							var codeFlg = false;
							foreach (var code in codes)
							{
								codeFlg |= !Input.GetKey(code);
							}

							typeFlg &= codeFlg;
						}
						break;
					case "down":
						foreach (var codes in table[key])
						{
							var codeFlg = false;
							foreach (var code in codes)
							{
								codeFlg |= Input.GetKeyDown(code);
							}

							typeFlg &= codeFlg;
						}
						break;
					case "up":
						foreach (var codes in table[key])
						{
							var codeFlg = false;
							foreach (var code in codes)
							{
								codeFlg |= Input.GetKeyUp(code);
							}

							typeFlg &= codeFlg;
						}
						break;
					default:
						throw new ArrayTypeMismatchException();
				}

				result &= typeFlg;
            }

			return result;
		}

		static public KeyTable Hash(params object[] args)
		{
			if (args.Length % 2 != 0)
				throw new IndexOutOfRangeException();

			Hashtable result = new Hashtable();
			for (int i = 0; i < args.Length; i += 2)
			{
				if (!(args[i] is string && args[i + 1] is string))
					throw new ArrayTypeMismatchException();

				result.Add(args[i], args[i + 1]);
			}

			return HashToKeyTable(result);
		}

		static private KeyTable HashToKeyTable(KeyHash hash)
		{
			KeyTable result = new KeyTable();

            foreach (string key in hash.Keys)
			{
				List<List<KeyCode>> outMethods = new List<List<KeyCode>>();

				// 書式：A,B|C,D|E|F,G
				string keyMethod = hash[key] as string;

				List<string> keyMethodItems = new List<string>(keyMethod.Split(','));
				foreach(var methodItem in keyMethodItems)
				{
					List<KeyCode> outCodes = new List<KeyCode>();
					var keyCodes = methodItem.Split('|');
					foreach(var k in keyCodes)
					{
						// 文字列をKeyCodeに変換
						if(k != "")
							outCodes.Add((KeyCode)Enum.Parse(typeof(KeyCode), k));
					}

					if(outCodes.Count != 0)
						outMethods.Add(outCodes);
				}

				result.Add(key.ToLower(), outMethods);
			}

			return result;
		}
	}
}