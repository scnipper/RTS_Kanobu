namespace Entity
{
	public class WizEarthUnit : Unit
	{
		protected override bool IsAttackX2(Unit unit)
		{
			return unit is WizWaterUnit;
		}
	}
}