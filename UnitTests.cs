using System;
using SwinGameSDK;
using NUnit.Framework;

namespace BattleShips
{
	[TestFixture]
	public class UnitTests
	{
		///Testing whether the mute button (M) works
		[Test()]

		public void TestingtheMuteButton ()
		{
			string x;

			SwinGame.PlayMusic("Background");
			SwinGame.KeyTyped (KeyCode.vk_m);

			if (SwinGame.MusicPlaying ())
			{
				x = "playing";
			} 

			else 
			{
				x = "Mute";
			}

			Assert.AreEqual (x, "Mute");
		}

		//Testing whether ships get destroyed when completely hit
		[Test ()]
		public void TestBattleshipDestroyed ()
		{
			Ship b = new Ship (ShipName.Battleship);
			b.Hit ();
			b.Hit ();
			b.Hit ();
			b.Hit ();
			Assert.IsTrue(b.IsDestroyed);
		}

		[Test ()]
		public void TestAircraftDestroyedT ()
		{
			Ship a = new Ship (ShipName.AircraftCarrier);
			a.Hit ();
			a.Hit ();
			a.Hit ();
			a.Hit ();
			a.Hit ();
			Assert.IsTrue(a.IsDestroyed);
		}

		//Testing whether mouse point is at the right Cell 
		[Test ()]
		public void TestMousePointAtCellRow1Col1 ()
		{
			Point2D mouse = default(Point2D);
			mouse = SwinGame.PointAt (400, 180);
			int row = 0;
			int col = 0;
			row = Convert.ToInt32(Math.Floor((mouse.Y  - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
			col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));
			Assert.AreEqual (row, 1);
			Assert.AreEqual (col, 1);
		}
	}
}

