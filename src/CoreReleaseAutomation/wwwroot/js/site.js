function onReleaseTypeChanged()
{
    var str = document.getElementById("Description").value;
    var type = document.getElementById("Hotfix");
    var newStr = "";

    if (type.checked === true) {
        newStr = str.replace("End Of Sprint:", "Hotfix PDR:");        
    }
    else {
        newStr = str.toString().replace("Hotfix PDR:", "End Of Sprint:");
    }

    document.getElementById("Description").value = newStr;
}

function onOverrideChanged() {
    
    var overrideNameYes = document.getElementById("OverrideNameYes");
    var releaseName = document.getElementById("ReleaseName");
    
    if (overrideNameYes.checked === true) {        
        releaseName.readOnly = false;
    }
    else {        
        releaseName.readOnly = true;
    }    
}

function submitRelease() {    
    if (confirm('Are you sure you want to execute this release?')) {
        executeReleaseProcess();
    } else {
        return false;
    }
}

/* Release Functions */
function executeReleaseProcess()
{
    $.when(CreatePatchFolders(), CopyAllReleaseFilesToPatchFolder(), DeleteConfigs(), AddVersionToTxtFiles(),
        DeleteReleaseFolderAndFiles(), CreateJobTracker(), UpdateCoredevWithNewVersion(), SaveRelease()).done(function () {
            Complete();
    });   
}

function CreatePatchFolders() {

    if (document.getElementById("CopyFiles") === "No") return;

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/CreatePatchFolders?releaseType=' + releaseType,        
        success: function () {
            executionLog.value = executionLog.value + "\nCreate Patch Folders Completed.";            
        },
        error: function () {
            executionLog.value = executionLog.value + "\nCreate Patch Folders Failed.";            
        }
    });
}

function CopyAllReleaseFilesToPatchFolder() {

    if (document.getElementById("CopyFiles") === "No") return;

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/CopyAllReleaseFilesToPatchFolder?releaseType=' + releaseType,
        success: function () {
            executionLog.value = executionLog.value + "\nCopy All Release Files To Patch Folder Completed.";
        },
        error: function () {
            executionLog.value = executionLog.value + "\nCopy All Release Files To Patch Folder Failed.";
        }
    });
}

function DeleteConfigs() {

    if (document.getElementById("CopyFiles") === "No") return;

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/DeleteConfigs?releaseType=' + releaseType,
        success: function () {
            executionLog.value = executionLog.value + "\nDelete Configs Completed.";
        },
        error: function () {
            executionLog.value = executionLog.value + "\nDelete Configs Failed.";
        }
    });
}

function AddVersionToTxtFiles() {

    if (document.getElementById("CopyFiles") === "No") return;

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/AddVersionToTxtFiles?releaseType=' + releaseType,
        success: function () {
            executionLog.value = executionLog.value + "\nAdd Version To Txt Files Completed.";
        },
        error: function () {
            executionLog.value = executionLog.value + "\nAdd Version To Txt Files Failed.";
        }
    });
}

function DeleteReleaseFolderAndFiles() {

    if (document.getElementById("CopyFiles") === "No") return;

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/DeleteReleaseFolderAndFiles?releaseType=' + releaseType,
        success: function () {
            executionLog.value = executionLog.value + "\nDelete Release Folder And Files Completed.";
        },
        error: function () {
            executionLog.value = executionLog.value + "\nDelete Release Folder And Files Failed.";
        }
    });
}

function CreateJobTracker() {
    
    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";
    var releaseName = document.getElementById("ReleaseName");
    var jobTracker = document.getElementById("JobTracker");

    var jobTrackerData = { ReleaseType: releaseType, ReleaseName: releaseName };

    $.ajax({
        type: 'POST',
        url: './api/Release/CreateJobTracker',        
        data: jobTrackerData,
        dataType: 'json',
        success: function (data) {
            
            jobTracker.value = data.value;
            executionLog.value = executionLog.value + "\nCreate JobTracker Completed.";            
        },
        error: function (data) {
            executionLog.value = executionLog.value + "\nCreate JobTracker Failed.\n" + data;                       
            jobTracker.value = data.value;
        }
    });
}

function UpdateCoredevWithNewVersion() {

    var executionLog = document.getElementById("ExecutionLog");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    $.ajax({
        type: 'GET',
        url: './api/Release/UpdateCoredevWithNewVersion?releaseType=' + releaseType,
        success: function () {
            executionLog.value = executionLog.value + "\nUpdate Log Version With the New Version Completed.";
        },
        error: function () {
            executionLog.value = executionLog.value + "\nUpdate Log Version With the New Version Failed.";
        }
    });
}

function SaveRelease() {
    
    var executionLog = document.getElementById("ExecutionLog");
    var jobTracker = document.getElementById("JobTracker");
    var releaseName = document.getElementById("ReleaseName");
    var manger = document.getElementById("Manger");
    var releaseType = document.getElementById("Hotfix").checked === true ? "Hotfix" : "EndOfSprint";

    var release = { ReleaseType: releaseType, ReleaseName: releaseName, JobTracker: jobTracker, Manger: manger};

    $.ajax({
        type: 'POST',
        url: './api/Release/SaveRelease',        
        data: release,        
        dataType: 'json',
        success: function (data) {            
            executionLog.value = executionLog.value + "\nSave Release Completed.";            
        },
        error: function (data) {
            executionLog.value = executionLog.value + "\nSave Release Failed.\n" + data;            
        }
    });
}

function Complete() {
    
    var submitRelease = document.getElementById("submitRelease");
    var backToList = document.getElementById("backToList");

    //Hidden Save release button
    submitRelease.style.visibility = 'hidden';

    //Show link to redirect to home
    backToList.style.visibility = 'visible';

    //change all fields to readonly
    $(':input').attr('readonly', 'readonly');
    $(':textarea').attr('readonly', 'readonly');
    $(':radio').attr('readonly', 'readonly');
    $(':checkbox').attr('readonly', 'readonly');

    alert("Release Execection Complete.");
}
/* End Release Functions */