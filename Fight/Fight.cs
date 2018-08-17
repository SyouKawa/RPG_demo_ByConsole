using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class Fight
	{
		//Inter-Data
		public List<Player> playerTeam=new List<Player>();
		public Player hostile = new Player();
		//public ActionRes actionRes;
		public List<string> Log;
		//Control
		bool isYourTurn = true;
		//Inter-FightPanel
		public FightPanel fightPanel;//init in FightPanel

		public Fight() {}
		public Fight(Player _hostile ,List<Player> _playerTeam)
		{
			hostile = _hostile;
			playerTeam = _playerTeam;

			//default
			Log = new List<string>();
			//actionRes = new ActionRes(-1,LoadMode.Null);
			foreach (Panel panel in DisplayController.Instance.panels)
			{
				if (panel.type == PanelType.Fight)
				{
					//fightPanel = new FightPanel(panel);
				}
			}
		}

		public static int actionInfo = -1, attackInfo = -1, chrInfo = -1;

		public void RoundBattle()
		{
			//SpawnPriority();
			while (!hostile.Get_isDead())
			{
				if (isYourTurn)
				{
					//Get 3-Args to release Skill or Item.
					Console.BackgroundColor = ConsoleColor.Black;
					fightPanel.logPanel.DrawFrame();
					fightPanel.logPanel.DisplayLog();
					while (chrInfo == -1)
					{
						chrInfo = DisplayController.Instance.ControlCurPanel();
					}

					//Settlement
					int skillID = playerTeam[chrInfo].ownSkillID[attackInfo];
					if (actionInfo == 0)
					{
						Skill SettleTempskill = playerTeam[chrInfo].Attack(skillID);
						GlobalSkillsManager.Instance.AddSpecialEffectPositive(SettleTempskill.skillID , playerTeam , hostile);
						fightPanel.logPanel.AddLog(SettleTempskill.descrp+'('+ SettleTempskill.info+')');
						fightPanel.logPanel.DisplayLog();
						hostile.BeHit(SettleTempskill);
						GlobalSkillsManager.Instance.AddSpecialEffectPassive(SettleTempskill.skillID , playerTeam , hostile);
						fightPanel.logPanel.AddLog("this is a log.");
						fightPanel.logPanel.DisplayLog();
					}
					if (actionInfo == 1)
					{
						//playerTeam[chrInfo].UseItem(skillID);
						//hostile.BeHit(skillID);
						
						//TODO Display the descrp of Item
					}
					//TestCode
					//isYourTurn = false;
					//reset
					actionInfo = -1;
					attackInfo = -1;
					chrInfo = -1;
				}
				else
				{
					//TODO-Greedy-Alth Attack-mode
				}
			}
		}

		#region Serve RoundBattle's Functions

		private void SpawnPriority()
		{
			if (hostile.HP > playerTeam[0].HP) isYourTurn = false;
			else isYourTurn = true;
		}

		#endregion
	}
}
