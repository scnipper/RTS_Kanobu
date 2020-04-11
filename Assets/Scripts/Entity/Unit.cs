using Entity.Fight;
using UnityEngine;

namespace Entity
{
	public class Unit : MoveToTarget
	{
		private SpriteRenderer spriteRenderer;

		protected override void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			base.Start();
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
				}
			}
				
		}

		private void Deselect()
		{
			spriteRenderer.color = Color.white;
		}
		private void OnMouseDown()
		{
			if (Field.SelectedUnit != null)
			{
				Field.SelectedUnit.Deselect();
			}
			spriteRenderer.color = Color.blue;
			Field.SelectedUnit = this;
		}

		public HexagonField Field { get; set; }
		public Hexagon CurHexagon { get; set; }
		public Transform Target { get; set; }
	}
}