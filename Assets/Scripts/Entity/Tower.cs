using UnityEngine;
using Util;

namespace Entity
{
	public class Tower : MonoBehaviour
	{
		public Vector2 startPos;
		public bool isTop;
		private Vector2[] posForSpawn = new Vector2[3];
		public Transform unitsPlace;

		public RusticUnit rusticUnit;
		
		
		public enum TypeUnits
		{
			Rustic,Knight,WizFire,WizEarth,WizWater
		}

		private void Start()
		{
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

		public void SpawnUnit(TypeUnits typeUnits)
		{
			Unit unit = null;

			switch (typeUnits)
			{
				case TypeUnits.Rustic:
					unit = rusticUnit;
					break;
			}

			if (FoodController.Get.Food >= unit.price)
			{
				FoodController.Get.Decrement(unit.price);
				var newUnit = Instantiate(unit,unitsPlace);

				for (int i = 0; i < 20; i++)
				{
					var pos = posForSpawn[Random.Range(0,3)];
					var hexagonByPos = Field.GetHexagonByPos((int) pos.x, (int) pos.y);
					if(hexagonByPos.IsUnitAdd) continue;
				
					hexagonByPos.IsUnitAdd = true;
					newUnit.transform.position = hexagonByPos.transform.position;
					newUnit.Field = Field;
					newUnit.CurHexagon = hexagonByPos;
					break;
				}
			}
			
			
			
		}

		public bool IsPlayer { get; set; }

		public HexagonField Field { get; set; }
	}
}