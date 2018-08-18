using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class LogPanel:Panel
	{
		//content
		public List<string> logs;
		private int curLoopLine;
		private int allOutput;
		//public string configPath;

		public LogPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type = PanelType.Descrp;
			logs = new List<string>();

			//default
			curLoopLine = 0;
			allOutput = 0;
		}

		public void UpdateLogs()
		{
			if (allOutput > size.heightRow - 2)
			{
				logs.Clear();
				allOutput = 0;
			}
		}

		public void AddLog(string log)
		{
			logs.Add(log);
		}

		public void DisplayLog()
		{
			ResetOutputStyle();
			//BeyondCheck before Print.
			UpdateLogs();
			allOutput = 0;//all Line count
			for (int i = 0, curAddLine = 0 ; i < logs.Count ; i++, curAddLine = 0)
			{
				string curLog = logs[i];
				int leftLen = curLog.Length;
				//Get a string-with \n
				//DONT use English in Chinese Log.
				for (int turn=1; leftLen > size.widthCol ;turn++)
				{
					curLog=curLog.Insert(turn * (size.widthCol - 2 - 1) , "\n");
					curAddLine += 1;
					leftLen -= size.widthCol;
				}
				//Use newString to display.
				string[] showLog = curLog.Split('\n');
				int j;
				for (j=0; j<=curAddLine ; j++)
				{
					if (i % 2 == 0)
					{
						Console.ForegroundColor = ConsoleColor.DarkBlue;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.DarkGreen;
					}
					Console.SetCursorPosition(startPos.x +2, startPos.y+1+curLoopLine);
					Console.Write(showLog[j]);
					allOutput++;
					curLoopLine++;
				}
			}
			curLoopLine = 0;
			ResetOutputStyle();
		}
	}
}
