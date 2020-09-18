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
using Qmate_Entities = Qmate.Preliminary.Registration.General.Data.Qmate_Entities;

namespace WebApplication19.Controllers
{
    public class HomeController : Controller
    {
        Qmate_Entities entities = new Qmate_Entities();
        public ActionResult Index()
        {
            return View(GetJobsList(1));
        }
        private IEnumerable<RetrieveJobList_Result> GetJobsList(int key = 0)
        {
            return entities.RetrieveJobList(key).ToList();
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>")]
        public async Task<JsonResult> SaveDb(int? terminalId, string authCode, int? clientId, int? segmentId, string setTime, int? jobId, int? employeeId, int? needApply, string clientPhone, string clientEmail, string commentary, string clientName, string information, string typeInformation, string timeInHold, int? setId, int? notificationType, int? notificationEvt)
        {
            try
            {
                entities.AddClient(terminalId, authCode, clientId, segmentId, setTime, employeeId, needApply, clientPhone, clientEmail, commentary, clientName, information, typeInformation, timeInHold, setId, notificationType, notificationEvt, jobId);
                await entities.SaveChangesAsync();
            }
            catch(Exception er)
            {
                //MessageBox.Show(er.Message);
            }
            return JsonConvert.DeserializeObject<dynamic>("Success!");
        }
    }
}