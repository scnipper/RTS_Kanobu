using System.Collections;
using UnityEngine;
using Util;

namespace Entity
{
	public class Base : MonoBehaviour
	{
		public SpriteRenderer fillImg;
		public Transform unitsPlace;
		public FireBall fireBall;
		public Tower tower1;
		public Tower tower2;

		private int attackTower;
		private float power = 0;
		private static readonly int Fill = Shader.PropertyToID("_Fill");
		private Transform baseTr;


		//private List<Unit> unitAround = new List<Unit>();
		private int unitAround = 0;
		private int lastId;

		private void Start()
		{
			attackTower = (int) P.Get.config.Fontan.AttackTower;
			UpdatePowerFill();
			baseTr = transform;
			StartCoroutine(UpdateViewUnits());
		}

		private IEnumerator UpdateViewUnits()
		{
			lastId = 0;
			while (true)
			{
				unitAround = 0;

				foreach (Transform tr in unitsPlace)
				{
					if (Vector2.Distance(tr.position, baseTr.position) < 250)
					{
						Unit unit = tr.GetComponent<Unit>();

						if (unitAround == 0 || unit.IdPlayer == lastId)
						{
							unitAround++;
							lastId = unit.IdPlayer;
						}
						else
						{
							unitAround = 0;
							break;
						}
					}
				}
				print("unit around "+unitAround);

				for (int i = 0; i < unitAround; i++)
				{
					AddPower();
				}

				yield return new WaitForSeconds(1);
			}
		}

		private void AddPower()
		{
			power += 1.0f / P.Get.config.Fontan.TimeCapture;

			if (power >= 1)
			{
				power = 0;
				FireBall fire = Instantiate(fireBall,transform.position,Quaternion.identity);

				Transform target = null;

				if (lastId == tower1.IdPlayer)
				{
					target = tower2.transform;
				}
				if (lastId == tower2.IdPlayer)
				{
					target = tower1.transform;
				}

				fire.endTargetDistance += () =>
				{
					target.GetComponent<Tower>().DecrementHp(attackTower);
				};

				fire.Target = target;
			}

			UpdatePowerFill();
		}

		private void UpdatePowerFill()
		{
			fillImg.material.SetFloat(Fill, power);
		}
	}
}