using System;
using System.Collections;
using Entity;
using UnityEngine;

namespace Util.Net
{
	public class MainNetListener : MonoBehaviour
	{
		private byte[] buffer = new byte[256];

		private int received = 0;
		private int size = 256;
		private bool isCommandReceive;
		private byte command;

		private void Start()
		{
			StartReceive();
			StartCoroutine(CommandInvoker());
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
				}
				
				isCommandReceive = false;
				
				yield return new WaitForEndOfFrame();
			}
		}

		private void StartReceive()
		{
			P.Get.client.BeginReceive(buffer, received, size - received, 0, ReciveComplete, P.Get.client);
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
	}
}