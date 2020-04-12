using System;
using UnityEngine;
using Util;
using Util.Net;

namespace Entity
{
	public class Hexagon : MonoBehaviour
	{

		private SpriteRenderer spr;
		private bool isMoving;
		private Color saveColor;

		private byte[] buffer = new byte[32];
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
			if (Field.SelectedUnit != null)
			{
				if (!isMoving)
				{
					buffer[0] = Commands.SetPos;
					AppendToBuffer(1,BitConverter.GetBytes(transform.GetSiblingIndex()));
					AppendToBuffer(5,BitConverter.GetBytes(Field.SelectedUnit.IdUnit));
					
					int sending = 0;
					while (sending < 32)
					{
						sending += P.Get.client.Send(buffer,sending,32 -sending,0);
					}
				}
				MoveHere(Field.SelectedUnit);
				
			}
			
		}
		
		private void AppendToBuffer(int pos,byte[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				buffer[pos + i] = data[i];
			}
		}

		public void MoveHere(Unit unit)
		{
			if (!isMoving)
			{
				isMoving = true;
				SpriteRenderer.color = Color.red;
				unit.Target = transform;
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