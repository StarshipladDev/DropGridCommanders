namespace DropGrid.Core.Environment.Template
{
    public class TestMechUnitTemplate : UnitTemplate
    {
        public TestMechUnitTemplate()
        {
            Attributes.HpMax = 4;
            Attributes.Hp = 4;
            Attributes.AttackDamageDefault = 2;
            Attributes.DefenseDefault = 0;
        }
    }
}