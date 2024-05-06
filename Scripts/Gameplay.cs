using Godot;
using System;

public class Gameplay : Node
{
	[Export]
	private PackedScene greenZombie, redZombie, ghost;
	
	private int ElapsedSecond = 0;
	
	public int CurrentHighScore; 
	
	public int DiedCount = 2;

	
	private Vector2 spawn_Left = new Vector2(-5000f, 535f);
	private Vector2 spawn_Right = new Vector2(6390f, 535f);

	private Timer restart;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode("Score Counter").GetNode("UI").GetNode<AnimatedSprite>("heart").Play("default");
		GetNode("Score Counter").GetNode("UI").GetNode<AnimatedSprite>("mana").Play("default");
		GetNode<SaveScoret>("/root/SaveScoret").LoadValue("HighScore");
		GetNode<SaveScoret>("/root/SaveScoret").LoadValue("DeadCount");
		CurrentHighScore = GetNode<SaveScoret>("/root/SaveScoret").HighScore;
		DiedCount = GetNode<SaveScoret>("/root/SaveScoret").DeadCount;
		GetNode("Score Counter").GetNode("UI").GetNode<Label>("heartcount").Text = DiedCount + " x";
		restart = GetNode<Timer>("Restart");
		GetNode<Timer>("ElapsedTime").Start();
		int Second = CurrentHighScore % 60;
		int Minute = CurrentHighScore / 60;
		var StrElapsed =  String.Format("{0:D2}:{1:D2}", Minute, Second);
		GetNode("HighScoreView").GetNode("CanvasLayer").GetNode("Control").GetNode<Label>("HighScore").Text = StrElapsed;
	}

	void _on_Monster_Spawn()
	{
		int randEnemy = new Random().Next(0, 3);
		Monster newMonster = null;
		switch (randEnemy)
		{
			case 0: newMonster = greenZombie.Instance() as Monster;
				break;

			case 1: newMonster = redZombie.Instance() as Monster;
				break;

			case 2: newMonster = ghost.Instance() as Monster;
				break;
		}

		Vector2 temp;

		if(new Random().Next(0, 2) > 0)
		{
			temp = spawn_Right;
			newMonster.moveLeft = true;
		}
		else
		{
			temp = spawn_Left;
		}
		newMonster.SetPosition(temp);
		AddChild(newMonster);
	}

	public void PlayerDied()
	{
		restart.Start();
		if(ElapsedSecond>CurrentHighScore){
			GetNode<SaveScoret>("/root/SaveScoret").SaveValue("HighScore", ElapsedSecond);
		}
		DiedCount -= 1;
		GetNode<SaveScoret>("/root/SaveScoret").SaveValue("DeadCount", DiedCount);
		GetNode("Score Counter").GetNode("UI").GetNode<Label>("heartcount").Text = DiedCount + " x";
	}

	void _on_Player_Died()
	{
		if(DiedCount>0){
			GetTree().ReloadCurrentScene();
		}
		else{
			GetNode("CanvasLayer2").GetNode<AnimationPlayer>("AnimationPlayer2").Play("gameover");
			GetNode<Timer>("endondead").Start();
		}
	}
	private void _on_ElapsedTime_timeout()
	{
		ElapsedSecond += 1;
		int Second = ElapsedSecond % 60;
		int Minute = ElapsedSecond / 60;
		var StrElapsed =  String.Format("{0:D2}:{1:D2}", Minute, Second);
		GetNode("Score Counter").GetNode("UI").GetNode("Control").GetNode<Label>("Points").Text = StrElapsed;
	}
	private void _on_endondead_timeout()
	{
		GetTree().ChangeScene("res://Scenes/TitleNew.tscn");  
	}
}












