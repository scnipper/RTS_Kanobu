using System;
using System.Collections;
using UnityEngine;
using Util;
using Util.Net;

namespace Entity
{
	public class Tower : MonoBehaviour
	{
		public Vector2 startPos;
		public bool isTop;
		private Vector2[] posForSpawn = new Vector2[3];
		public Transform unitsPlace;

		public RusticUnit rusticUnit;
		public KnightUnit knightUnit;
		public WizFireUnit wizFireUnit;
		public WizEarthUnit wizEarthUnit;
		public WizWaterUnit wizWaterUnit;
		public int idUnit = 1000;
		public SpriteRenderer fill;

		public GameObject earthSchool;
		public GameObject waterSchool;
		public GameObject fireSchool;
		
		private int hp;
		private int maxHp;
		private int posToSpawn;

		private bool isSending = true;
		
		byte[] buffer = new byte[32];
		private int sending;
		private static readonly int Fill = Shader.PropertyToID("_Fill");
		private Transform towerTr;


		public enum TypeUnits
		{
			Rustic,Knight,WizFire,WizEarth,WizWater
		}

		private void Start()
		{
			towerTr = transform;
			StartCoroutine(UpdateViewUnits());
			StartCoroutine(SetPrice());
			
			hp = (int) P.Get.config.Tower.Hp;
			maxHp = hp;
			if (isTop)
			{
				posForSpawn[0] = new Vector2(startPos.x-1,startPos.y);
				posForSpawn[1] = new Vector2(startPos.x-1,startPos.y-1);
				posForSpawn[2] = new Vector2(startPos.x,startPos.y-1);
			}
			else
			{
				posForSpawn[0] = new Vector2(startPos.x+1,startPos.y);
				posForSpawn[1] = new Vector2(startPos.x+1,startPos.y+1);
				posForSpawn[2] = new Vector2(startPos.x,startPos.y+1);
			}
		}
		
		
		private IEnumerator UpdateViewUnits()
		{
			while (true)
			{

				foreach (Transform tr in unitsPlace)
				{
					if (Vector2.Distance(tr.position, towerTr.position) < 350)
					{
						Unit unit = tr.GetComponent<Unit>();

						if(unit.IdPlayer != IdPlayer)
							DecrementHp(unit.attack);
					}
				}

				yield return new WaitForSeconds((float) P.Get.config.Tower.SpeedAttack);
			}
		}

		private IEnumerator SetPrice()
		{
			yield return new WaitForSeconds(1);
			knightUnit.SetFromConfig();
			rusticUnit.SetFromConfig();
			wizEarthUnit.SetFromConfig();
			wizFireUnit.SetFromConfig();
			wizWaterUnit.SetFromConfig();
			Field.priceKnight.text = knightUnit.Price+"";
			Field.priceRustic.text = rusticUnit.Price+"";
			Field.priceWizEarth.text = wizEarthUnit.Price+"";
			Field.priceWizFire.text = wizFireUnit.Price+"";
			Field.priceWizWater.text = wizWaterUnit.Price+"";
		}

		public void SpawnUnit(TypeUnits typeUnits,bool withoutFood = false)
		{
			Unit unit = null;

			switch (typeUnits)
			{
				case TypeUnits.Rustic:
					unit = rusticUnit;
					break;
				case TypeUnits.Knight:
					unit = knightUnit;
					break;
				case TypeUnits.WizEarth:
					unit = wizEarthUnit;
					break;
				case TypeUnits.WizFire:
					unit = wizFireUnit;
					break;
				case TypeUnits.WizWater:
					unit = wizWaterUnit;
					break;
			}

			if (FoodController.Get.Food >= unit.price || withoutFood)
			{
				/*if (unit is WizWaterUnit && !wizWaterUnit.gameObject.activeSelf)
				{
					wizWaterUnit.gameObject.SetActive(true);
					if(!withoutFood)
						FoodController.Get.Decrement(unit.price);
				}
				if (unit is WizFireUnit && !wizFireUnit.gameObject.activeSelf)
				{
					wizFireUnit.gameObject.SetActive(true);
					if(!withoutFood)
						FoodController.Get.Decrement(unit.price);
				}
				if (unit is WizEarthUnit && !wizEarthUnit.gameObject.activeSelf)
				{
					wizEarthUnit.gameObject.SetActive(true);
					if(!withoutFood)
						FoodController.Get.Decrement(unit.price);
				}*/
				
				var newUnit = Instantiate(unit,unitsPlace);

				
				var pos = posForSpawn[posToSpawn];
				var hexagonByPos = Field.GetHexagonByPos((int) pos.x, (int) pos.y);
				posToSpawn++;
				if (posToSpawn >= posForSpawn.Length)
				{
					posToSpawn = 0;
				}
				if (!hexagonByPos.IsUnitAdd)
				{
					if(!withoutFood)
						FoodController.Get.Decrement(unit.price);
					hexagonByPos.IsUnitAdd = true;
					newUnit.transform.position = hexagonByPos.transform.position;
					newUnit.Field = Field;
					newUnit.CurHexagon = hexagonByPos;
					newUnit.IdUnit = idUnit++;
					newUnit.IdPlayer = IdPlayer;

					if (isSending && IsPlayer)
					{
						isSending = false;
						buffer[0] = Commands.SpawnUnit;
						buffer[1] = (byte) typeUnits;
						SendSpawn();

					}

				}
				else
				{
					Destroy(newUnit.gameObject);
				}
			
				
				
			}
			
			
			
		}


		public void DecrementHp(int dec)
		{
			hp -= dec;
			fill.material.SetFloat(Fill,hp/(float)maxHp);

			if (hp <= 0)
			{
				Field.GameOver(IdPlayer);
			}
			
		}
		private void SendSpawn()
		{
			if (P.Get.client != null)
			{
				P.Get.client.BeginSend(buffer, sending, 32 - sending, 0, SendOk, P.Get.client);
			}

		}

		private void SendOk(IAsyncResult ar)
		{
			sending += P.Get.client.EndSend(ar);
			if (sending >= 32)
			{
				sending = 0;
				isSending = true;
				print("sending spawn");
			}
			else
			{
				SendSpawn();
			}
			
			
		}

		public bool IsPlayer { get; set; }
		
		public int IdPlayer { get; set; }

		public HexagonField Field { get; set; }
	}
}