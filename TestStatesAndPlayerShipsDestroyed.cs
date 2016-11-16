using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleShips
{
	public class TestStatesAndPlayerShipsDestroyed
	{
		/// <summary>
		/// Tests if gamestates can be switched successfully
		/// </summary>
		[Test()]
		public void TestSwitchStateAction ()
		{

			GameController.SwitchState (GameState.Deploying);
			Assert.AreEqual (GameController.CurrentState,GameState.Deploying);
		}

		/// <summary>
		/// Tests if game states can be successfull added.
		/// </summary>
		[Test()]
		public void TestStateAdd()
		{
			GameController.AddNewState (GameState.AlteringSettings);
			Assert.AreEqual (GameController.CurrentState,GameState.AlteringSettings);

			GameController.AddNewState (GameState.EndingGame);
			Assert.AreEqual (GameController.CurrentState,GameState.EndingGame);

			GameController.EndCurrentState ();
			Assert.AreEqual (GameController.CurrentState,GameState.AlteringSettings);
		}

		/// <summary>
		/// Tests if the initial state of the game when started is deploying.
		/// </summary>
		[Test()]
		public void TestStartGame()
		{
			BattleShipsGame Test = new BattleShipsGame ();
			GameController.StartGame ();
			Assert.AreEqual (GameController.CurrentState,GameState.Deploying);
		}

		/// <summary>
		/// Tests if all player ships are killed if all til;es are attacked
		/// </summary>
		[Test()]
		public void TestPlayerShipsKilled()
		{
			BattleShipsGame Test = new BattleShipsGame ();
			Player p = new Player (Test);
			Test.AddDeployedPlayer (p);
			Assert.AreEqual (Test.Player,p);

			Assert.AreEqual (p.PlayerGrid.ShipsKilled,0);
			Assert.IsTrue (p.PlayerGrid.AllDeployed);
			for (int i = 0; i < p.PlayerGrid.Width; i++)
			{
				for (int j = 0; j < p.PlayerGrid.Height; j++)
					p.PlayerGrid.HitTile (i, j);
			}
			//Test if all ships are killed
			Assert.AreEqual (p.PlayerGrid.ShipsKilled,5);
		}

	
	}
}

