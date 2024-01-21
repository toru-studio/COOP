using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class LightTower : Tower
{
	private Area2D StrongBeam;
	private Area2D WeakBeam;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		this.TriggerPeriod = 1.0;
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

	protected override void OnAreaEntered(Node node)
	{
		if (node.GetParent().GetType().BaseType == typeof(Fox))
		{
			this.FoxLL.AddLast(node.GetParent<Fox>());
		}
	}

	protected override void OnAreaExited(Node node)
	{
		if (node.GetParent().GetType().BaseType == typeof(Fox))
		{
			LinkedListNode<Fox> n = this.FoxLL.Find(node.GetParent<Fox>());
			if (n != null)
			{
				this.FoxLL.Remove(n);
			}
		}
	}

	protected override void Attack()
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

	protected override void LookAt()
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
