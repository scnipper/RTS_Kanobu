using System;
using Entity.Fight;
using UnityEngine;

namespace Entity
{
	public class FireBall : MoveToTarget
	{

		public event Action endTargetDistance;
		protected override void Start()
		{
			isRotate = true;
			base.Start();
		}

		private void Update()
		{
			if (Target != null)
			{
				MoveTo(Target);
				if (isEndDistance)
				{
					Target = null;
					endTargetDistance?.Invoke();
					Destroy(gameObject);
				}
			}
		}

		public Transform Target { get; set; }
	}
}