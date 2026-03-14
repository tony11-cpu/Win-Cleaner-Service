using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace winRubish_TempDataServiceWeeklyDeletor
{
    public partial class rubishDataDeletor : ServiceBase
    {
        public rubishDataDeletor()
        {
            InitializeComponent();
            this.ServiceName = "WRDD";
        }

        public void RunAsConsole()
        {
            OnStart(null);
        }

        private void _clearPaths()
        {
            foreach (var rawPath in new string[]
            {
                ConfigurationManager.AppSettings["Path_WindowsTemp"],
                ConfigurationManager.AppSettings["Path_WindowsMinidump"],
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_LocalTemp"]        ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_CrashDumps"]       ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_WER_LocalArchive"] ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_WER_LocalQueue"]   ?? ""),
                ConfigurationManager.AppSettings["Path_WER_SystemArchive"],
                ConfigurationManager.AppSettings["Path_WER_SystemQueue"],
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_ChromeCache"]      ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_ChromeGPUCache"]   ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_EdgeCache"]        ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_EdgeCodeCache"]    ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_EdgeGPUCache"]     ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_BraveCache"]       ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_DiscordAppData"]   ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_DiscordLocal"]     ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_Spotify"]          ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_Slack"]            ?? ""),
                Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["Path_Zoom"]             ?? ""),
            })
            {
                if (string.IsNullOrWhiteSpace(rawPath) || !Directory.Exists(rawPath))
                {
                    if (Environment.UserInteractive)
                        Console.WriteLine($"[SKIP] Path missing or empty: {rawPath}");

                    continue;
                }

                if (Environment.UserInteractive)
                    Console.WriteLine($"[CLEANING] {rawPath}");

                bool isMinidump = rawPath.IndexOf("minidump", StringComparison.OrdinalIgnoreCase) >= 0;
                DateTime cutoff = DateTime.Now.AddHours(isMinidump ? -720 : -48);

                IEnumerable<FileSystemInfo> entries;
                try
                {
                    entries = new DirectoryInfo(rawPath).EnumerateFileSystemInfos("*", SearchOption.AllDirectories);
                }
                catch
                {
                    if (Environment.UserInteractive)
                        Console.WriteLine($"[ERROR] Could not read: {rawPath}");

                    continue;
                }

                int deleted = 0;
                foreach (FileSystemInfo info in entries)
                {
                    if (info is DirectoryInfo) continue;
                    try
                    {
                        if (info.LastWriteTime < cutoff)
                        {
                            info.Delete();
                            deleted++;
                            if (Environment.UserInteractive)
                                Console.WriteLine($"  [DELETED] {info.FullName}");
                        }
                    }
                    catch
                    {
                        if (Environment.UserInteractive)
                            Console.WriteLine($"  [LOCKED]  {info.FullName}");

                        continue;
                    }
                }

                if (Environment.UserInteractive)
                    Console.WriteLine($"DONE {rawPath}, {deleted} file(s) deleted.\n");
            }

            if (Environment.UserInteractive)
                Console.WriteLine("WRDD Cleanup completed successfully.");
            else
                EventLog.WriteEntry("WRDD", "Cleanup Completed Successfully", EventLogEntryType.Information);
        }

        protected override void OnStart(string[] args)
        {
            RequestAdditionalTime(300000);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
            _clearPaths();
        }

        protected override void OnStop() { }
    }
}