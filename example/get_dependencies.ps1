$currentDir = (Get-Item -Path ".\" -Verbose).FullName
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.0.5/MLANE.ane?raw=true -OutFile "$currentDir\..\native_extension\ane\MLANE.ane"
Invoke-WebRequest -Uri https://github.com/tuarua/Windows-ML-ANE/releases/download/0.0.5/SqueezeNet.onnx?raw=true -OutFile "$currentDir\src\SqueezeNet.onnx"
