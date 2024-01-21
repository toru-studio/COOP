using Godot;
using System;

public partial class Game : Node2D
{
	private FoxSpawner Spawner;
	private Label CurrencyLabel;
	private int Currency;
	private Random Random;

	private double test_elapsed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random = new Random();
		test_elapsed = 0.0;
		
		// Link Nodes
		try
		{
			this.Spawner = (FoxSpawner)this.FindChild("Pathing").FindChild("Path2D");
		}
		catch (Exception e)
		{
			GD.Print("Fox Spawner Not Found");
		}

		try
		{
			this.CurrencyLabel = (Label)this.FindChild("Currency");
		}
		catch (Exception e)
		{
			GD.Print("Currency Label Not Found");
		}

		this.CurrencyLabel.Text = "0";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		test_elapsed += delta;
		if (test_elapsed > 1.0)
		{
			this.Spawner.AddFox(GD.Load<PackedScene>("res://Source/Entities/Fox/fox.tscn"));
			test_elapsed = 0.0;
		}
	}

	public int GetCurrency()
	{
		return Currency;
	}

	public void AddCurrency(int value)
	{
		Currency += value;
		CurrencyLabel.Text = Convert.ToString(Currency);
	}

	public double GetRandom()
	{
		return Random.NextDouble();
	}
}
