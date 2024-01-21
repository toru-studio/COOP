using Godot;
using System;

public partial class LightTower : Node2D
{
	private Area2D Area2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Area2D = GetNode<Area2D>("Area2D");
		this.Area2D.AreaEntered += OnAreaEntered;
		this.Area2D.AreaExited += OnAreaExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnAreaEntered(Node node)
	{
		if (node.GetParent().GetType().BaseType == typeof(Fox))
		{
			GD.Print("TEST");
		}
	}

	private void OnAreaExited(Node node)
	{
		GD.Print("test");
	}
}
