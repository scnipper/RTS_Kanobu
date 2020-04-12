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
			print("Start game scene");
			P.Get.config = Config.FromJson(configFile.text);
			print(P.Get.config == null);
		}
	}
}