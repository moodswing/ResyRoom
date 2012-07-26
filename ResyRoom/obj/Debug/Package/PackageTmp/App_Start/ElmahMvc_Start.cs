[assembly: WebActivator.PreApplicationStartMethod(typeof(ResyRoom.App_Start.ElmahMvc), "Start")]
namespace ResyRoom.App_Start
{
    public class ElmahMvc
    {
        public static void Start()
        {
            Elmah.Mvc.Bootstrap.Initialize();
        }
    }
}