using UnityEngine;
using UnityEngine.UI;

namespace Util
{
	public class FoodController : MonoBehaviour
	{
		private static FoodController instance;

		private int food;
		private Text foodText;

		private void Awake()
		{
			food = (int) P.Get.config.StartFood;
			instance = this;
			foodText = GetComponent<Text>();
		}


		public void Decrement(int minus)
		{
			food -= minus;
			if (food < 0)
			{
				food = 0;
			}
			UpdateView();
		}

		public void Add(int add)
		{
			food += add;
			UpdateView();
		}

		private void UpdateView()
		{
			foodText.text = food + "";
		}

		public int Food => food;

		public static FoodController Get => instance;
	}
}