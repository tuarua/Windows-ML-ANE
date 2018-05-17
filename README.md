# Windows ML ANE

This is a Windows AIR Native Extension to test the [Windows ML Preview](https://docs.microsoft.com/en-gb/windows/uwp/machine-learning/).  
It also serves as a useful reference for how to call UWP APIs from a C# based ANE.

----------

#### The ANE + Dependencies
From the command line cd into /example and run:
````shell
PS get_dependencies.ps1
`````

##### Windows Installation - Important!
* Copy the contents of the "c_sharp_libs_x86" folder into the bin folder of your AIRSDK. 

The location of this will vary depending on your IDE. These dlls need to reside in the folder where adl.exe is run from.

* This ANE was built with MS Visual Studio 2015. As such your machine (and user's machines) will need to have Microsoft Visual C++ 2015 Redistributable (x86) runtime installed.
https://www.microsoft.com/en-us/download/details.aspx?id=48145

* This ANE also uses .NET 4.7 Framework. This is included with Windows 10 April Update 2018.

* For release builds, the c_sharp_libs_x86 files need to be packaged in the same folder as your exe.  

##### Windows 64bit

AIR29 now includes 32bit and 64bit in the AIR SDK.
However it appears adl.exe is 32bit only. Therefore when debugging use x86 version of the cef and cefsharp dlls.

If you are using the 64bit version for release follow the above instructions replacing x86 with x64 where applicable


### Prerequisites

You will need:
 - Windows 10 April Update 2018 (1803)
 - .NET 4.7
 - AIRSDK 29
 - Visual Studio 2017
 
 
### References
 - https://docs.microsoft.com/en-gb/windows/uwp/machine-learning/
 - https://github.com/ChangweiZhang/Awesome-WindowsML-ONNX-Models
 - https://blogs.windows.com/buildingapps/2017/01/25/calling-windows-10-apis-desktop-application/#3h4lsKlIalPxRWet.97
 - https://msdn.microsoft.com/en-us/library/windows/desktop/mt695951(v=vs.85).aspx
