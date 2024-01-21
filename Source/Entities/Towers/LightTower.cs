using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class LightTower : Node2D
{
	private Area2D Area2D;
	private Area2D StrongBeam;
	private Area2D WeakBeam;

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

		try
		{
			this.StrongBeam = GetNode<Area2D>("StrongBeam");
			this.WeakBeam = GetNode<Area2D>("WeakBeam");
		}
		catch (Exception e)
		{
			GD.Print("Strong and/or Weak could not be found");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Alapsed += delta;
		this.LookAt();
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
		Array<Area2D> overlappingStrong = this.StrongBeam.GetOverlappingAreas();
		Array<Area2D> overlappingWeak = this.WeakBeam.GetOverlappingAreas();
		foreach (Area2D area in overlappingStrong)
		{
			if (area.GetParent().GetType().BaseType == typeof(Fox))
			{
				area.GetParent<Fox>().ShineOn(50);
			}
		}

		foreach (Area2D area in overlappingWeak)
		{
			if (area.GetParent().GetType().BaseType == typeof(Fox))
			{
				area.GetParent<Fox>().ShineOn(25);
			}
		}
	}

	private void LookAt()
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

		Vector2 dir = target.Value.GlobalPosition - GlobalPosition;
		this.Rotation = dir.Angle();
	}
}
