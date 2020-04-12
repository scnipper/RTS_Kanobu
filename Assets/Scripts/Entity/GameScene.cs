using QuickType;
using UnityEngine;
using Util;

namespace Entity
{
	public class GameScene : MonoBehaviour
	{
		public TextAsset configFile;
		private void Awake()
		{
			P.Get.config = Config.FromJson(configFile.text);
		}
	}
}