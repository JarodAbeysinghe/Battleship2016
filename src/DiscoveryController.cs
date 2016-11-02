using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		if (SwinGame.KeyTyped(KeyCode.vk_a)) {
			//SwinGame.StopMusic();
			SwinGame.PlayMusic(GameResources.GameMusic("Background1"));
		}
		if (SwinGame.KeyTyped(KeyCode.vk_b)) {
			//SwinGame.StopMusic();
			SwinGame.PlayMusic(GameResources.GameMusic("Background"));
		}


		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
		if (UtilityFunctions.IsMouseInRectangle((SwinGame.ScreenWidth()/2)- 50, 94, 75, 15) && SwinGame.MouseClicked(MouseButton.LeftButton)) {
			GameController.AddNewState(GameState.ViewingMainMenu);
			SwinGame.ResetTimer (GameTimer);
		}
		if (UtilityFunctions.IsMouseInRectangle((SwinGame.ScreenWidth()/2)+ 50, 94, 80, 15) && SwinGame.MouseClicked(MouseButton.LeftButton)) {
			GameController.AddNewState(GameState.ViewingHighScores);
		}
		if (UtilityFunctions.IsMouseInRectangle((SwinGame.ScreenWidth()/2)+ 317, 94, 75, 15) && SwinGame.MouseClicked(MouseButton.LeftButton)) {
			GameController.AddNewState (GameState.Quitting);
			SwinGame.ResetTimer (GameTimer);
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
			if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
				GameController.Attack(row, col);
			}
		}
	}
	public static Timer GameTimer = SwinGame.CreateTimer ();
	public static uint _time;
	public static string s;
	public static int x = 0;
	public static uint min = 0;
	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;

		const int LEFT_TOP = 306;
		if (x == 0)
		{	
			SwinGame.ResetTimer (GameTimer);
			SwinGame.StartTimer (GameTimer);
			x++;
		}
		if ((SwinGame.KeyDown(KeyCode.vk_LSHIFT) | SwinGame.KeyDown(KeyCode.vk_RSHIFT)) & SwinGame.KeyDown(KeyCode.vk_c)) {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		} else {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage();

		SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
		SwinGame.DrawTextLines("Main Menu", Color.Blue, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2) - 50, 94, 75, 15);
		SwinGame.DrawTextLines("High Scores", Color.Blue, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)+ 50, 94, 80, 15);
		SwinGame.DrawTextLines("Quit", Color.Gray, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)+ 317, 94, 75, 15);
		SwinGame.DrawTextLines("Quit", Color.Gray, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)+ 317, 94, 75, 15);
		SwinGame.DrawTextLines("Press A to Change to a song/Press B to Change back", Color.Blue, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)-50, 570, 400, 15);

		SwinGame.DrawTextLines("Ships Destroyed:", Color.Red, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)-398, 300, SCORES_LEFT, LEFT_TOP-6);

		SwinGame.DrawText(GameController.ComputerPlayer.PlayerGrid.ShipsKilled.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, LEFT_TOP-6);
		s = _time.ToString ();
		_time = SwinGame.TimerTicks (GameTimer)/1000;

		SwinGame.DrawTextLines("Time: " +s, Color.Blue, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, (SwinGame.ScreenWidth()/2)-450, 94, 400, 15);
		if (_time == 300)
		{
			SwinGame.DrawTextLines("Click the mouse to Exit    ", Color.Yellow, Color.Transparent, GameResources.GameFont("Menu"), FontAlignment.AlignRight, 0, 550, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			SwinGame.DrawTextLines("TIME UP!!!", Color.Yellow, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 250, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			SwinGame.PauseTimer (GameTimer);
			if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
				SwinGame.ResetTimer (GameTimer);
				GameController.AddNewState(GameState.Quitting);
			}
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
