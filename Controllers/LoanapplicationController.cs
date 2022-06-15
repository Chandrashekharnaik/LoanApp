using LoanApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanApp.Controllers
{
    public class LoanapplicationController : Controller
    {
        // GET: Loanapplication
        // GET: LoanApplication
        public ActionResult SaveFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveFile(HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            string path = Server.MapPath("~/App_Data/File");
            string fileName = Path.GetFileName(file.FileName);
            string fileName1 = Path.GetFileName(file1.FileName);
            string filename2 = Path.GetFileName(file2.FileName);

            string fullPath = Path.Combine(path, fileName);
            string combinepath = Path.Combine(path, fileName1);
            string combinepath1 = Path.Combine(path, filename2);

            file.SaveAs(fullPath);
            file1.SaveAs(combinepath);
            file2.SaveAs(combinepath1);
            if (file == null)
            {
                return View();
            }

            return RedirectToAction("LoanTracker");
        }


        [Authorize]
        public ActionResult Application()
        {

            LoanApplication Loanapp = new LoanApplication();

            return View(Loanapp);
        }


        [HttpPost]
        public ActionResult Application(LoanApplication LoanApp)
        {

            using (HomeLoanSiteEntities dbmodel = new HomeLoanSiteEntities())
            {
                if (dbmodel.LoanApplications.Any(x => x.PanNumber == LoanApp.PanNumber))
                {
                    ViewBag.DuplicateMessage = "Already Apllied on this PanNumber";
                    return View("Application", LoanApp);
                }
                dbmodel.LoanApplications.Add(LoanApp);
                try
                {

                    dbmodel.SaveChanges();

                }
                catch (DbEntityValidationException dbEx)
                {

                    //Console.WriteLine(dbEx);
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {



                            return View("Application");
                        }
                    }
                }

            }
            ModelState.Clear();
            return RedirectToAction("SaveFile");
        }



        //[HttpGet]
        public ActionResult LoanTracker()
        {

            LoanApplication loanapp = new LoanApplication();
            return View();
        }
        //[Authorize]
        [HttpPost]
        public ActionResult LoanTracker(LoanApplication loanapp)
        {

            using (HomeLoanSiteEntities dbmodel = new HomeLoanSiteEntities())
            {
                // var sqrlw = dbmodel.LoanApplications.SelectMany(x => x.Verified,dbmodel.LoanApplications.Where(y =>y.PanNumber == loanapp.PanNumber));


                // var sqrlw = dbmodel.LoanApplications.Where{ x => x.PanNumber == loanapp.PanNumber}, Select new { x => x.Verified == true };

                var sqrlw = dbmodel.LoanApplications.
                   Where(x => x.PanNumber == loanapp.PanNumber)
                   .Select(stu => stu.Verified==true);
                //  bool r = Convert.ToBoolean(sqrlw);


                var sqr = dbmodel.LoanApplications.
                 Where(x => x.PanNumber == loanapp.PanNumber)
                 .Select(stu => stu.Approved == true);



                var sql = dbmodel.LoanApplications.Where(x => x.PanNumber == loanapp.PanNumber).Count();
                
              
                    if (sql >0)

                    {
                        foreach (var n in sqrlw)
                        {
                            if (n == true)
                            {
                                ViewBag.Verifymsg = "Your application is verified sent for approval...";
                                foreach (var item in sqr)
                                {
                                    if (item == true)
                                    {
                                        ViewBag.Approvalmsg = "Your application is approved money will transfer soon...";
                                    }
                                    else
                                    {
                                        ViewBag.NotApprovalmsg = "your application is not approved wait for approval";
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.NotVerifymsg = "Your Application is not verified Wait for Verification ";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Userreg = "You are not applied for loan ";
                    }
                
                   
                
                


               



                //var verify = (dbmodel.LoanApplications.Where(x => x.Verified == true));
                ////var sql = (dbmodel.LoanApplications.Where(x => x.Verified == true).Count());

                //var sqlr = (dbmodel.LoanApplications.Where(x => x.PanNumber == loanapp.PanNumber && x.Verified == true));
                //var approve = dbmodel.LoanApplications.Where(x => x.Approved == true);
                //if (sqrlw > 0)
                //{
                //    ViewBag.Verifymsg = "Your application is verified sent for approval";

                //    // IQueryable<LoanApplications> sqrlw1 = (bool)sqrlw;
                //    if (sqr>0)
                //    {
                //        ViewBag.Approvalmsg = "Your application is approved money will transfer soon";

                //    }
                //}

                //else
                //{
                //    ViewBag.NewMessage = "Not Verified wait for verification";
                //    return View("LoanTracker");
                //}

            }
            return View();

        }
    }
}
