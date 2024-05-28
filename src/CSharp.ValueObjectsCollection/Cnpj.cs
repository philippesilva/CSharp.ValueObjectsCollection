namespace ValueObjectsCollection
{
	public readonly partial struct Cnpj
	{
		public readonly string Value;

		public Cnpj(string value)
		{
			Value = RemoveCharacters(value);

			if (Validate(value))
				Value = value.Trim().ToLower();
			else
				throw new ArgumentException($"Invalid CNPJ '{value}'");
		}

		private static string RemoveCharacters(string value) => value.Trim().Replace(".", "").Replace("-", "");


		#region Implicit Operators

		public static implicit operator Cnpj(string value) => new(value);
		public static implicit operator Cnpj(int value) => new(value.ToString());
		public static implicit operator Cnpj(decimal value) => new(value.ToString());
		public static implicit operator Cnpj(long value) => new(value.ToString());

		#endregion

		public override string ToString() => Value;

		public string Formatted() => $"{Value[..3]}.{Value.Substring(3, 6)}.{Value.Substring(6, 9)}-{Value.Substring(10, 2)}";

		public int ToNumber() => Convert.ToInt32(Value);


		private static readonly string[] cnpjInvalid =
		{
			"00000000000000",
			"11111111111111",
			"22222222222222",
			"33333333333333",
			"44444444444444",
			"55555555555555",
			"66666666666666",
			"77777777777777",
			"88888888888888",
			"99999999999999",
		};

		public static bool Validate(string value)
		{
			var multiplicator1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			var multiplicator2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			
			value = value.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
			
			if (value.Length != 14 || !long.TryParse(value, out var _))
				return false;
			
			if (cnpjInvalid.Contains(value))
				return false;

			var tempCnpj = value[..12];
			var sum = 0;
			
			for (int i = 0; i < 12; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplicator1[i];
			
			var remainder = (sum % 11);
			remainder = remainder < 2 ? 0 : 11 - remainder;
			var digit = remainder.ToString();
			tempCnpj += digit;
			sum = 0;
			
			for (int i = 0; i < 13; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplicator2[i];

			remainder = sum % 11;
			remainder = remainder < 2 ? 0 : 11 - remainder;
			digit += remainder;
			
			return value.EndsWith(digit);
		}
	}
}
