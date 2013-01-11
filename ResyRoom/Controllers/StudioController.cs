using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResyRoom.Models;

namespace ResyRoom.Controllers
{
    public class StudioController : Controller
    {
        #region >>> Configure Studio's view
        public ActionResult Configure(Estudio estudio)
        {
            return View(estudio);
        }
        #endregion
    }
}
