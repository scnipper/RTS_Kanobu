namespace Entity
{
	public class WizFireUnit : Unit
	{
		protected override bool IsAttackX2(Unit unit)
		{
			return unit is WizEarthUnit;
		}
	}
}