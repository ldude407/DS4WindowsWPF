﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace DS4WinWPF
{
    public static class StartupMethods
    {
        public static string lnkpath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\DS4Windows.lnk";

        public static bool HasStartProgEntry()
        {
            bool exists = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\DS4Windows.lnk");
            return exists;
        }

        public static bool HasTaskEntry()
        {
            TaskService ts = new TaskService();
            Task tasker = ts.FindTask("RunDS4Windows");
            return tasker != null;
        }

        public static bool RunAtStartup()
        {
            return HasStartProgEntry() || HasTaskEntry();
        }

        public static void WriteStartProgEntry()
        {
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); // Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            try
            {
                var lnk = shell.CreateShortcut(lnkpath);
                try
                {
                    string app = Assembly.GetExecutingAssembly().Location;
                    lnk.TargetPath = Assembly.GetExecutingAssembly().Location;
                    lnk.Arguments = "-m";

                    //lnk.TargetPath = Assembly.GetExecutingAssembly().Location;
                    //lnk.Arguments = "-m";
                    lnk.IconLocation = app.Replace('\\', '/');
                    lnk.Save();
                }
                finally
                {
                    Marshal.FinalReleaseComObject(lnk);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);
            }
        }

        public static void DeleteStartProgEntry()
        {
            if (File.Exists(lnkpath) && !new FileInfo(lnkpath).IsReadOnly)
            {
                File.Delete(lnkpath);
            }
        }

        public static bool CanWriteStartEntry()
        {
            bool result = false;
            if (!new FileInfo(lnkpath).IsReadOnly)
            {
                result = true;
            }

            return result;
        }

        public static void WriteTaskEntry()
        {
            DeleteTaskEntry();
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.Triggers.Add(new LogonTrigger());
            string dir = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).DirectoryName;
            td.Actions.Add(new ExecAction($@"{dir}\task.bat",
                "",
                dir));

            td.Principal.RunLevel = TaskRunLevel.Highest;
            ts.RootFolder.RegisterTaskDefinition("RunDS4Windows", td);
        }

        public static void DeleteTaskEntry()
        {
            TaskService ts = new TaskService();
            Task tasker = ts.FindTask("RunDS4Windows");
            if (tasker != null)
            {
                ts.RootFolder.DeleteTask("RunDS4Windows");
            }
        }

        public static bool CheckStartupExeLocation()
        {
            string lnkprogpath = ResolveShortcut(lnkpath);
            return lnkprogpath != Process.GetCurrentProcess().MainModule.FileName;
        }

        public static void LaunchOldTask()
        {
            TaskService ts = new TaskService();
            Task tasker = ts.FindTask("RunDS4Windows");
            if (tasker != null)
            {
                tasker.Run("");
            }
        }

        private static string ResolveShortcut(string filePath)
        {
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); // Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            string result;

            try
            {
                var shortcut = shell.CreateShortcut(filePath);
                result = shortcut.TargetPath;
                Marshal.FinalReleaseComObject(shortcut);
            }
            catch (COMException)
            {
                // A COMException is thrown if the file is not a valid shortcut (.lnk) file 
                result = null;
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);
            }

            return result;
        }
    }
}
