using AutoMapper;
using CoreReleaseAutomation.Helpers;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace CoreReleaseAutomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISetup setup;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> log, IHttpContextAccessor hca, IUnitOfWork uow, IMapper map, ISetup stp, IWebHostEnvironment env)
        {
            logger = log ?? throw new ArgumentNullException(nameof(logger));
            httpContextAccessor = hca ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            unitOfWork = uow ?? throw new ArgumentNullException(nameof(unitOfWork));
            mapper = map ?? throw new ArgumentNullException(nameof(mapper));
            setup = stp ?? throw new ArgumentNullException(nameof(setup));
            _env = env;
        }

        //[Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            if (!AuthenticationHelper.IsLoggedIn(httpContextAccessor)) return RedirectToAction("Index", "Login");

            var result = await unitOfWork.ReleaseRepository.GetAll();

            var list = mapper.Map<IEnumerable<Release>, IEnumerable<HomeViewModel>>(result);

            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult AutoLogin()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            var newVersion = unitOfWork.LogVersionRepository.GetNewVersion("Hotfix");

            var release = new HomeViewModel
            {
                ReleaseName = $"CoreRelease-{newVersion.Version}.{newVersion.Patch}",
                Managers = setup.Managers,
                Manager = "Ash",
                Description = WebUtility.HtmlDecode(setup.Description),
                StatusList = setup.StatusList
            };

            return View(release);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(HomeViewModel release) {

            return RedirectToAction("Index", "Home");

        }

        #region Release Process in the Controller          
        //[HttpPost]
        //[Authorize]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult Create(HomeViewModel release)
        //{                                                 
        //    if (!ModelState.IsValid) return View(release);

        //    try
        //    {
        //        var executiopnLog = new StringBuilder("Release Started.");
        //        var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(release.ReleaseType);
        //        var pathOrigem = "";
        //        var pathDestiny = "";

        //        if (release.CopyFiles == "Yes")
        //        {
        //            //Hotfix or EndOfSprint
        //            pathOrigem = (release.ReleaseType == "Hotfix") ? setup.PathHotfix : setup.PathEndOfSprint;

        //            //Create Patch Folders
        //            pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");
        //            var extractedFilesFolder = Path.Combine(pathDestiny, "Extracted Files");
        //            var testPlansFolder = Path.Combine(pathDestiny, "Test Plans");

        //            executiopnLog.Append(FileHelper.CreateRootFolder(pathDestiny)); // Patch 18.1                    
        //            executiopnLog.Append(FileHelper.CreateRootFolder(testPlansFolder)); // Patch 18.1\Test Plans

        //            //Copy All Release Files to Patch Folder
        //            FileHelper.CloneDirectory(pathOrigem, pathDestiny);
        //            executiopnLog.Append("\nCopy All Release Files to Patch Folder completed");

        //            //Delete Configs -  Patch Folder
        //            var listOfConfigPath = new List<string>() {
        //                Path.Combine(extractedFilesFolder, @"CR Portal\CRP.default.config"),
        //                Path.Combine(extractedFilesFolder, @"Inteport\Web.default.config"),
        //                Path.Combine(extractedFilesFolder, @"Integate\Views\Web.config"),
        //                Path.Combine(extractedFilesFolder, @"Integate\Web.default.config"),
        //                Path.Combine(extractedFilesFolder, @"Inteflow Engine\Inteflow Engine.default.config")
        //            };

        //            executiopnLog.Append(FileHelper.DeleteConfigFiles(listOfConfigPath, release.Version));

        //            //Add version to TXT files - Patch Folder
        //            var listOfVersionFiles = new List<string>() {
        //                Path.Combine(extractedFilesFolder, @"CR Portal\Version.txt"),
        //                Path.Combine(extractedFilesFolder, @"Inteport\Version_Inteport.txt"),
        //                Path.Combine(extractedFilesFolder, @"Integate\Version_Integate.txt"),
        //                Path.Combine(extractedFilesFolder, @"Inteflow Engine\Version_Inteflow Engine.txt")
        //            };

        //            executiopnLog.Append(FileHelper.AppendVersionToFile(listOfVersionFiles, $"Version: {newVersion.Version}.{newVersion.Patch}"));

        //            //Delete Release Folder and Files
        //            var endOfSprintAndHotFixPaths = new List<string>() { Path.Combine(setup.PathHotfix, "Extracted Files"), Path.Combine(setup.PathEndOfSprint, "Extracted Files") };
        //            executiopnLog.Append(FileHelper.DeleteEndOfSprintAndHotFixFolder(endOfSprintAndHotFixPaths));
        //        }
        //        else
        //        {
        //            executiopnLog.Append("No files has been copied.");
        //        }

        //        var newRelease = mapper.Map<HomeViewModel, Release>(release);

        //        newRelease.Version = $"{newVersion.Version}.{newVersion.Patch}";

        //        //Create JobTracker
        //        var jobTracker = JobTrackerHelper.SendJobTracker(AuthenticationHelper.UserId(httpContextAccessor), newRelease.Description, pathDestiny, newRelease.ReleaseName, 
        //                        setup.JobTrackerUser, setup.JobTrackerPassword, newRelease.Version, setup.InterPortUrl, _env.ContentRootPath);

        //        //Update Coredev with new version                                
        //        unitOfWork.LogVersionRepository.Remove(unitOfWork.LogVersionRepository.GetOldVersion());
        //        unitOfWork.LogVersionRepository.Add(unitOfWork.LogVersionRepository.GetNewVersion(release.ReleaseType));
        //        executiopnLog.Append("\nVersion has been updated.");

        //        //save Release
        //        newRelease.JobTracker = jobTracker.ToString();
        //        newRelease.ExecutionLog = executiopnLog.ToString();
        //        newRelease.Description = WebUtility.HtmlEncode(setup.Description);
        //        newRelease.Status = "Waiting Approval";
        //        newRelease.PathOrigem = pathOrigem;
        //        newRelease.PathDestiny = pathDestiny;
        //        newRelease.dt_modify = DateTime.Now;
        //        newRelease.id_modify = AuthenticationHelper.UserId(httpContextAccessor);

        //        unitOfWork.ReleaseRepository.Add(newRelease);
        //        executiopnLog.Append("\nNew Release Saved.");

        //        //Save Database changes
        //        unitOfWork.Commit();
        //        executiopnLog.Append("\nRelease Completed");

        //        return RedirectToAction("Details", "Home", new { id = newRelease.ReleaseId });
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");

        //        return View(release);
        //    }            
        //}
        #endregion

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Home");

            var result = await unitOfWork.ReleaseRepository.GetById(id);

            return View(result);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Home");

            var result = await unitOfWork.ReleaseRepository.GetById(id);

            unitOfWork.ReleaseRepository.Remove(result);
            await unitOfWork.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult ReleaseApprovedById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Home");

            var result = unitOfWork.ReleaseRepository.GetReleaseWaitingApprovalById(id);

            if (JobTrackerHelper.CheckStatusJobTracker(setup.InterPortUrl, result.JobTracker))
            {
                EmailHelper.SendEmail(result.ReleaseType, result.Version, result.PathDestiny, result.Description, setup.EmailSubject, setup.EmailBody, setup.EmailFrom, setup.EmailTo, setup.Smtp);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult ReleaseApproved()
        {
            var result = unitOfWork.ReleaseRepository.GetAllReleaseWaitingApproval();

            foreach (var item in result)
            {
                if (JobTrackerHelper.CheckStatusJobTracker(setup.InterPortUrl, item.JobTracker))
                {
                    EmailHelper.SendEmail(item.ReleaseType, item.Version, item.PathDestiny, item.Description, setup.EmailSubject, setup.EmailBody, setup.EmailFrom, setup.EmailTo, setup.Smtp);

                    item.Status = "Approved";
                    unitOfWork.ReleaseRepository.Update(item);
                    unitOfWork.Commit();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
