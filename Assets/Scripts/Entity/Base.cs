using UnityEngine;
using Util;

namespace Entity
{
	public class Base : MonoBehaviour
	{
		public SpriteRenderer fillImg;

		private int attackTower;
		private float power = 0;
		private static readonly int Fill = Shader.PropertyToID("_Fill");


		private void Start()
		{
			attackTower = (int) P.Get.config.Fontan.AttackTower;
			UpdatePowerFill();
		}

		public void AddPower()
		{
			power += 1.0f/P.Get.config.Fontan.TimeCapture;

			if (power > 1)
			{
				power = 1;
			}

			UpdatePowerFill();
		}

		private void UpdatePowerFill()
		{
			fillImg.material.SetFloat(Fill,power);
		}
	}
}