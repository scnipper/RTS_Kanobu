using System.Collections;
using Entity.Fight;
using UnityEngine;
using Util;

namespace Entity
{
	public class Unit : MoveToTarget
	{
		private SpriteRenderer spriteRenderer;

		public bool isSelectable = true;
		public int hp = 200;
		public int attack = 20;
		public int price = 20;
		public int configId;
		private Transform transformParent;
		private Transform unitTr;
		private float speedAttack;

		protected override void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			unitTr = transform;
			transformParent = unitTr.parent;
			StartCoroutine(AttackCycle());
			SetFromConfig();
			base.Start();
		}

		private void SetFromConfig()
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

				if (Field != null && Vector2.Distance(unitTr.position, Field.basePos.position) < 250)
				{
					Field.basePos.GetComponent<Base>().AddPower();
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

		private void Deselect()
		{
			spriteRenderer.color = Color.white;
		}
		private void OnMouseDown()
		{
			if (isSelectable)
			{
				if (Field.SelectedUnit != null)
				{
					Field.SelectedUnit.Deselect();
				}
				spriteRenderer.color = Color.blue;
				Field.SelectedUnit = this;
			}
			
		}

		public HexagonField Field { get; set; }
		public Hexagon CurHexagon { get; set; }
		public Transform Target { get; set; }
	}
}