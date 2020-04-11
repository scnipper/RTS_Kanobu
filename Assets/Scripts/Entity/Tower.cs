using UnityEngine;

namespace Entity
{
	public class Tower : MonoBehaviour
	{
		public Vector2 startPos;
		public Unit unit;
		public bool isTop;
		private Vector2[] posForSpawn = new Vector2[3];
		private Transform unitsPlace;

		private void Start()
		{
			unitsPlace = transform.parent.Find("Units");
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

		public void SpawnUnit()
		{
			var newUnit = Instantiate(unit,unitsPlace);

			var pos = posForSpawn[Random.Range(0,3)];
			newUnit.transform.position = Field.GetHexagonByPos((int) pos.x, (int) pos.y).transform.position;
		}

		public bool IsPlayer { get; set; }

		public HexagonField Field { get; set; }
	}
}