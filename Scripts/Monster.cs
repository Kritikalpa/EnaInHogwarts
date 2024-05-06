using Godot;
using System;

public class Monster : KinematicBody2D
{
	public float moveSpeed = 500f;

	public float gravity = 20f;

	private Vector2 movement;

	public bool moveLeft;

	public float min_X = -5900f, max_X = 6650f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimatedSprite>("Animation").Play("Walk");
		if (moveLeft)
		{
			moveSpeed *= -1f;
			GetNode<AnimatedSprite>("Animation").FlipH = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		movement.y += gravity;
		movement.x = moveSpeed;
		movement = MoveAndSlide(movement);
		if(Position.x > max_X || Position.x < min_X)
		{
			QueueFree();
		}
	
	}
	private void _on_Timer_timeout()
	{
		QueueFree();
	}
}





