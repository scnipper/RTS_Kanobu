using UnityEngine;

namespace Entity
{
	public class Hexagon : MonoBehaviour
	{

		private SpriteRenderer spr;
		private void Start()
		{
			
		}


		public SpriteRenderer SpriteRenderer => spr == null ? GetComponent<SpriteRenderer>() : spr;

		public Vector2 Size => SpriteRenderer.size * transform.localScale * 100;
	}
}