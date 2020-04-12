using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using QuickType;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

namespace Entity
{
	public class StartScene : MonoBehaviour
	{
		public TextAsset configFile;
		public Text errorText;
		public Text waitText;
		private byte[] buffer;
		private bool isWait;
		private string textWait = "Wait";

		private void Awake()
		{
			P.Get.config = Config.FromJson(configFile.text);
			buffer = new byte[2];
		}

		private IEnumerator Wait()
		{
			isWait = true;
			while (isWait)
			{

				waitText.text = textWait;
				for (int i = 0; i < 5; i++)
				{
					waitText.text += ".";
					yield return new WaitForSeconds(0.4f);
				}
				
				yield return new WaitForSeconds(0.2f);
			}
			
			yield return new WaitForEndOfFrame();
			StartGame();
		}

		public void StartGame()
		{
			SceneManager.LoadScene("GameScene");

		}

		public void Connect()
		{
			StartCoroutine(Wait());

			print("start connect");
			try
			{
				IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Settings.address), Settings.port);
 
				P.Get.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				// подключаемся к удаленному хосту
				P.Get.client.Connect(ipPoint);
				P.Get.client.BeginReceive(buffer,0,2,0,ReciveComplete,P.Get.client);
				
			}
			catch(Exception ex)
			{
				errorText.text = ex.Message;
			}
		}

		private void ReciveComplete(IAsyncResult ar)
		{
			if (P.Get.client.EndReceive(ar) >=2)
			{
				Settings.playerNum = buffer[0];
				isWait = false;
			}

		}
		
	}
}