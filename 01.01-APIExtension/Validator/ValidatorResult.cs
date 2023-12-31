using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.Validator
{
    public class ValidatorResult
    {
        public ValidatorResult()
        {
            Failures = new List<string?>();
        }

        public bool IsValid => !Failures.Any();

        public List<string?> Failures { get; set; }
    }
}
