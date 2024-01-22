using Godot;
using System;
using System.Collections.Generic;

public partial class FoxSpawner : Path2D
{
	private readonly double REFRESH_RATE = 0.6;

	private Queue<PackedScene> foxes;
	private double elapsed;
	private int waveNum = 0;
	private int foxCount = 0;
	private int maxWaves = 4;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.foxes = new Queue<PackedScene>();
		this.elapsed = 0.0;
		waveNum = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		elapsed += delta;
		if (elapsed > REFRESH_RATE)
		{
			if (foxCount == 0 && waveNum > maxWaves)
			{
				GD.Print("All waves completed!");
			}
			else if (foxCount == 0)
			{
				StartWave();
			}

			elapsed = 0.0;
		}
	}

	public void AddFox(PackedScene fox)
	{
		foxes.Enqueue(fox);
	}

	public void StartWave()
	{
		GD.Print(waveNum);
		int foxesToSpawn = 1 * waveNum;
		for (int i = 0; i < foxesToSpawn; i++)
		{
			PackedScene foxScene = foxes.Dequeue();
			if (foxScene != null)
			{
				// Create an instance of the scene
				Node foxNodeInstance = foxScene.Instantiate();
				// Check if the instance is a Node2D
				if (foxNodeInstance is Node2D foxNode)
				{
					this.AddChild(foxNode);
					foxCount++;
					// Connect the FoxDestroyed signal with the updated delegate name
				  if (foxNode is Fox foxInstance)
					{
						foxInstance.FoxDestroyed += OnFoxDestroyed;
					}

				}
			}
		}
	}

private void OnFoxDestroyed()
{
	foxCount--;
	if (foxCount == 0)
	{
		GD.Print("All foxes destroyed for this wave!");
		waveNum++;
	}
}

}
