using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Util.Net
{
	public class MainNetListener : MonoBehaviour
	{
		private byte[] buffer = new byte[32];

		private int received = 0;
		private int size = 32;
		private bool isCommandReceive;
		private byte command;

		private List<Unit> cacheUnit = new List<Unit>();

		private void Start()
		{
			StartReceive();
			StartCoroutine(CommandInvoker());
			StartCoroutine(MonitoringPositions());
		}

		private IEnumerator MonitoringPositions()
		{
			while (true)
			{

				if (UnitsPlace.childCount -6 != cacheUnit.Count)
				{
					cacheUnit.Clear();

					foreach (Transform tr in UnitsPlace)
					{
						Unit unit = tr.GetComponent<Unit>();
						if(!(unit is FoodUnit))
							cacheUnit.Add(unit);
					}
				}

				/*if (cacheUnit.Count > 0)
				{
					SendingPos();
				}*/
				
				yield return null;
			}
		}
		

		/*private void SendingPos()
		{
			foreach (var unit in cacheUnit)
			{
				if (unit.IdPlayer == Settings.playerNum)
				{
					buffer[0] = Commands.SetPos;
					byte[] idUnit = BitConverter.GetBytes(unit.IdUnit);
					var pos = unit.transform.position;
					byte[] xBytes = BitConverter.GetBytes(pos.x);
					byte[] yBytes = BitConverter.GetBytes(pos.y);
					AppendToBuffer(1,idUnit);
					AppendToBuffer(1 + idUnit.Length,xBytes);
					AppendToBuffer(1 + idUnit.Length + xBytes.Length,yBytes);
					int send = 0;
					while (send < size)
					{
						send += P.Get.client.Send(buffer,send,size-send,0);
					}
				}
			}
		}
		private string ByteArrayToString(byte[] ba)
		{
			return BitConverter.ToString(ba).Replace("-","");
		}
*/
		

		private byte[] FromBuffer(int pos, int length)
		{
			byte[] data = new byte[length];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = buffer[pos + i];
			}

			return data;
		}

		private IEnumerator CommandInvoker()
		{
			while (true)
			{

				yield return new WaitUntil(()=>isCommandReceive);


				switch (command)
				{
					case Commands.SpawnUnit:
						SpawnTower.SpawnUnit((Tower.TypeUnits) buffer[1],true);
						break;
					case Commands.SetPos:

						int idHexagon = BitConverter.ToInt32(FromBuffer(1,4),0);
						int idUnit = BitConverter.ToInt32(FromBuffer(5,4),0);
						Hexagon hexagon = HexagonField.GetChild(idHexagon).GetComponent<Hexagon>();
						foreach (var unit in cacheUnit)
						{
							if (unit.IdUnit == idUnit)
							{
								hexagon.MoveHere(unit);
								break;
							}
						}
						break;
				}
				
				isCommandReceive = false;
				
				yield return new WaitForEndOfFrame();
			}
		}

		private void StartReceive()
		{
			if (P.Get.client != null)
			{
				P.Get.client.BeginReceive(buffer, received, size - received, 0, ReciveComplete, P.Get.client);
			}

		}

		private void ReciveComplete(IAsyncResult ar)
		{
			received += P.Get.client.EndReceive(ar);

			
			if (received >= size)
			{
				received = 0;
				command = buffer[0];

				isCommandReceive = true;

			}
			
			StartReceive();
		}


		public Transform UnitsPlace { get; set; }
		public Tower SpawnTower { get; set; }
		
		public Transform HexagonField { get; set; }
	}
}