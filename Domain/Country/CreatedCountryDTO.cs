using System;
using System.Collections.Generic;

namespace Domain.Countries
{
    public class CreatedCountryDTO
    {
        public Guid Id { get; private set; }
        public IList<string> Errors { get; set; }
        public bool IsValid { get; set; }

        public CreatedCountryDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }

        public CreatedCountryDTO(IList<string> errors)
        {
            Errors = errors;
        }
    }
}
