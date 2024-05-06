using Godot;
using System;

public class Fireball : Area2D
{
	private float SPEED = 1500f;
	private Vector2 velocity = Vector2.Zero;
	private int direction = -1;
	private bool flag = false;
	
	private AnimatedSprite animation;

	// Called when the node enters the scene tree for the first time.
	
	
	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite>("AnimatedSprite");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		velocity.x = SPEED*delta*direction;
		Translate(velocity);
		animation.Play("shoot");
	}

	void _on_VisibilityNotifier2D_screen_exited(){
		QueueFree();
	}
	
	public void SetFireballDirection(int dir){
		direction = dir;
		
	}
	
	void _on_Body_entered(PhysicsBody2D body){
		if(body.IsInGroup("Enemy")){
			GetParent().GetNode(body.Name).GetNode<AnimatedSprite>("Animation").Play("Dead");
			QueueFree();
			GetParent().GetNode<Monster>(body.Name).moveSpeed = 0f;
			GetParent().GetNode(body.Name).GetNode<Timer>("Timer").Start();
			GetParent().GetNode(body.Name).GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
			GetParent().GetNode<Monster>(body.Name).gravity = 0f;
		}
	}
}




