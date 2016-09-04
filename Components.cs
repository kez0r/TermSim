using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TermSim
{
    class Components
    {
        //core
        public static void ClearConsole()
        {
            if (FrmMain.Instance == null) return;

            var console = FrmMain.Instance.Controls.Find("rtbConsole", true);
            console[0].Text = "";

            FrmMain.Instance.TerminalWriteLine("System", "Console cleared.", false, Color.DodgerBlue);
        }
        
        public static void SaveConsoleLog()
        {
            //create time stamped log file (txt)
            var stream = new StreamWriter(Environment.CurrentDirectory + "\\logs\\" + DateTime.Today.ToString("M-d-yy") + "_" + DateTime.Now.ToString("h-mm") + DateTime.Now.ToString("tt") + ".log");

            var console = FrmMain.Instance.rtbConsole;
            var logText = console.Lines;

            stream.WriteLine("----- Console Log: Begin. -----");
            stream.WriteLine("");

            foreach (var line in logText)
                stream.WriteLine(line);

            stream.WriteLine("");
            stream.WriteLine("----- Console Log: End. [Saved @ " + DateTime.Now.ToString("h:mm") + DateTime.Now.ToString("tt") + " on " + DateTime.Today.ToString("M/d/yy") + "] -----");

            stream.Close();

            FrmMain.Instance.TerminalWriteLine("System", "Console log saved!", true, Color.DodgerBlue);
        }
        
        //file system navigation
        public static void ChangeDirectory(List<CommandLineFlag> flags)
        {
            if (flags.Count != 0)
            foreach (CommandLineFlag f in flags)
            {
                //parse flags
                switch (f.Flag)
                {
                    case "i":
                        //check for various syntax input possibilities
                        if (f.Argument == "..")
                        {
                            //check if pwd is root drive
                            var root = Path.GetPathRoot(FrmMain.ApplicationVariables.pwd);
                            if (root != null && FrmMain.ApplicationVariables.pwd == root.TrimEnd('\\').ToLower())
                            {
                                FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
                                break;
                            }

                            //set pwd to parent of current directory
                            FrmMain.ApplicationVariables.pwd = Directory.GetParent(FrmMain.ApplicationVariables.pwd).ToString().TrimEnd('\\').ToLower();

                            FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
                            break;
                        }
                    
                        //input: local drive + \
                        if (f.Argument == Path.GetPathRoot(Environment.CurrentDirectory).ToLower())
                        {
                            FrmMain.ApplicationVariables.pwd = Path.GetPathRoot(Environment.CurrentDirectory).Replace("\\", "").ToLower();
                            FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
                            break;
                        }

                        //input: pwd + argument
                        if (Directory.Exists(FrmMain.ApplicationVariables.pwd + "\\" + f.Argument))
                        {
                            FrmMain.ApplicationVariables.pwd = FrmMain.ApplicationVariables.pwd + "\\" + f.Argument.TrimStart('\\').ToLower();
                            FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
                            break;
                        }

                        //input: full path
                        if (Directory.Exists(f.Argument))
                        {
                            FrmMain.ApplicationVariables.pwd = f.Argument.Replace("\\\\", "\\").ToLower();
                            FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
                            break;
                        }

                        //path does not exist
                        if (!Directory.Exists(f.Argument))
                            FrmMain.Instance.TerminalWriteLine("Error", "Path not found!", true, Color.Red);

                        break;
                    case "help":
                        Help("cd", 1, "[-i]", "[cd -i:c:\\windows, cd -i:\"c:\\with spaces\"]");
                        break;
                    default:
                        FrmMain.Instance.TerminalWriteLine("Error", "Invalid Syntax!", true, Color.Red);
                        break;
                }
            }
            else
                FrmMain.Instance.TerminalWriteLine("Error", "Invalid Syntax!", true, Color.Red);
        }

        public static void ListFiles(List<CommandLineFlag> flags)
        {
            if (flags.Count != 0)
            {
                foreach (CommandLineFlag f in flags)
                    //parse flags
                    switch (f.Flag)
                    {
                        case "help":
                            Help("ls", 1, "[-help]", "[ls, ls -help]");
                            break;
                    }
            }
            else
            {
                var pwd = FrmMain.ApplicationVariables.pwd;
                var root = Path.GetPathRoot(pwd);

                //check if pwd is root drive
                if (root != null && pwd == root.TrimEnd('\\').ToLower())
                    pwd = pwd + "\\";

                //create array of files in present working directory & sort files since GetFiles doesn't guarantee sorting
                var getFiles = Directory.GetFiles(pwd);
                Array.Sort(getFiles);

                FrmMain.Instance.TerminalWriteLine("System", "listing... [" + pwd + "]", true, Color.DodgerBlue);
                FrmMain.Instance.TerminalWriteLine("System", "..", true, Color.DodgerBlue);

                //print folders
                foreach (var folder in Directory.GetDirectories(pwd))
                    FrmMain.Instance.TerminalWriteLine("System", ".." + folder.Replace(pwd, "").TrimStart('\\'), true, Color.DodgerBlue);

                //print files
                foreach (var file in getFiles)
                    FrmMain.Instance.TerminalWriteLine("System", file.Replace(pwd, "").TrimStart('\\'), true, Color.DodgerBlue);
            }
        }

        public static void PresentWorkingDirectory()
        {
            FrmMain.Instance.TerminalWriteLine("System", FrmMain.ApplicationVariables.pwd, true, Color.DodgerBlue);
        }

        //misc
        public static void Help(string command, int numArgs, string argList, string usageEx)
        {
            if (FrmMain.Instance == null) return;

            FrmMain.Instance.TerminalWriteLine("Help", "Command: [" + command + "] ‒ Arguments: [" + numArgs + "]", true, Color.DarkOrchid);
            FrmMain.Instance.TerminalWriteLine("Help", "Args: " + argList, true, Color.DarkOrchid);
            FrmMain.Instance.TerminalWriteLine("Help", "Usage: " + usageEx, true, Color.DarkOrchid);
        }
        
        public static void MinimizeToTray()
        {
            FrmMain.ApplicationVariables.minToTray = FrmMain.ApplicationVariables.minToTray == 1 ? 0 : 1; //toggle variable
            FrmMain.Instance.TopMost = FrmMain.ApplicationVariables.onTop != 0; //toggle topmost state

            FrmMain.Instance.TerminalWriteLine("Settings", FrmMain.ApplicationVariables.minToTray == 1 ? "Minimize to tray ‒ [enabled]" : "Minimize to tray ‒ [disabled]", true, Color.Coral);
        }

        public static void StayOnTop()
        {
            FrmMain.ApplicationVariables.onTop = FrmMain.ApplicationVariables.onTop == 1 ? 0 : 1; //toggle variable
            FrmMain.Instance.TopMost = FrmMain.ApplicationVariables.onTop != 0; //toggle topmost state

            FrmMain.Instance.TerminalWriteLine("Settings", FrmMain.ApplicationVariables.onTop == 1 ? "Stay on top ‒ [enabled]" : "Stay on top ‒ [disabled]", true, Color.Coral);
        }
    }
}