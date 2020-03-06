$currentDir = (Get-Item -Path ".\" -Verbose).FullName
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.1.0/MLANE.ane?raw=true -OutFile "$currentDir\..\native_extension\ane\MLANE.ane"
Invoke-WebRequest -Uri https://github.com/tuarua/FreSharp/releases/download/2.4.0/FreSharp.ane?raw=true -OutFile "$currentDir\..\native_extension\ane\FreSharp.ane"
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.1.0/SqueezeNet.onnx?raw=true -OutFile "$currentDir\src\SqueezeNet.onnx"
