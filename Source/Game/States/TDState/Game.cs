using Godot;
using System;

public partial class Game : Node2D
{
	private FoxSpawner Spawner;
	private Label CurrencyLabel;
	private int Currency;
	private Label HealthLabel;
	private int Health;
	private bool GameOver;
	private Random Random;

	private double test_elapsed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random = new Random();
		test_elapsed = 0.0;
		GameOver = false;
		
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
		
		try
		{
			this.HealthLabel = (Label)this.FindChild("Health");
		}
		catch (Exception e)
		{
			GD.Print("Health Label Not Found");
		}

		this.Health = 100;
		this.HealthLabel.Text = "100";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameOver) return;
		if (this.Health <= 0)
		{
			Node scene = this.GetTree().CurrentScene;
			Node2D gameOver = (Node2D)scene.FindChild("GameOver");
			// Destroy the panel
			scene.FindChild("Panel").QueueFree();
			// TODO Stop Waves
			// Switch Music
			scene.FindChild("Node").QueueFree();
			((AudioStreamPlayer)scene.FindChild("GameOverMusic")).Play();
			// Show end card
			gameOver.Visible = true;
			((AnimationPlayer)gameOver.FindChild("AnimationPlayer")).Play("Game Over");
			GameOver = true;
		}
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

	public void RemoveHealth(int value)
	{
		Health -= value;
		HealthLabel.Text = Convert.ToString(Health);
	}
	
	public double GetRandom()
	{
		return Random.NextDouble();
	}
}
