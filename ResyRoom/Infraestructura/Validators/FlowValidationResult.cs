using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Infraestructura.Validators
{
    public class FlowValidationResult : ValidationResult
    {
        public ValidationResultTypes ValidationResultType { get; set; }
        public FlowValidationResult(FlowValidationResult flowValidationResult) : base(flowValidationResult) { }
        public FlowValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        public FlowValidationResult(string errorMessage) : base(errorMessage) { }
    }
}