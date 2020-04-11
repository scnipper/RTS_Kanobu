using UnityEngine;

namespace Entity
{
	public class HexagonField : MonoBehaviour
	{
		public Hexagon hexagon;
		private void Start()
		{
			GenerateHexagons();
		}

		private void GenerateHexagons()
		{
			var hexagonSize = hexagon.Size;
			print("size "+hexagonSize);
			float offset = 0;
			for (int i = 0; i < 10; i++)
			{
				offset = i % 2 == 0 ? hexagonSize.x / 2 : 0;
				for (int j = 0; j < 10; j++)
				{
					var newHexagon = Instantiate(hexagon,transform);
					
					newHexagon.transform.position = new Vector2(offset + (j * hexagonSize.x),i * hexagonSize.y/1.5f);
				}
			}
		}
	}
}