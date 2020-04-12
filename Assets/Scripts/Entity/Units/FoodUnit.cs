using Util;

namespace Entity
{
	public class FoodUnit : Unit
	{
		protected override void Attack(Unit unit)
		{
			if (unit is RusticUnit)
			{
				FoodController.Get.Add((int) P.Get.config.CraftFood);
			}
		}
	}
}