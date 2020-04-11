using UnityEngine;

namespace Entity
{
	public class Base : MonoBehaviour
	{
		public SpriteRenderer fillImg;

		private float power = 0;
		private static readonly int Fill = Shader.PropertyToID("_Fill");


		private void Start()
		{
			UpdatePowerFill();
		}

		public void AddPower(float delta)
		{
			power += delta;

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