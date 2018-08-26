@echo off
"c:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.exe" /build "smartassembly\crfsuite.{sa}proj"
sleep 1
"C:\Program Files (x86)\Inno Setup 5\Compil32.exe" /cc CRFSuiteInstall.iss
"c:\Program Files (x86)\reflector\Reflector.exe" Output\CRFSuite.exe
