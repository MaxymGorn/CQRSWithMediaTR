using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Windows.Forms;
using WebApplication19.Data;
using WebApplication19.ForEachAsyncExtension;
using Qmate_Entities = Qmate.Preliminary.Registration.General.Data.Qmate_Entities;
namespace WebApplication19.ViewModel
{
    public class ViewModel
    {
        Qmate_Entities entities;
        private static ViewModel instance;
        private ViewModel()
        {
            this.entities = new Qmate_Entities();
            retrieveJobList_Results = GetJobsList(1);
        }
        public static  ViewModel GetInstance()
        {
                if (instance == null)
                    instance = new ViewModel();
                return instance; 
        }
        public int GetJobIdAsync(string name)
        {
            int result = 0;
            Task.Run(async ()=>{
                var text = name.Remove(name.Length - 2);
                using (CancellationTokenSource source = new CancellationTokenSource())
                {
                    CancellationToken token = source.Token;

                    await retrieveJobList_Results.ForEachAsync(retrieveJobList_Results.Count(), async i =>
                    {

                        if (i.nameUkr.Length == text.Length)
                        {
                            Interlocked.Exchange(ref result, i.JobId);
                            token.ThrowIfCancellationRequested();
                        }
                    }, token);
                }
            }).Wait();
            return result;
        }
        public IEnumerable<RetrieveWorkloadTime_Result> GetRetrieveWorkloadTime(string date, int job)
        {
            var collection = entities.RetrieveWorkloadTime(1, date, job);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RetrieveWorkloadTime_Result, RetrieveWorkloadTime_Result>()).CreateMapper();
            return retrieveWorkloadTime_Results = mapper.Map<IEnumerable<RetrieveWorkloadTime_Result>, List<RetrieveWorkloadTime_Result>>(collection);
            //MessageBox.Show(retrieveWorkloadTime_Results.Count().ToString());
        }
        public void GetRetrieveWorkloadDays(string date, int job)
        {
            int countOfDaysFromToday = 60;
            DateTime beginDate = DateTime.Now;
            DateTime endDate = beginDate.AddDays(countOfDaysFromToday);
            string datePattern = @"yyyy-MM-dd HH:mm:ss";
            DateTime dt = DateTime.ParseExact(beginDate.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
            string beginDateForSQL = dt.ToString(datePattern);
            dt = DateTime.ParseExact(endDate.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
            string endDateForSQL = dt.ToString(datePattern);
            var collection = entities.RetrieveWorkloadDays(1,  job, beginDateForSQL, endDateForSQL);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RetrieveWorkloadDays_Result, RetrieveWorkloadDays_Result>()).CreateMapper();
            retrieveWorkloadDays_Results = mapper.Map<IEnumerable<RetrieveWorkloadDays_Result>, List<RetrieveWorkloadDays_Result>>(collection);
        }
        public IEnumerable<RetrieveJobList_Result> GetJobsList(int key = 0)
        {
            return entities.RetrieveJobList(key).ToList();
        }
        public void MakeClient(int? terminalId, string authCode, int? clientId, int? segmentId, string setTime, int? jobId, int? employeeId, int? needApply, string clientPhone, string clientEmail, string commentary, string clientName, string information, string typeInformation, string timeInHold, int? setId, int? notificationType, int? notificationEvt)
        {
            try
            {
                entities.AddClient(terminalId, authCode, clientId, segmentId, setTime, employeeId, needApply, clientPhone, clientEmail, commentary, clientName, information, typeInformation, timeInHold, setId, notificationType, notificationEvt, jobId);
                entities.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public IEnumerable<RetrieveJobList_Result> retrieveJobList_Results { get; set; }
        public IEnumerable<RetrieveWorkloadTime_Result> retrieveWorkloadTime_Results  { get; set; }
        public IEnumerable<RetrieveWorkloadDays_Result> retrieveWorkloadDays_Results { get; set; }
    }
}