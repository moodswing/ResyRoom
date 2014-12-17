using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResyRoom.Infraestructura.Validators
{
    public class CustomValidationResult
    {
        public class CustomModelValidationResult : ModelValidationResult
        {
            public ValidationResultTypes ValidationResultType { get; set; }
        }
    }

    public enum ValidationResultTypes
    {
        Warning = 0x00000001,
        Critical = 0x00000010,
        Informational = 0x00000100,
        None = 0x00001000
    }
}