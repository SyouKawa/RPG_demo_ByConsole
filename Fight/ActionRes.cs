using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class ActionRes
	{
		public int resID;
	 	public LoadMode resMode;
		public int teamIndex;

		public ActionRes(int _resID,LoadMode mode)
		{
			resID = _resID;
			resMode = mode;
		}
	}
}
