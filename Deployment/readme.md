Compare Server Sample Deployment Script
=========================================

*DeploySamples.ps1* is a powershell script which creates websites with the needed settings then deploy the samples to IIS.

To be able to use the script, running script must be enabled on your system. Open PowerShell and run:

		
		Set-ExecutionPolicy RemoteSigned



The samples should not be deployed in a User sub-directory like Desktop, Documents,.. Please, avoid all locations like: C:\Users\{USERNAME}\Path .

To deploy the samples please do as follows:

+ Create a folder on a local Disk, for example C:\Compare-Server\
+ Copy the compare server installer, samples and scripts there.
+ Deploy the samples from that location.



Run the script with no parameters
----------------------------------

You can run the script with no parameters, as long as the Samples folder is in the current directory.
A folder "Websites" will be created in the current location and used for the deployment.



Run the script with parameters
------------------------------

Two parameters can be provided:

+ SamplePath: this is the location of the samples. It must contain the following directories:
	- AdvWebSample
	- BasicWebSample
	- ConfigPageSample
	- WebAdmin
	- WebClient

+ WebsitePath: This is the location that will be used to create the websites and deploy the samples.

	For example:


		 .\DeploySamples.ps1 -WebsitePath "C:\websites" -SamplePath "C:\samples"



Note that the paths provided must be absolute, otherwise the script will not execute and display an error: "Cannot validate argument on parameter ..."