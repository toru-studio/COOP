using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class LightningTower : Tower
{
	private Area2D StrikeZone;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		this.TriggerPeriod = 10;
		try
		{
			this.StrikeZone = GetNode<Area2D>("StrikeZone");
		}
		catch (Exception e)
		{
			GD.Print("StrikeZone could not be found");
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
		Array<Area2D> overlappingBeam = this.StrikeZone.GetOverlappingAreas();
		foreach (Area2D area in overlappingBeam)
		{
			if (area.GetParent().GetType().BaseType == typeof(Fox))
			{
				area.GetParent<Fox>().ShineOn(10);
				area.GetParent<Fox>().StunTargets();
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
		var temp = GetChild(0);
		Console.WriteLine(temp);
		this.Rotation = dir.Angle();
	}
}
