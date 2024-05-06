using Godot;
using System;

public class StoryLine : Node2D
{
	public override void _Ready()
	{
		GetNode<SaveScoret>("/root/SaveScoret").SaveValue("DeadCount", 2);
	}
	public void _on_TextureButton_pressed(){
		GetTree().ChangeScene("res://Scenes/Gameplay.tscn");
	}

}
