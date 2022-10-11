using GenesisMedicalStaffingAgency.IRepositories;
using GenesisMedicalStaffingAgency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GenesisMedicalStaffingAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository; 
        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }
        public ActionResult OurAgency()
        {
            return View();
        }
        public ActionResult WorkWithUs()
        {
            return View();
        }
        public ActionResult CurrentJobOpenings()
        {
            var jobs = _jobRepository.GetJobs();
            return View(jobs);
        }
        public ActionResult EmploymentApplication(int? Id)
        {
            ViewBag.jobId = Id;
            return View();
        }
        public ActionResult InsertEmploymentApplication(ApplyJobs applyJobs)
        {
            int id = _jobRepository.AppliedJob(applyJobs);
            if (id > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult NursingStaffingServices()
        {
            return View();
        }
        public ActionResult PrivateDutyHomeCare()
        {
            return View();
        }
        public ActionResult AddJobs(int? Id)
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                if(Id > 0)
                {
                    var job = _jobRepository.GetSingleJob(Id);
                    return View(job);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditJobById(int? Id)
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                if (Id > 0)
                {
                    var job = _jobRepository.GetSingleJob(Id);
                    return View(job);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteJob(int? Id)
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                if (Id > 0)
                {
                    _jobRepository.DeleteJob(Id);
                    return RedirectToAction("CurrentJobOpenings", "Home");
                }
                return RedirectToAction("CurrentJobOpenings", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AddJobsInDB(Jobs jobs)
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                if (jobs.Id != 0 || jobs.Id != null)
                {
                    int id = _jobRepository.EditJob(jobs);
                    if (id > 0)
                    {
                        return RedirectToAction("CurrentJobOpenings", "Home");
                    }
                    return RedirectToAction("AddJobs", "Home");
                }
                else
                {
                    int id = _jobRepository.AddJobs(jobs);
                    if (id > 0)
                    {
                        return RedirectToAction("CurrentJobOpenings", "Home");
                    }
                    return RedirectToAction("AddJobs", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ViewEmployeeApplication()
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                var records = _jobRepository.GetAllApplyJobs();
                return View(records);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
