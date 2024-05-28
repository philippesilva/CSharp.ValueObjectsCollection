namespace ValueObjectsCollection.UnitTests
{
	public class EmailAddressTest
	{
		[Fact]
		public void Email_IsInvalid_ReturnSuccess()
		{
			const string emailInvalid = "test.....@djfkld";

			Assert.False(EmailAddress.Validate(emailInvalid));
		}

		[Theory]
		[InlineData("test@test.com.br")]
		public void Email_IsValid_ReturnSuccess(string email)
		{
			Assert.True(EmailAddress.Validate(email));
		}
	}
}