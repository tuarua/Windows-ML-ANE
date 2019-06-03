# Windows ML ANE

This is a Windows AIR Native Extension to test [Windows Machine Learning](https://docs.microsoft.com/en-gb/windows/uwp/machine-learning/).  
It also serves as a useful reference for how to call UWP APIs from a C# based ANE.

----------

#### The ANE + Dependencies
From the command line cd into /example and run:
````shell
PS get_dependencies.ps1
`````

##### Windows Installation - Important!
The C# binaries(dlls) are now packaged inside the ANE. All of these **need to be deleted** from your AIRSDK.     
FreSharp.ane is now a required dependency for Windows. 

* This ANE was built with MS Visual Studio 2015. As such your machine (and user's machines) will need to have Microsoft Visual C++ 2015 Redistributable (x86) runtime installed.
https://www.microsoft.com/en-us/download/details.aspx?id=48145

* This ANE also uses .NET 4.7 Framework. This is included with Windows 10 April Update 2018.

### Prerequisites

You will need:
 - Windows 10 October Update 2018 (1809)
 - .NET 4.7
 - AIRSDK 32
 - Visual Studio 2017
 
 
### References
 - https://docs.microsoft.com/en-gb/windows/uwp/machine-learning/
 - https://blogs.windows.com/buildingapps/2017/01/25/calling-windows-10-apis-desktop-application/#3h4lsKlIalPxRWet.97
 - https://msdn.microsoft.com/en-us/library/windows/desktop/mt695951(v=vs.85).aspx
