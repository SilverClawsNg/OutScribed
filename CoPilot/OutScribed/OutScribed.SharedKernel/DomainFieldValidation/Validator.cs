namespace OutScribed.SharedKernel.DomainFieldValidation
{
    public static class Validator
    {
        /// <summary>
        /// Checks a collection of ValidationField objects for various invalid conditions.
        /// Handles optional fields by ignoring null/empty-equivalent values if optional,
        /// but validating size/existence if a non-null value is provided.
        /// </summary>
        /// <param name="fields">A collection of ValidationField objects, each containing field info and constraints.</param>
        /// <returns>A list of InvalidParameters for invalid fields. Returns an empty list if all are valid.</returns>
        public static List<InvalidParameters> GetInvalidFields(params ValidationField[] fields)
        {
            var invalidFields = new List<InvalidParameters>();

            foreach (var field in fields)
            {
                bool isInvalid = false;

                // --- Primary NULL check (common for both optional and required) ---
                if (!field.IsOptional && field.Value == null)
                {
                    isInvalid = true; // If required and null, it's invalid.
                }

                // --- Type-Specific & Constraint Checks (only if not already marked invalid by primary null check) ---
                if (!isInvalid) // Proceeds if field.Value was not null (for both optional & required)
                                // OR if it was optional and null (but that was already 'continue'd above).
                {

                    // For optional fields with a non-null value, we apply "content" checks:
                    // Length for strings, explicit min/max for numbers, enum definition.
                    // We *DO NOT* check for "empty-equivalent" values like IsNullOrWhiteSpace, Ulid.Empty, <=0 for numbers,
                    // as these are considered acceptable for an *optional* field that happens to have a value.
                    if (field.Value is string stringValue)
                    {

                        if (field.IsOptional)
                        {
                            if (field.Value != null && (stringValue.Length < field.MinLength || stringValue.Length > field.MaxLength))
                            {
                                isInvalid = true;
                            }
                        }
                        else
                        {
                            // For required fields, all "empty-equivalent" and content checks apply.
                            // Required strings *must not* be empty/whitespace
                            if (string.IsNullOrWhiteSpace(stringValue) || stringValue.Length < field.MinLength || stringValue.Length > field.MaxLength)
                            {
                                isInvalid = true;
                            }
                        }

                    }
                    if (field.Value is Ulid guidValue)
                    {
                        if (!field.IsOptional)
                        {
                            if (guidValue == Ulid.Empty) // Required Ulids *must not* be empty
                            {
                                isInvalid = true;
                            }
                        }
                        
                    }
                    else if (field.Value is int intValue)
                    {
                        if (field.IsOptional)
                        {
                            if(field.Value != null)
                            {
                                // For optional ints, '0' is valid unless explicitly outside MinValue/MaxValue.
                                // Only apply min/max if a constraint is set (i.e., not 0).
                                if (intValue < field.MinValue || intValue > field.MaxValue)
                                {
                                    isInvalid = true;
                                }
                            }
                        }
                        else
                        {
                            // For optional ints, '0' is valid unless explicitly outside MinValue/MaxValue.
                            // Only apply min/max if a constraint is set (i.e., not 0).
                            if (intValue < field.MinValue || intValue > field.MaxValue)
                            {
                                isInvalid = true;
                            }
                        }
                        

                    }
                    else if (field.Value is long longValue) // Added long for completeness
                    {
                        if (field.IsOptional)
                        {
                            if(field.Value != null)
                            {
                                if (longValue < field.MinValue || longValue > field.MaxValue)
                                {
                                    isInvalid = true;
                                }
                            }
                        }
                        else
                        {
                            if (longValue < field.MinValue || longValue > field.MaxValue)
                            {
                                isInvalid = true;
                            }
                        }
                   
                    }
                    else if (field.Value is decimal decimalValue) // Added decimal for completeness
                    {
                        if (field.IsOptional)
                        {
                            if(field.Value != null)
                            {
                                if (decimalValue < field.MinValue || decimalValue > field.MaxValue)
                                {
                                    isInvalid = true;
                                }
                            }
                        }
                        else
                        {
                            if (decimalValue < field.MinValue || decimalValue > field.MaxValue)
                            {
                                isInvalid = true;
                            }
                        }
                       
                    }
                    else if (field.Value is Enum enumValue)
                    {
                        if (field.IsOptional)
                        {
                            if(field.Value != null)
                            {
                                if (!Enum.IsDefined(enumValue.GetType(), enumValue))
                                {
                                    isInvalid = true;
                                }
                            }
                        }
                        else
                        {
                            // Still check if the enum value is defined (if provided, it must be a valid enum member).
                            // Also, apply specific invalid enum values like ProductType 65.
                            if (!Enum.IsDefined(enumValue.GetType(), enumValue))
                            {
                                isInvalid = true;
                            }
                        }
                       
                    }

                }

                if (isInvalid)
                {
                    invalidFields.Add(new InvalidParameters(field.Name, field.Value));
                }
            }

            return invalidFields;
        }
    }
}
