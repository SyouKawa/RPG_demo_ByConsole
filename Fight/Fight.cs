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
		public List<Character> playerTeam=new List<Character>();
		public Character hostile = new Character();
		//public ActionRes actionRes;
		public List<string> Log;
		//Control
		bool isYourTurn = true;
		//Inter-FightPanel
		public FightPanel fightPanel;//init in FightPanel

		public Fight() {}
		public Fight(Character _hostile,List<Character> _playerTeam)
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
					while (chrInfo == -1)
					{
						chrInfo = DisplayController.Instance.ControlCurPanel();
					}
					int skillID = playerTeam[chrInfo].ownSkillID[attackInfo];

					if (actionInfo == 0)
					{
						playerTeam[chrInfo].Attack(skillID);
						hostile.BeHit(skillID);
					}
					if (actionInfo == 1)
					{
						playerTeam[chrInfo].UseItem(skillID);
						hostile.BeHit(skillID);
					}
					if (actionInfo == 2)
					{
						//TODO- Escape.
					}
					isYourTurn = false;
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
