using System.Collections;
using Entity.Fight;
using UnityEngine;
using Util;

namespace Entity
{
	public class Unit : MoveToTarget
	{

		public bool isSelectable = true;
		public int hp = 200;
		public int attack = 20;
		public int price = 20;
		public int configId;
		private Transform transformParent;
		private Transform unitTr;
		private float speedAttack = 1;

		protected override void Start()
		{
			unitTr = transform;
			transformParent = unitTr.parent;
			StartCoroutine(AttackCycle());
			SetFromConfig();
			base.Start();
		}

		public void SetFromConfig()
		{
			foreach (var configUnit in P.Get.config.Units)
			{
				if (configUnit.Id == configId)
				{
					hp = (int) configUnit.Hp;
					attack = (int) configUnit.Attack;
					price = (int) configUnit.Price;
					speedAttack = (float) configUnit.SpeedAttack;
				}
			}
		}

		private IEnumerator AttackCycle()
		{
			while (true)
			{
				foreach (Transform tr in transformParent)
				{
					if (tr != unitTr)
					{
						if (Vector2.Distance(tr.position, unitTr.position) < 50)
						{
							Attack(tr.GetComponent<Unit>());
						}
					}
				}
				

				yield return new WaitForSeconds(speedAttack);
			}
		}

		protected virtual void Attack(Unit unit)
		{
			unit.hp -= attack;
			if (unit.hp <= 0)
			{
				Destroy(unit.gameObject);
			}
		}

		private void Update()
		{
			if (Target != null)
			{
				MoveTo(Target);

				if (isEndDistance)
				{
					CurHexagon.IsUnitAdd = false;
					CurHexagon.Deselect();
					var hexagon = Target.GetComponent<Hexagon>();
					hexagon.IsMoving = false;
					hexagon.IsUnitAdd = true;
					CurHexagon = hexagon;
					Target = null;
				}
			}
				
		}

		
		private void OnMouseDown()
		{
			if (isSelectable && IdPlayer == Settings.playerNum)
			{
				
				//spriteRenderer.color = Color.blue;
				Field.SelectedUnit = this;
			}
			
		}

		public HexagonField Field { get; set; }
		public Hexagon CurHexagon { get; set; }
		public Transform Target { get; set; }
		public int IdUnit { get; set; }

		public int Price => price;
		public int IdPlayer { get; set; }
	}
}