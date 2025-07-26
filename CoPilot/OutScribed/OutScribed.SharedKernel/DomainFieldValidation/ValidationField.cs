namespace OutScribed.SharedKernel.DomainFieldValidation
{
    public class ValidationField
    {
        public string Name { get; }
        public object? Value { get; }
        public int MinValue { get; } // For numeric checks. 0 means ignore.
        public int MaxValue { get; } // For numeric checks. 0 means ignore.
        public int MinLength { get; } // For string length checks. 0 means ignore.
        public int MaxLength { get; } // For string length checks. 0 means ignore.
        public bool IsOptional { get; } // To replace the separate optionalFieldNames parameter

        public ValidationField(string name, object? value, int minValue = 0, int maxValue = 0, int minLength = 0, int maxLength = 0, bool isOptional = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Field name cannot be null or whitespace.", nameof(name));

            Name = name;
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
            MaxLength = maxLength;
            MinLength = minLength;
            IsOptional = isOptional;
        }

        // Implicit conversion from (string, object?) tuple for convenience
        public static implicit operator ValidationField((string name, object? value) tuple)
        {
            return new ValidationField(tuple.name, tuple.value);
        }
    }
}
