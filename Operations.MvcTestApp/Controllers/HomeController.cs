using System.Threading.Tasks;
using System.Web.Mvc;

namespace Operations.MvcTestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> IndexAsync()
        {
            using (Op.Start("async test"))
            {
                await Worker.DoWorkAsync();
                return View("Index");
            }
        }

        public ActionResult Error()
        {
            Op.Run(nameof(Worker.DoWorkWithError), Worker.DoWorkWithError);
            return View("Index");
        }

        public async Task<ActionResult> ErrorAsync()
        {
            using (Op.Start("async error - parent action"))
            {
                await Op.RunAsync($"{nameof(Worker)}.{nameof(Worker.DoWorkWithErrorAsync)}", 
                    Worker.DoWorkWithErrorAsync, Op.Context(new { Parent = "async error - parent action" })); 
                return View("Index");
            }
        }

        public virtual ActionResult ChildAction(int number)
        {
            using (Op.Start("some child work"))
            {
                Worker.DoWorkAsync();
                return PartialView("Child", number);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}