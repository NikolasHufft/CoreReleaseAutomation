# CoreReleaseAutomation

web page which will be hosted on CoreTest to automate all the manual steps in our core release.

 

Steps to Automate Core Release

·         Create a mvc form that will list the steps that needs to be performed for the core release. This should use AD to authenticate the user.

o   Release Type

§  Radio button options

·         Hotfix

·         EndofSprint

o   Release name

§  Prepopulated & disabled text field “CoreRelease-5.2.0.12”

§  Release name value should be read from the database – t_log_version

§  Checkbox underneath the Release name text field to overwrite the text field value.

·         If the checkbox is ticked – enable the text field and ensure the field contains no space as we would be using this as the Job Tracker ID

o   Release Desc

§  Text area to detail Impact assessment/install instructions/testing instruction for the release

o   Copy files to patch folder

§  Checkbox – if checked, copy files from (S:\Products\Inteflow\Version 5\Binary Releases\Version 5.2.0\Hotfix or EndOfSprint) to S:\Products\Inteflow\Version 5\Binary Releases\Version 5.2.0\Patch No folder

o   Manager Approval

§  Drop down list with a list of managers

§  This should be configurable.

 

Once development has been completed for a hotfix or endofsprint patch, the developer would be expected to navigate to this web page in order to initiate the core release process. Once the dev submits the above form following process should be kicked off

·         Create a job tracker where the AppId would be the Release name

·         Job Tracker user credentials should be configurable through setting

·         Once the job tracker has been submitted, copy the files from the Hotfix/EndofSprint folder to the patch folder. Ensure that files have been copied to the right folder structure.

·         Create Test plans folder inside the patch folder

·         Update the version number in all folders

·         Check if the release has been authorised

o   If yes, Send email to Core & Product team to notify that the release has been authorised.

·         Create new database table to track all the core releases

·         The web page should also display the progress of the core process. 
