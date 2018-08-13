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
		public List<Character> playerTeam;
		public Character hostile;
		public ActionRes actionRes;
		public List<string> Log;
		//Control
		bool isYourTurn = true;
		//Inter-FightPanel
		FightPanel fightPanel;

		public Fight(Character _hostile,List<Character> _playerTeam)
		{
			hostile = _hostile;
			playerTeam = _playerTeam;

			//default
			Log = new List<string>();
			actionRes = new ActionRes(-1,LoadMode.Null);
			foreach (Panel panel in DisplayController.Instance.panels)
			{
				if (panel.type == PanelType.Fight)
				{
					//fightPanel = new FightPanel(panel);
				}
			}
		}

		public void RoundBattle()
		{
			SpawnPriority();
			while (!hostile.Get_isDead())
			{
				if (isYourTurn)
				{
					//GetInput from FightPanel
					ActionRes actionInfo = null;
					while (actionInfo==null)
					{
						actionInfo= DisplayController.Instance.ControlCurPanel();
					}
					playerTeam[actionInfo.teamIndex].Attack(actionInfo.resID);
					hostile.BeHit(actionInfo.resID);
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
