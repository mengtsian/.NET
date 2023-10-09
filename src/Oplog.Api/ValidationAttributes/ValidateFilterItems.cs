using Oplog.Core.Commands.CustomFilters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.ValidationAttributes
{
    public class ValidateFilterItems : ValidationAttribute
    {
        public int NoOfFilters { get; set; }

        public override bool IsValid(object value)
        {
            if (value is IList<CreateCustomFilterItem> list)
            {
                if (list.Count < NoOfFilters)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
