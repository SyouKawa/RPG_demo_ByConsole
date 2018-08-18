using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

		public int RoundBattle()
		{
			//SpawnPriority();
			Random random = new Random();//for monster releasing skill.
			while (!hostile.Get_isDead()&&!playerTeam[0].Get_isDead())
			{
				if (isYourTurn)
				{
					//Get 3-Args to release Skill or Item.
					//reset
					actionInfo = -1;
					attackInfo = -1;
					chrInfo = -1;
					RefreshLogPanel();
					while (chrInfo == -1)
					{
						chrInfo = DisplayController.Instance.ControlCurPanel();
						if (DisplayController.Instance.panels.Peek().type != PanelType.Fight)
						{
							return -1;//打断
						}
					}
					//Settlement

					int skillID = playerTeam[chrInfo].ownSkillID[attackInfo];
					if (actionInfo == 0)
					{
						Skill SettleTempskill = playerTeam[chrInfo].Attack(skillID);
						if (SettleTempskill.skill_name[0]=='0')//Didnt pass RCC
						{
							string RCCres = (SettleTempskill.skill_name.Split('|'))[1];
							DisplayController.Instance.OpenPanel(new DescripPanel("I n f o " , new Pos(30 , 12) , new Size(10 , 6) , "该技能无法使用!\n"+ RCCres));
							continue;
						}
						GlobalSkillsManager.Instance.AddSpecialEffectPositive(SettleTempskill.skillID , playerTeam , hostile, fightPanel.logPanel);
						fightPanel.Draw();
						fightPanel.logPanel.AddLog(playerTeam[chrInfo].name + "发动了" + SettleTempskill.skill_name+"\n"+SettleTempskill.descrp+"(附加效果："+ SettleTempskill.info+')');
						fightPanel.logPanel.DisplayLog();
						RefreshLogPanel();

						hostile.BeHit(SettleTempskill);
						GlobalSkillsManager.Instance.AddSpecialEffectPassive(SettleTempskill.skillID , playerTeam , hostile, fightPanel.logPanel);
						fightPanel.Draw();
						fightPanel.logPanel.AddLog(hostile.name + "受到了" +SettleTempskill.damage + "点伤害");
						fightPanel.logPanel.DisplayLog();
						RefreshLogPanel();
					}
					if (actionInfo == 1)
					{
						//playerTeam[chrInfo].UseItem(skillID);
						//hostile.BeHit(skillID);
						
						//TODO Display the descrp of Item
					}
					isYourTurn = false;
					//reset
					actionInfo = -1;
					attackInfo = -1;
					chrInfo = -1;
				}
				else
				{
					//TODO-Greedy-Alth Attack-mode
					int ID = random.Next(1001 , 1022);
					hostile.GetSettleSkillPrototype(ID);
					Skill SettleTempskill = hostile.settleTempSkill;
					GlobalSkillsManager.Instance.AddSpecialEffectPositive(SettleTempskill.skillID , playerTeam , hostile, fightPanel.logPanel);
					//fightPanel.logPanel.DisplayLog();
					//RefreshLogPanel();
					DisplayController.Instance.OpenPanel(new DescripPanel("I n f o " , new Pos(10 , 10) , new Size(10 , 4) , "==对方的回合=="));
					Thread.Sleep(2500);
					DisplayController.Instance.CloseCurPanel();
					//Skill SettleTempskill = new Skill();

					foreach (Player player in playerTeam)
					{
						player.BeHit(SettleTempskill);
					}
					GlobalSkillsManager.Instance.AddSpecialEffectPassive(SettleTempskill.skillID , playerTeam , hostile, fightPanel.logPanel);
					fightPanel.Draw();
					fightPanel.logPanel.AddLog(hostile.name + "发动了" + SettleTempskill.skill_name + "对小队每人造成了" + SettleTempskill.damage + "点伤害");
					fightPanel.logPanel.DisplayLog();
					RefreshLogPanel();
					isYourTurn = true;
				}
			}
			//TODO destroy monster in map
			return 0;
		}

		#region Serve RoundBattle's Functions

		private void SpawnPriority()
		{
			if (hostile.HP > playerTeam[0].HP) isYourTurn = false;
			else isYourTurn = true;
		}

		private void RefreshLogPanel()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			fightPanel.logPanel.DrawFrame();
			fightPanel.logPanel.DisplayLog();
		}

		#endregion
	}
}
