namespace ValueObjectsCollection
{
	public readonly partial struct Cpf
	{
		public readonly string Value;

		public Cpf(string value)
		{
			Value = RemoveCharacters(value);

			if (Validate(value))
				Value = value.Trim().ToLower();
			else
				throw new ArgumentException($"Invalid CPF '{value}'");
		}

		private static string RemoveCharacters(string value) => value.Trim().Replace(".", "").Replace("-", "");


		#region Implicit Operators

		public static implicit operator Cpf(string value) => new(value);
		public static implicit operator Cpf(int value) => new(value.ToString());
		public static implicit operator Cpf(decimal value) => new(value.ToString());
		public static implicit operator Cpf(long value) => new(value.ToString());

		#endregion

		public override string ToString() => Value;

		public string Formatted() => $"{Value[..3]}.{Value.Substring(3, 6)}.{Value.Substring(6, 9)}-{Value.Substring(10, 2)}";

		public int ToNumber() => Convert.ToInt32(Value);


		private static readonly string[] cpfInvalid =
		{
			"00000000000",
			"11111111111",
			"22222222222",
			"33333333333",
			"44444444444",
			"55555555555",
			"66666666666",
			"77777777777",
			"88888888888",
			"99999999999"
		};

		public static bool Validate(string value)
		{
			value = RemoveCharacters(value);

			var multiplicator1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			var multiplicator2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

			if (!long.TryParse(value, out var _))
				return false;

			if (value.Length != 11 || cpfInvalid.Contains(value))
				return false;

			var tempCpf = value[..9];
			var sum = 0;

			for (int i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplicator1[i];

			var remainder = sum % 11;
			remainder = remainder < 2 ? 0 : 11 - remainder;
			var digit = remainder.ToString();
			tempCpf += digit;
			sum = 0;

			for (int i = 0; i < 10; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplicator2[i];

			remainder = sum % 11;
			remainder = remainder < 2 ? 0 : 11 - remainder;
			digit += remainder;

			return value.EndsWith(digit);
		}
	}
}
