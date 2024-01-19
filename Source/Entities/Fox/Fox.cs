using System;
using Godot;

public partial class Fox : PathFollow2D
{
	private readonly float PATH_END = 0.985f;
	
	private float speed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ProgressRatio = 0.0f;
		this.speed = 0.2f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.ProgressRatio += speed * (float)delta;
		GD.Print("TEST");
		if (this.ProgressRatio > PATH_END)
		{
			this.Free();
		}
	}
}
