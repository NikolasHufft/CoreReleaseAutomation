using AutoMapper;
using CoreReleaseAutomation.Helpers;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CoreReleaseAutomation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseController : ControllerBase
    {
        private readonly ILogger<ReleaseController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISetup setup;
        private readonly IWebHostEnvironment _env;

        public ReleaseController(ILogger<ReleaseController> log, IHttpContextAccessor hca, IUnitOfWork uow, IMapper map, ISetup stp, IWebHostEnvironment env)
        {
            logger = log ?? throw new ArgumentNullException(nameof(logger));
            httpContextAccessor = hca ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            unitOfWork = uow ?? throw new ArgumentNullException(nameof(unitOfWork));
            mapper = map ?? throw new ArgumentNullException(nameof(mapper));
            setup = stp ?? throw new ArgumentNullException(nameof(setup));
            _env = env;
        }

        [HttpGet("releaseType")]
        [Route("CreatePatchFolders")]
        [Authorize]
        public string CreatePatchFolders(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder();
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(releaseType);
                var pathOrigem = (releaseType == "Hotfix") ? setup.PathHotfix : setup.PathEndOfSprint;
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                //Create Patch Folders                                
                var extractedFilesFolder = Path.Combine(pathDestiny, "Extracted Files");
                var testPlansFolder = Path.Combine(pathDestiny, "Test Plans");

                executiopnLog.Append(FileHelper.CreateRootFolder(pathDestiny)); // Patch 18.1                    
                executiopnLog.Append(FileHelper.CreateRootFolder(testPlansFolder)); // Patch 18.1\Test Plans

                return executiopnLog.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet("releaseType")]
        [Route("CopyAllReleaseFilesToPatchFolder")]
        [Authorize]
        public string CopyAllReleaseFilesToPatchFolder(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder();
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(releaseType);
                var pathOrigem = (releaseType == "Hotfix") ? setup.PathHotfix : setup.PathEndOfSprint;
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                //Copy All Release Files to Patch Folder
                FileHelper.CloneDirectory(pathOrigem, pathDestiny);
                return executiopnLog.Append("\nCopy All Release Files to Patch Folder completed").ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet("releaseType")]
        [Route("DeleteConfigs")]
        [Authorize]
        public string DeleteConfigs(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder();
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(releaseType);
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                var extractedFilesFolder = Path.Combine(pathDestiny, "Extracted Files");

                //Delete Configs -  Patch Folder
                var listOfConfigPath = new List<string>() {
                        Path.Combine(extractedFilesFolder, @"CR Portal\CRP.default.config"),
                        Path.Combine(extractedFilesFolder, @"Inteport\Web.default.config"),
                        Path.Combine(extractedFilesFolder, @"Integate\Views\Web.config"),
                        Path.Combine(extractedFilesFolder, @"Integate\Web.default.config"),
                        Path.Combine(extractedFilesFolder, @"Inteflow Engine\Inteflow Engine.default.config")
                };

                return executiopnLog.Append(FileHelper.DeleteConfigFiles(listOfConfigPath, newVersion.Version)).ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet("releaseType")]
        [Route("AddVersionToTxtFiles")]
        [Authorize]
        public string AddVersionToTxtFiles(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder();
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(releaseType);
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                var extractedFilesFolder = Path.Combine(pathDestiny, "Extracted Files");

                //Add version to TXT files - Patch Folder
                var listOfVersionFiles = new List<string>() {
                        Path.Combine(extractedFilesFolder, @"CR Portal\Version.txt"),
                        Path.Combine(extractedFilesFolder, @"Inteport\Version_Inteport.txt"),
                        Path.Combine(extractedFilesFolder, @"Integate\Version_Integate.txt"),
                        Path.Combine(extractedFilesFolder, @"Inteflow Engine\Version_Inteflow Engine.txt")
                };

                return executiopnLog.Append(FileHelper.AppendVersionToFile(listOfVersionFiles, $"Version: {newVersion.Version}.{newVersion.Patch}")).ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet("releaseType")]
        [Route("DeleteReleaseFolderAndFiles")]
        [Authorize]
        public string DeleteReleaseFolderAndFiles(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder("Release Started.");
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(releaseType);
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                var extractedFilesFolder = Path.Combine(pathDestiny, "Extracted Files");

                //Delete Release Folder and Files
                var endOfSprintAndHotFixPaths = new List<string>() { Path.Combine(setup.PathHotfix, "Extracted Files"), Path.Combine(setup.PathEndOfSprint, "Extracted Files") };

                return executiopnLog.Append(FileHelper.DeleteEndOfSprintAndHotFixFolder(endOfSprintAndHotFixPaths)).ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet()]
        [Route("CreateJobTracker")]
        [Authorize]
        public string CreateJobTracker(JobTrackerViewModel jobTracker)
        {
            try
            {
                var executiopnLog = new StringBuilder("Release Started.");
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(jobTracker.ReleaseType);
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                //Create JobTracker
                return JobTrackerHelper.SendJobTracker(AuthenticationHelper.UserId(httpContextAccessor), jobTracker.Description, pathDestiny, jobTracker.ReleaseName,
                                setup.JobTrackerUser, setup.JobTrackerPassword, jobTracker.Version, setup.InterPortUrl, _env.ContentRootPath);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpGet("releaseType")]
        [Route("UpdateCoredevWithNewVersion")]
        [Authorize]
        public string UpdateCoredevWithNewVersion(string releaseType)
        {
            try
            {
                var executiopnLog = new StringBuilder();

                //Update Coredev with new version                                
                unitOfWork.LogVersionRepository.Remove(unitOfWork.LogVersionRepository.GetOldVersion());
                unitOfWork.LogVersionRepository.Add(unitOfWork.LogVersionRepository.GetNewVersion(releaseType));
                unitOfWork.Commit();

                return executiopnLog.Append("\nVersion has been updated.").ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("SaveRelease")]
        [Authorize]
        public string SaveRelease(ReleaseViewModel release)
        {
            try
            {
                var executiopnLog = new StringBuilder("Release Started.");
                var newVersion = unitOfWork.LogVersionRepository.GetNewVersion(release.ReleaseType);
                var pathOrigem = (release.ReleaseType == "Hotfix") ? setup.PathHotfix : setup.PathEndOfSprint;
                var pathDestiny = Path.Combine(setup.PathDestiny, $"Patch {newVersion.Patch}");

                var newRelease = mapper.Map<ReleaseViewModel, Release>(release);

                newRelease.Version = $"{newVersion.Version}.{newVersion.Patch}";
                newRelease.JobTracker = release.JobTracker;
                newRelease.ExecutionLog = release.ExecutionLog;
                newRelease.Description = WebUtility.HtmlEncode(release.Description);
                newRelease.Status = "Waiting Approval";
                newRelease.PathOrigem = pathOrigem;
                newRelease.PathDestiny = pathDestiny;
                newRelease.Manager = release.Manager;
                newRelease.dt_modify = DateTime.Now;
                newRelease.id_modify = AuthenticationHelper.UserId(httpContextAccessor);

                //save Release
                unitOfWork.ReleaseRepository.Add(newRelease);
                executiopnLog.Append("\nNew Release Saved.");

                //Save Database changes
                unitOfWork.Commit();

                return executiopnLog.Append("\nRelease Completed").ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message} - Details: {ex.StackTrace}");
                return ex.Message;
            }
        }
    }
}
