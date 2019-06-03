$currentDir = (Get-Item -Path ".\" -Verbose).FullName
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.0.8/MLANE.ane?raw=true -OutFile "$currentDir\..\native_extension\ane\MLANE.ane"
Invoke-WebRequest -Uri https://github.com/tuarua/FreSharp/releases/download/2.2.0/FreSharp.ane?raw=true -OutFile "$currentDir\..\native_extension\ane\FreSharp.ane"
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.0.8/SqueezeNet.onnx?raw=true -OutFile "$currentDir\src\SqueezeNet.onnx"
