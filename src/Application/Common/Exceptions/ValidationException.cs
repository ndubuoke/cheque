using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace ChequeMicroservice.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            IEnumerable<IGrouping<string, string>> failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (IGrouping<string, string> failureGroup in failureGroups)
            {
                string propertyName = failureGroup.Key;
                string[] propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }

        public string GetErrors()
        {
            string errors = null;
            try
            {
                if (Errors.Any())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (KeyValuePair<string, string[]> error in Errors)
                    {
                        stringBuilder.AppendFormat(",", error);
                    }
                }
                return errors?.TrimEnd(';');
            }
            catch (Exception ex) { return errors; }
        }
    }
}