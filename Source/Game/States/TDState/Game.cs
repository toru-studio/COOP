using Godot;
using System;

public partial class Game : Node2D
{
	private FoxSpawner spawner;

	private double test_elapsed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		test_elapsed = 0.0;
		try
		{
			this.spawner = (FoxSpawner)this.FindChild("Pathing").FindChild("Path2D");
		}
		catch (Exception e)
		{
			GD.Print("Fox Spawner Not Found");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		test_elapsed += delta;
		if (test_elapsed > .1)
		{
			this.spawner.AddFox(GD.Load<PackedScene>("res://Source/Entities/Fox/fox.tscn"));
			test_elapsed = 0.0;
		}
	}
}
