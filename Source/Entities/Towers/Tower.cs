using System;
using System.Collections.Generic;
using Godot;

public abstract partial class Tower : Node2D
{
	protected Area2D Area2D;
    
	protected  LinkedList<Fox> FoxLL;
	protected  double Alapsed;
	protected double TriggerPeriod;

	public override void _Ready()
	{
		this.Alapsed = 0.0;
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

	protected abstract void OnAreaEntered(Node area);
	
	protected abstract void OnAreaExited(Node area);

	protected abstract void Attack();

	protected abstract void LookAt();
}