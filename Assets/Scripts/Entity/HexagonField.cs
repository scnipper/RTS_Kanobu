using UnityEngine;
using UnityEngine.UI;
using Util;
using Util.Net;

namespace Entity
{
	public class HexagonField : MonoBehaviour
	{
		public Hexagon hexagon;
		public Tower tower1;
		public Tower tower2;
		public Transform basePos;
		private Vector2 hexagonSize;
		private Tower playTower;
		private Unit selectedUnit;
		
		public Text priceRustic; 
		public Text priceKnight; 
		public Text priceWizFire; 
		public Text priceWizEarth; 
		public Text priceWizWater; 

		private void Start()
		{
			hexagonSize = hexagon.Size;

			tower1.Field = this;
			tower2.Field = this;
			Tower spawnTower = null;
			if (Settings.playerNum == 0)
			{
				tower2.IsPlayer = true;
				playTower = tower2;
				spawnTower = tower1;
			}
			else if (Settings.playerNum == 1)
			{
				tower1.IsPlayer = true;
				playTower = tower1;
				spawnTower = tower2;
			}

			var netListen = new GameObject("NET_LISTEN");

			netListen.AddComponent<MainNetListener>().SpawnTower = spawnTower;
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
			playTower.SpawnUnit(Tower.TypeUnits.Rustic);
		}
		public void SpawnKnight()
		{
			playTower.SpawnUnit(Tower.TypeUnits.Knight);
		}
		public void SpawnWizWater()
		{
			playTower.SpawnUnit(Tower.TypeUnits.WizWater);
		}
		public void SpawnWizFire()
		{
			playTower.SpawnUnit(Tower.TypeUnits.WizFire);
		}
		public void SpawnWizEarth()
		{
			playTower.SpawnUnit(Tower.TypeUnits.WizEarth);
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
					newHexagon.Field = this;
				}
			}
		}

		private Vector2 GetPosHexagon(int x, int y)
		{
			float offset = y % 2 == 0 ? hexagonSize.x / 2 : 0;

			return new Vector2(offset + (x * hexagonSize.x), y * hexagonSize.y / 1.5f);
		}

		public Unit SelectedUnit
		{
			get => selectedUnit;
			set => selectedUnit = value;
		}
	}
}