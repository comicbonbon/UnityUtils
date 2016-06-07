using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(Image))]
public class ImageLoader : MonoBehaviour
{
	[SerializeField]
	private string extention = "png";
	public bool dummyCreateMode = false;

	private string parentAbsPath = "";
	public virtual string ParentAbsPath
	{
		get { return parentAbsPath; }
	}

	// 名前
	private string fileName
	{
		get
		{
			if (image.sprite == null)
				return gameObject.name;
			else
				return image.sprite.name;
		}
	}

	// 作業ダミー作成用パス(プロジェクトRootからの相対パス)
	private string dirPath
	{
		get
		{
			var result = "Assets/Resources/" + ParentAbsPath;

			if (image.sprite != null)
				result += "/" + gameObject.name;

			return result;
		}
	}
	private string filePath
	{
		get { return string.Format("{0}/{1}.{2}", dirPath, fileName, extention); }
	}

	// Resource取得用パス(Assets/Resourcesから下の)
	private string resourceDirPath
	{
		get
		{
			var result = ParentAbsPath;

			if (image.sprite != null)
				result += "/" + gameObject.name;

			return result;
		}
	}
	private string resourceFilePath
	{
		get { return string.Format("{0}/{1}", resourceDirPath, fileName); }
	}

	private Image imageComp = null;
	private Image image
	{
		get
		{
			if(imageComp == null)
			{
				imageComp = GetComponent<Image>();
			}

			return imageComp;
		}
	}

	private Dictionary<string, Sprite> spriteItems = new Dictionary<string, Sprite>();

	void Start()
	{
		parentAbsPath = GetParentAbsPath(gameObject.transform);

		if(dummyCreateMode)
		{
			// 作業ダミーファイル作成
			CreateResourceDirectory(true);
		}
		else
		{
			// 画像読み込み
			LoadImage();
		}
	}

	private string GetParentAbsPath(Transform trans)
	{
		var result = "";
		var parentTrans = trans.parent;

		if(parentTrans != null)
		{
			var p = GetParentAbsPath(parentTrans);

			if (p == "")
				result = parentTrans.name;
			else
				result = p + "/" + parentTrans.name;
		}

		return result;
	}

	private void CreateResourceDirectory(bool isDummy)
	{
		if (!Directory.Exists(dirPath))
			Directory.CreateDirectory(dirPath);

		if(isDummy && !File.Exists(filePath))
		{
			File.Create(filePath).Close();
		}
	}

	public void LoadImage()
	{
		if (!File.Exists(filePath))
			return;

		Debug.Log("FilePath : " + filePath);
		Debug.Log("RefPath : " + resourceFilePath);

		// Resourceを取得
		if (!spriteItems.ContainsKey(filePath))
		{
			var sp = Resources.Load<Sprite>(resourceFilePath);
			image.sprite = sp;
			spriteItems.Add(filePath, sp);
		}
		else
		{
			image.sprite = spriteItems[filePath];
		}
		image.SetNativeSize();
	}
}
