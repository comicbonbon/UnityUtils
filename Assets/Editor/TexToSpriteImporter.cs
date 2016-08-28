using UnityEngine;
using UnityEditor;
using System.Collections;

public class TexToSpriteImporter : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		// Import字のデフォルト設定
		var importer = assetImporter as TextureImporter;

		importer.textureType = TextureImporterType.Sprite;
	}
}
