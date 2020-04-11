using UnityEngine;

namespace Entity
{
	public class HexagonField : MonoBehaviour
	{
		public Hexagon hexagon;
		private Vector2 hexagonSize;

		private void Start()
		{
			hexagonSize = hexagon.Size;

			GenerateHexagons();
		}

		private void GenerateHexagons()
		{
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					var newHexagon = Instantiate(hexagon,transform);
					
					newHexagon.transform.position = GetPosHexagon(j,i);
				}
			}
		}

		private Vector2 GetPosHexagon(int x, int y)
		{
			float offset = y % 2 == 0 ? hexagonSize.x / 2 : 0;

			return new Vector2(offset + (x * hexagonSize.x), y * hexagonSize.y / 1.5f);
		}
	}
}