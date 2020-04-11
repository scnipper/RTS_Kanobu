using UnityEngine;

namespace Entity
{
	public class Hexagon : MonoBehaviour
	{

		private SpriteRenderer spr;
		private bool isMoving;
		private Color saveColor;

		private void Start()
		{
			saveColor = SpriteRenderer.color;
		}

		public void Deselect()
		{
			SpriteRenderer.color = saveColor;
		}

		private void OnMouseDown()
		{
			if (Field.SelectedUnit != null && !isMoving)
			{
				isMoving = true;
				SpriteRenderer.color = Color.red;
				Field.SelectedUnit.Target = transform;
			}
		}

		public SpriteRenderer SpriteRenderer => spr == null ? GetComponent<SpriteRenderer>() : spr;

		public Vector2 Size => SpriteRenderer.size * transform.localScale * 100;
		public bool IsUnitAdd { get; set; }
		public HexagonField Field { get; set; }

		public bool IsMoving
		{
			get => isMoving;
			set => isMoving = value;
		}
	}
}