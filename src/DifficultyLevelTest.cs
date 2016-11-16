using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleShips
{
	[TestFixture()]
	public class DifficultyLevelTest
	{
			[Test()]
			public void TestDifficulty()
			{
				BattleShipsGame Test = new BattleShipsGame ();

				GameController.SetDifficulty (AIOption.Medium);
				Assert.AreEqual (GameController.AICurrent, AIOption.Medium);

				GameController.SetDifficulty (AIOption.Hard);
				Assert.AreEqual (GameController.AICurrent, AIOption.Hard);

				GameController.SetDifficulty (AIOption.Easy);
				Assert.AreEqual (GameController.AICurrent, AIOption.Easy);
			}

	}
}

