﻿using UnityEngine;

namespace Entity
{
	public class HexagonField : MonoBehaviour
	{
		public Hexagon hexagon;
		public Tower tower1;
		public Tower tower2;
		private Vector2 hexagonSize;
		private Tower playTower;

		private void Start()
		{
			hexagonSize = hexagon.Size;

			tower1.Field = this;
			tower2.Field = this;
			if (Settings.isFirstPlayer)
			{
				tower2.IsPlayer = true;
				playTower = tower2;
			}
			else
			{
				tower1.IsPlayer = true;
				playTower = tower1;

			}
			GenerateHexagons();
		}

		public Hexagon GetHexagonByPos(int x,int y)
		{
			string compareStr = $"HEX_{x}_{y}";
			var find = transform.Find(compareStr);
			if (find != null)
			{
				return find.GetComponent<Hexagon>();
			}

			return null;
		}

		public void SpawnRustic()
		{
			playTower.SpawnUnit();
		}
		private void GenerateHexagons()
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					var newHexagon = Instantiate(hexagon,transform);
					newHexagon.name = $"HEX_{j}_{i}";
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