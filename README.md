\# WRDD — Windows Rubbish Data Deletor



My first Windows Service — built in C# .NET Framework. It silently cleans temp files, browser caches, crash dumps, and app junk from your PC every week without any manual effort.



---



\## What It Cleans



\- Windows Temp, Minidump, and Crash Dumps

\- Windows Error Reporting (WER) queues and archives

\- Browser caches — Chrome, Edge, Brave

\- App caches — Discord, Spotify, Slack, Zoom



Minidump files older than 30 days are removed. Everything else older than 48 hours is removed.



---



\## Installation



Open a Developer Command Prompt as Administrator and run:



```

InstallUtil.exe "C:\\Path\\To\\winRubish\_TempDataServiceWeeklyDeletor.exe"

```



The service appears in `services.msc` with startup type set to Manual — use Task Scheduler to automate it.



---



\## Automating With Task Scheduler



1\. Open Task Scheduler → `Create Task`

2\. \*\*General\*\* — check `Run with highest privileges`

3\. \*\*Triggers\*\* — Weekly, pick your day and time

4\. \*\*Actions\*\* — Start a program: `sc` with argument `start WRDD`

5\. Click OK — done



---



\## Built With



\- C# .NET Framework · Windows Services · InstallUtil · Task Scheduler · ConfigurationManager



---



\## Author



Tony — self-taught C# developer building real-world Windows desktop and service projects.

