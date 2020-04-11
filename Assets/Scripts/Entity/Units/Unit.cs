using System.Collections;
using Entity.Fight;
using UnityEngine;

namespace Entity
{
	public class Unit : MoveToTarget
	{
		private SpriteRenderer spriteRenderer;

		public bool isSelectable = true;
		public int hp = 200;
		public int attack = 20;
		private Transform transformParent;
		private Transform unitTr;

		protected override void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			unitTr = transform;
			transformParent = unitTr.parent;
			StartCoroutine(AtttackCycle());
			base.Start();
		}

		private IEnumerator AtttackCycle()
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

				yield return new WaitForSeconds(1);
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