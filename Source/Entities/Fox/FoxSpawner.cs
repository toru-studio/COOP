using Godot;
using System;
using System.Collections.Generic;

public partial class FoxSpawner : Path2D
{
	private readonly double REFRESH_RATE = 0.6;
	
	private Queue<PackedScene> foxes;
	private double elapsed;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.foxes = new Queue<PackedScene>();
		this.elapsed = 0.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		elapsed += delta;
		if (elapsed > REFRESH_RATE)
		{
			if (foxes.Count != 0)
			{
				this.AddChild(foxes.Dequeue().Instantiate());
			}

			elapsed = 0.0;
		}
	}

	public void AddFox(PackedScene fox)
	{
		foxes.Enqueue(fox);
	}
}
