namespace ValueObjectsCollection.UnitTests
{
    public class CpfTest
    {
		[Theory]
		[InlineData("413.858.940-66")]
		[InlineData("378.330.070-33")]
		[InlineData("11122233344466")]
		public void Email_IsInvalid_ReturnSuccess(string cpf)
		{
			Assert.False(Cpf.Validate(cpf));
		}

		[Theory]
		[InlineData("413.858.940-67")]
		[InlineData("378.330.070-30")]
		[InlineData("29327685067")]
		public void Email_IsValid_ReturnSuccess(string cpf)
		{
			Assert.True(Cpf.Validate(cpf));
		}
	}
}