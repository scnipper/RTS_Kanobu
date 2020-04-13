namespace Entity
{
	public class WizWaterUnit : Unit
	{
		protected override bool IsAttackX2(Unit unit)
		{
			return unit is WizFireUnit;
		}
	}
}