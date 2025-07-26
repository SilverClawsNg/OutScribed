namespace OutScribed.SharedKernel.DomainFieldValidation
{
    public readonly struct InvalidParameters(string fieldName, object? fieldValue)
    {
        public string FieldName { get; } = fieldName;
        public object? FieldValue { get; } = fieldValue;

        public override string ToString()
        {
            return $"{FieldName}: {(FieldValue == null ? "null" : $"'{FieldValue}'")}";
        }
    }
}
