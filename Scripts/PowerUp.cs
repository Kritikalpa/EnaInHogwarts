using Godot;
using System;

public class PowerUp : Area2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimatedSprite>("Potion").Play("Default");
	}

	private void _on_Body_entered(PhysicsBody2D body)
	{
		if(body.Name == "Player"){
			GetParent().GetNode<Player>("Player").count += 10;
			GetParent().GetNode("Score Counter").GetNode("UI").GetNode<Label>("manacount").Text  = GetParent().GetNode<Player>("Player").count + " x";
			QueueFree();
		}
	}
}



