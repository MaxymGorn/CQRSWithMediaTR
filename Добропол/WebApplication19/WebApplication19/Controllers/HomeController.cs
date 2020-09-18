using Newtonsoft.Json;
using Qmate.Preliminary.Registration.General.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows.Forms;
using WebApplication19.Data;
using WebApplication19.ViewModel;

namespace WebApplication19.Controllers
{
    public class HomeController : Controller
    {
        public ViewModel.ViewModel viewModel;
        public HomeController()
        {
            viewModel=  ViewModel.ViewModel.GetInstance();
        }
        public ActionResult Index()
        {
            return View(viewModel);
        }

        [HttpGet]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>")]
        public async Task<ActionResult> SetWorkTime(string date, string job)
        {
               return PartialView("SetWorkTime", viewModel.GetRetrieveWorkloadTime(date, viewModel.GetJobIdAsync(job)));

        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>")]
        public async Task<JsonResult> SaveDb(int? terminalId, string authCode, int? clientId, int? segmentId, string setTime, string jobId, int? employeeId, int? needApply, string clientPhone, string clientEmail, string commentary, string clientName, string information, string typeInformation, string timeInHold, int? setId, int? notificationType, int? notificationEvt)
        {
            viewModel.MakeClient(terminalId, authCode, clientId, segmentId, setTime, viewModel.GetJobIdAsync(jobId), employeeId, needApply, clientPhone, clientEmail, commentary, clientName, information, typeInformation, timeInHold, setId, notificationType, notificationEvt);
            return JsonConvert.DeserializeObject<dynamic>("Success!");
        }
    }
}