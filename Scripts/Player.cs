using Godot;
using System;

public class Player : KinematicBody2D {
	[Export]
	private PackedScene FIREBALL;

	private Vector2 movement = Vector2.Zero;

	private float move_Speed = 400f;

	private float gravity = 20f;

	private float jump_Force = -900f;

	private Vector2 up_Dir = Vector2.Up;

	public float max_Y = 800f;
	
	private Position2D crosshair;
	
	private double lim;
	
	public int  count = 10;
	
	public bool dead = false;
	
	public string BodyName;
	// for animation
	private AnimatedSprite animation;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

		animation = GetNode<AnimatedSprite>("Ena");
		crosshair = GetNode<Position2D>("Position2D");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta) {
		if(!dead){
			PlayerMovement();
		}
		if(Position.y > max_Y)
		{
			GetParent().GetNode<CameraFollow>("Main Cam").playerDied = true;
			GetParent<Gameplay>().PlayerDied();
			QueueFree();
		}

	}
	
	

	void PlayerMovement() {
		
		
		movement.y += gravity;

		if(Input.IsActionPressed("move_right")) {

			movement.x = move_Speed;

			AnimateMovement(true, true);
			if(Math.Sign(crosshair.Position.x) == -1){
				Vector2 pos = crosshair.Position;
				lim = pos.x*(-0.25);
				pos.x = (int)lim;
				crosshair.Position = pos;
			}
			

		} else if (Input.IsActionPressed("move_left")) {

			movement.x = -move_Speed;

			AnimateMovement(true, false);
			if(Math.Sign(crosshair.Position.x) == 1){
				Vector2 pos = crosshair.Position;
				pos.x += (float)lim*3;
				pos.x *= -1;
				crosshair.Position = pos;
			}
			
			

		}
			
		else{

			movement.x = 0f;

			AnimateMovement(false, true);

		}

		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("jump"))
			{
				movement.y = jump_Force;
				
			}
			
		}
		
		movement = MoveAndSlide(movement, up_Dir);
		
		if(Input.IsActionJustPressed("shoot")){
			ShootMagic();
		}
	}//PlayerMovement

	void AnimateMovement(bool moving, bool moveRight)
	{
		if (moving)
		{
			if(IsOnFloor()){
				if(count>0){
					animation.Play("WalkAttack");
				}
				else{
					animation.Play("Walk");
				}
			}
			else{
				animation.Play("Jump");
			}
			if(moveRight)
			{
				animation.FlipH = true;
			}
			else
			{
				animation.FlipH = false;
			}
		} else
		{
			if(IsOnFloor()){
				if(count>0){
					animation.Play("IdleAttack");
				}
				else{
					animation.Play("Idle");
				}
				
			}
			else
				animation.Play("Jump");
		}

	}
	void ShootMagic(){
		if(count > 0 ){
			Fireball magic = FIREBALL.Instance() as Fireball;
			if(Math.Sign(crosshair.Position.x) == 1){
				magic.SetFireballDirection(1);
			}
			else{
				magic.SetFireballDirection(-1);
			}
			GetParent<Gameplay>().AddChild(magic);
			magic.Position = GetNode<Position2D>("Position2D").GlobalPosition;
			count -= 1;
			GetParent().GetNode("Score Counter").GetNode("UI").GetNode<Label>("manacount").Text = count + " x";
		}
	}

	void _on_Body_entered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Enemy"))
		{
			dead = true;
			BodyName = body.Name;
			GetNode<Timer>("Timer").Start();
			GetParent().GetNode(BodyName).GetNode<AnimatedSprite>("Animation").Play("Attack");
			animation.Play("Dead");
			GetParent().GetNode<CameraFollow>("Main Cam").playerDied = true;
			GetParent<Gameplay>().PlayerDied();
			
		}
	}
	private void _on_Timer_timeout()
	{
		dead = false;
		QueueFree();
		GetParent().GetNode(BodyName).GetNode<AnimatedSprite>("Animation").Play("Walk");
	}

}//class




