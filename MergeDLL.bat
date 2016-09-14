G:
cd G:\FKForm\FKForm\ForGits
ILMerge.exe /target:library /targetplatform:v4 /out:G:\FKForm\FKForm\ForGits\FKForm.dll "G:\FKForm\FKForm\ForGits\FKFormControl\bin\Debug\*.dll"  /wildcards /allowDup
@echo "Merge Complete"
pause 