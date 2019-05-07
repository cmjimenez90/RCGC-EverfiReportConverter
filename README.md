# Everfi Report Converter
![CircleCI branch](https://img.shields.io/circleci/project/github/cmjimenez90/RCGC-EverfiReportConverter/master.svg?label=MASTER&style=plastic)
![CircleCI branch](https://img.shields.io/circleci/project/github/cmjimenez90/RCGC-EverfiReportConverter/develop.svg?label=DEV&style=plastic)

## Current Version *v1.0.0*

## Purpose

Reads a supplied CSV file to memory and imports the data to the specified excel sheet. The excel sheet is a premade template provided by Everfi.
The filled out excel sheet is then submitted to Everfi in order to upload user accounts to their system.

The supplied csv is expected to have the following fields:
- *FIRST_NAME*
- *LAST_NAME*
- *EMAIL*
- *SUPERVISOR*
- *EMPLOYEE_ID*
  *LOCATION_TITLE*
- *LOCATION_ABR*
- *GROUP_TITLE*
- *GROUP_ABR*

This application was designed for a specifc use case. May add the functionality to allow to import any csv to any excel sheet in the future; allowing for a more dynamix use case. This feature is not needed at this time.

## HOW TO USE

The project is built using dotnet and can be built as follows.

**IMPORTANT**
Make sure dotnet core is installed.

- **REQUIRED VERSION**:  > 2.2 

### Build Instructions
1.  Clone the repository; if needed.
2.  Change to the following directory in order to build the project: "{cloned repo path}/src/RCGC.EverfiReportConverter"
    1.  {cloned repo path} = path where the repo was cloned
3.  Run the following command
    1.  dotnet publish -c RELEASE -o {directory} --self-contained -r win-x64
        1.   {directory} = output path of the build application and dlls
4.   Copy output directory to desired location
5.   Adjust appsettings.json configuration as needed.

### App Setting Configuration

- **AppConfiguration** : *REQUIRED* | Paths and locations of template and csv files used during the apps process
  - "ArchiveDirectory" : "Directory to store the csv file after the process is completed"
  - "CSVReportPath" : "Full path of the csv report that will be imported into the Everfi Template"
  - "ExcelTemplatePath" : "Full path of the excel template provided by Everfi"
  - "ReportSavePath" : "The full output path that the template will be saved too after successful importing of the csv data."

- **CSVFieldOverrides** : *OPTIONAL* | CSV Fields that can be overided
  - Fields
    - "FIELD NAME" : "TEXT TO SUBSTITUTE"

- **Serilog** : *Required*
  - "MinimumLevel" : Log level to use | (Verbose, Debug, Information, Warning, Error)
  - "Path" : Path to store the logs of the export process.
