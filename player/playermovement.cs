using Godot;
using System;

public partial class playermovement : CharacterBody2D
{
	public const float Speed = 300.0f;

	private AnimationPlayer ap;

	private Sprite2D player_sprite;

	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		player_sprite = GetNode<Sprite2D>("playerSprite");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("moveleft", "moveright", "moveup", "movedown");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
			ap.Play("p_anim_walk");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			ap.Play("p_anim_idle");
		}

		Velocity = velocity;

		//roll with q&e
		if (Input.IsActionPressed("rotateleft")) 
			{
				player_sprite.Rotation += 1;
			}
		
		if (Input.IsActionPressed("rotateright")) 
			{
				player_sprite.Rotation -= 1;
			}
		if (Input.IsActionJustReleased("rotateleft") || Input.IsActionJustReleased("rotateright"))
			{
				player_sprite.Rotation = 0;
			}

		MoveAndSlide();
	}
}
