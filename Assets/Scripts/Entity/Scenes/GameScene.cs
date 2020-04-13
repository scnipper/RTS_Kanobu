using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity
{
	public class GameScene : MonoBehaviour
	{
		public void Restart()
		{
			SceneManager.LoadScene(0);
		}
	}
}