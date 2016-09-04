using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TermSim
{
    public partial class FrmMain : Form
    {
        public FrmMain() 
        {
            InitializeComponent();

            ApplicationVariables.pwd = Environment.CurrentDirectory.ToLower(); //set present working directory

            ActiveControl = txtCmd; //focus cursor on console
        }

        public class ApplicationVariables 
        {
            //terminal variables
            public static string pwd, lastCommand = "";

            //application config variables
            public static int onTop, minToTray;
        }
        
        public static FrmMain Instance { get; private set; }

        protected override void OnShown(EventArgs e) 
        {
            base.OnShown(e);
            Instance = this;
        }

        protected override void OnClosed(EventArgs e) 
        {
            base.OnClosed(e);
            Instance = null;
        }

        private void FrmMain_Load(object sender, EventArgs e) 
        {
            TerminalWriteLine("System", "TermSim initialized...", false, Color.DodgerBlue);
        }

        //--- command input events ---
        private void txtCmd_KeyDown(object sender, KeyEventArgs e) //command input (enter key) 
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtCmd.Text.Trim() != "")
                    {
                        TerminalInputString(txtCmd.Text); //write command string to terminal
                        ApplicationVariables.lastCommand = txtCmd.Text; //store last inputted command

                        CommandLine.InputCommand(txtCmd.Text); //pass command string to processor method 

                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        //keep focus on most recent line
                        txtCmd.Clear();
                        txtCmd.Focus();
                    }
                    break;
                case Keys.Up:
                    txtCmd.Text = ApplicationVariables.lastCommand; //TODO: figure out this stupid text length bullshit
                    break;
            }
        }

        private void lblSubmit_Click(object sender, EventArgs e) //command input (button) 
        {
            if (txtCmd.Text.Trim() == "") return;

            TerminalInputString(txtCmd.Text); //write command string to terminal
            ApplicationVariables.lastCommand = txtCmd.Text; //store last inputted command

            CommandLine.InputCommand(txtCmd.Text); //pass command string to processor method

            //keep focus on most recent line
            txtCmd.Clear();
            txtCmd.Focus();
        }

        //--- examples of rich text formatted visual stuff ---
        public void TerminalInputString(string cmd) //generates the rich text formatted console input string for user commands 
        {
            var userArray = Regex.Split(Environment.UserDomainName, @"-");

            rtbConsole.AppendText("\n" + userArray[0]);
            rtbConsole.SelectionColor = Color.MediumTurquoise;
            rtbConsole.AppendText("@");
            rtbConsole.SelectionColor = Color.White;
            rtbConsole.AppendText(userArray[1] + ":~");
            rtbConsole.SelectionColor = Color.MediumTurquoise;
            rtbConsole.AppendText(TerminalPath());
            rtbConsole.SelectionColor = Color.White;
            rtbConsole.AppendText(" $ " + cmd);
        }

        public void TerminalWriteLine(string msgType, string message, bool newLine, Color msgColor) //write message to terminal screen 
        {
            var time = DateTime.Now.ToString("h:mm") + DateTime.Now.ToString("tt"); //format time

            if (newLine)
                rtbConsole.AppendText("\n");

            //timestamp
            rtbConsole.AppendText("[");
            rtbConsole.SelectionColor = Color.SlateGray;
            rtbConsole.AppendText(time);
            rtbConsole.SelectionColor = rtbConsole.ForeColor;
            rtbConsole.AppendText("] ");

            //message
            rtbConsole.SelectionColor = msgColor;
            rtbConsole.AppendText(msgType + " ");
            rtbConsole.SelectionColor = rtbConsole.ForeColor;
            rtbConsole.AppendText("» " + message);

            rtbConsole.Focus(); //focus the log
            rtbConsole.SelectionStart = rtbConsole.Text.Length; //keep focus on most recent line
        }

        public static string TerminalPath() 
        {
            try
            {
                var pathArray = Regex.Split(ApplicationVariables.pwd, @"\\");
                var drvRoot = Path.GetPathRoot(Environment.CurrentDirectory).Replace("\\", "").ToLower();

                var wrkPath = "\\" + pathArray[pathArray.Count() - 1];

                if (pathArray.Count() > 1)
                    wrkPath = "\\" + pathArray[pathArray.Count() - 2] + wrkPath;

                if (pathArray.Count() > 2)
                    wrkPath = "\\" + pathArray[pathArray.Count() - 3] + wrkPath;

                if (pathArray.Count() > 3)
                    wrkPath = "..." + wrkPath;

                return wrkPath.Replace("\\" + drvRoot, "drv[" + drvRoot.Replace(":", "") + "]");
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}