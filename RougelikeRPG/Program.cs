using System;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System.Text;

using RougelikeRPG.GameObject;
using RougelikeRPG.Tool;

namespace RougelikeRPG
{
	class Program
	{
		static void Main(string[] args)
		{
			Item item = new Item("name","descrp",'◆',1,1);
			Load.AddSerializeJsonGameObject(item,Load.ObjectMode.Item);

			List<Item> test= Load.GetConfigFromFile<Item>();
			foreach (var temp in test)
			{
				Console.WriteLine();
			}
		}
	}
}
