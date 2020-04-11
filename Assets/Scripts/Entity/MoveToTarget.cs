using System;
using UnityEngine;
using Util;

namespace Entity.Fight
{
	[Serializable]
	public class MovingParam
	{
		public float speedMove;
		public float minDistance;
	}

	public class MoveToTarget : MonoBehaviour
	{
		public MovingParam movingParam;

		protected Vector2 vectorMove;

		protected float angle;
		protected bool isRotateToTarget = true;

		protected float scaleVectorMove = 1;
		private bool isLockTurnRadius;

		private float lastBodyRotation;

		// если идет вращение по линейной интерполяции
		private bool isStartRotate;
		private float timeToRotate;

		private bool isFirstRotateBody;

		// нужно ли повернуть вектор движения в самом начале
		private bool isFirstRotateVectorMove;
		private bool isCollisionExit;
		protected bool isEndDistance;
		// вращать ли сам объект
		protected bool isRotate;
		protected Transform transformMoving;
		private bool isMove = true;
		protected Vector2 moveWhenNotTarget;
		private bool isFirstMove = true;


		protected virtual void Start()
		{
			vectorMove = new Vector2(0, movingParam.speedMove);
			moveWhenNotTarget = new Vector2(movingParam.speedMove/2, 0);
			transformMoving = transform;
			//body = GetComponent<Rigidbody2D>();
		}

		public void MoveTo(Transform target)
		{
			if (!isMove)
			{
				return;
			}

			if (target == null )
			{
				if(isFirstMove)
					transformMoving.position += (Time.deltaTime * (Vector3)moveWhenNotTarget);

				return;
			}

			isFirstMove = false;
			RotateToTarget(target);
			if (movingParam.minDistance > 0)
			{
				if (Vector2.Distance(target.position, transformMoving.position) > movingParam.minDistance)
				{
					isEndDistance = false;
					MovingTo();
				}
				else
				{
					isEndDistance = true;
				}
			}
			else
			{
				MovingTo();
			}

		}

		private void OnCollisionExit2D(Collision2D other)
		{
			isCollisionExit = false;
		}


		

		public void RotateToTarget(Transform target)
		{
			if (target == null)
			{
				return;
			}
			
			var position = transformMoving.position;
			Vector3 posPlayer = target.position;

			position -= posPlayer;

			angle = Vector2.SignedAngle(Vector2.up, position) + 180;
		
			if(isRotate)
				transformMoving.eulerAngles = Vector3.forward * angle;

			RotateVectorMove(angle);
			
		}


		private void MovingTo()
		{
			transformMoving.position += (Time.deltaTime * (Vector3)vectorMove);
		}

	

		private void RotateVectorMove(float ang)
		{
			vectorMove.Set(0, movingParam.speedMove);
			vectorMove = vectorMove.Rotate(ang);

		}
		

		/**
		* Угол -180 -- 180 превращает в 0 -- 360
		*/
		protected float GetTrueAngle(float _angle)
		{
			return _angle < 0 ? _angle + 360 : _angle;
		}
		
		

		public Vector2 VectorMove => vectorMove;

		public bool IsMove
		{
			get => isMove;
			set => isMove = value;
		}

		public bool IsEndDistance => isEndDistance;
	}
}