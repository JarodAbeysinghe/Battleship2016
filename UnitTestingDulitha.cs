using System;
using NUnit.Framework;

namespace BattleShips
{
	[TestFixture()]
	public class UnitTestingDulitha
	{

		[Test()]
		public void TestTugDestoy ()
		{
			Ship ship = new Ship (ShipName.Tug); //creating the Tug ship
			ship.Hit (); //hit the ship 
			Assert.IsTrue(ship.IsDestroyed); //Check whther the ship is destroyed
		}


		[Test ()]
		public void TestSubmarineDestroy ()
		{
			Ship ship = new Ship (ShipName.Submarine); //creating the Submarine ship
			ship.Hit ();//Hit twice in order to destroy the whole ship
			ship.Hit ();
			Assert.IsTrue(ship.IsDestroyed); // check whether it is destroyed
		}

	}
}

