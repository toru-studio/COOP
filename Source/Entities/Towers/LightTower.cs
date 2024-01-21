using Godot;
using System;
using System.Collections.Generic;

public partial class LightTower : Node2D
{
	private Area2D Area2D;

	private LinkedList<Fox> FoxLL;
	private double Alapsed;
	private double TriggerPeriod;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Alapsed = 0.0;
		this.TriggerPeriod = 1.0;
		this.FoxLL = new LinkedList<Fox>();
		try
		{
			this.Area2D = GetNode<Area2D>("Area2D");
			this.Area2D.AreaEntered += OnAreaEntered;
			this.Area2D.AreaExited += OnAreaExited;
		}
		catch (Exception e)
		{
			GD.Print("Area2D on tower could not be found");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Alapsed += delta;
		if (this.Alapsed >= this.TriggerPeriod)
		{
			this.Attack();
			this.Alapsed = 0.0;
		}
		
	}

	private void OnAreaEntered(Node node)
	{
		if (node.GetParent().GetType().BaseType == typeof(Fox))
		{
			FoxLL.AddLast(node.GetParent<Fox>());
		}
	}

	private void OnAreaExited(Node node)
	{
		if (node.GetParent().GetType().BaseType == typeof(Fox))
		{
			LinkedListNode<Fox> n = FoxLL.Find(node.GetParent<Fox>());
			if (n != null)
			{
				FoxLL.Remove(n);
			}
		}
	}

	private void Attack()
	{
		if (FoxLL.Count == 0)
		{
			return;
		}
		LinkedListNode<Fox> target = FoxLL.First;
		while (target.Value == null)
		{
			target = target.Next;
		}
		target.Value.ShineOn(300);
	}
}
