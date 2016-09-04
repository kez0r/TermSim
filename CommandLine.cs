// Copyright (c) 2011 Jonathan Wood
// Edited & Re-factored by kez (ProcessArguments, InputCommand, Help methods by kez) [mailto:kez@jigmatic.com]      

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace TermSim
{
    public class CommandLineFlag
	{
        public string Flag { get; set; }
        public string Argument { get; set; }
	}
	
	class CommandLine
	{
        public List<string> Arguments { get; set; } //command-line argument list
        public List<CommandLineFlag> Flags { get; set; } //command-line flag list

		//parser state variables
	    readonly string _cmd;
		int _pos;

		//main constructor method
		public CommandLine(string commandLine)
		{
			//initialize parser variables
			Arguments = new List<string>();
            Flags = new List<CommandLineFlag>();
            _cmd = commandLine;
            _pos = 0;

			//loop until all characters processed
			while (!EndOfText)
			{
                while (Char.IsWhiteSpace(CurrentChar())) //skip whitespace
					MoveAhead();

				//check if 'flag'
				if (CurrentChar() == '/' || CurrentChar() == '-')
				{
					MoveAhead();
					var start = _pos;

					while (Char.IsLetterOrDigit(CurrentChar()))
						MoveAhead();

				    if (_pos <= start) continue;

				    var flag = new CommandLineFlag {Flag = _cmd.Substring(start, _pos - start)}; //construct parsed flag

				    if (CurrentChar() == ':') //check for flag argument delimiter
				    {
				        MoveAhead();
                        flag.Argument = ParseArgument(); //parse flag value (argument)
				    }

				    Flags.Add(flag);
				}
				else
				{
                    var arg = ParseArgument(); //parse command-line argument

					if (arg.Length > 0)
						Arguments.Add(arg);
				}
			}
		}

		///<summary> Returns true if the current position is end of string. </summary>
		protected bool EndOfText
		{
			get { return (_pos >= _cmd.Length); }
		}

		///<summary> Returns the character at the current position. </summary>
		protected char CurrentChar()
		{
			return (_pos < _cmd.Length) ? _cmd[_pos] : (char)0;
		}

		///<summary> Advances current position to next character. </summary>
		protected char MoveAhead()
		{
			var c = CurrentChar();
			_pos = Math.Min(_pos + 1, _cmd.Length);
			return c;
		}

		///<summary> Parses a command-line argument. Supports arguments enclosed in double or single quotation marks.
		/// If no argument can  be parsed, an empty string is returned. </summary>
		protected string ParseArgument()
		{
			string result;

			if (CurrentChar() == '"' || CurrentChar() == '\'')
			{
				//parse quoted argument
				var quote = MoveAhead();
				var start = _pos;

			    //ReSharper disable once LoopVariableIsNeverChangedInsideLoop (re-sharper doesn't realize MoveAhead() increments loop control variable)
				while (!EndOfText && CurrentChar() != quote)
					MoveAhead();

				result = _cmd.Substring(start, _pos - start);
				MoveAhead(); //skip closing quote
			}
			else
			{
                var start = _pos; //parse argument

				while (!EndOfText && !Char.IsWhiteSpace(CurrentChar()) && CurrentChar() != '/' && CurrentChar() != '-')
					MoveAhead();

				result = _cmd.Substring(start, _pos - start);
			}

			//return parsed argument
			return result;
		}

        ///<summary> Process command line string and route arguments to the proper method. </summary>
        internal static void ProcessArguments(CommandLine cmd)
        {
            var args = cmd.Arguments.ToArray(); //convert argument list to array.

            //format checks
            if (!args.Any()) //no commands present
                return;

            if (args.Count() > 1) //more than one command used. TODO: change the way commands are parsed to allow for non-flag arguments
            {
                FrmMain.Instance.TerminalWriteLine("Error", "Invalid Syntax: Multiple Commands present!", true, Color.Red);
                return;
            }

            //determine which method to use with the passed arguments
            switch (args[0])
            {
                case "clear":
                    Components.ClearConsole(); //clear console window (0 arguments)
                    break;
                case "exit":
                    FrmMain.Instance.Dispose(); //close program (0 arguments)
                    break;
                case "savelog":
                    Components.SaveConsoleLog(); //save the contents of the console to the logs folder (0 arguments)
                    break;
                //file system navigation
                 case "pwd":
                    Components.PresentWorkingDirectory(); //print present working directory (0 arguments)
                    break;
                 case "cd":
                    Components.ChangeDirectory(cmd.Flags); //change present working directory (2 arguments; -i:[x], -help)
                    break;
                 case "ls":
                    Components.ListFiles(cmd.Flags); //list files in present working directory (1 argument; -help)
                    break;
                //misc
                case "mintray":
                    Components.MinimizeToTray(); //toggle minimize to tray (0 arguments)
                    break;
                case "ontop":
                    Components.StayOnTop(); //toggle stay on top (0 arguments)
                    break;
                default:
                    FrmMain.Instance.TerminalWriteLine("Error", "Undefined Command!", true, Color.Red);
                    break;
            }
        }

        ///<summary> Split command strings (at 'pipe' characters) into a string array and process each string sequentially. </summary>
	    public static void InputCommand(string cmdString)
	    {
            var cmdArray = Regex.Split(cmdString, @"\s\|\s"); //split commands at pipes

            //process each command sequentially 
            foreach (var cmd in cmdArray)
                ProcessArguments(new CommandLine(cmd));
	    }
	}
}