using Util;

namespace Entity
{
	public class FoodUnit : Unit
	{
		protected override void Attack(Unit unit)
		{
			if (unit is RusticUnit && unit.IdPlayer == Settings.playerNum)
			{
				FoodController.Get.Add((int) P.Get.config.CraftFood);
			}
		}
	}
}