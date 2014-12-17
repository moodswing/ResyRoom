using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResyRoom.Infraestructura.Validators
{
    public class CustomModelError : ModelError
    {
        public CustomModelError(string errorMessage)
            : base(errorMessage)
        { }

        public CustomModelError(Exception exception)
            : base(exception)
        { }

        public CustomModelError(Exception exception, string errorMessage)
            : base(exception, errorMessage)
        { }

        /// <summary>
        /// Defines the kind of validation result this instance represents
        /// </summary>
        public ValidationResultTypes ValidationResultType { get; set; }
    }
}