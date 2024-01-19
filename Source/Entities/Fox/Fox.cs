using System;
using COOP.Source.Entities.Fox;
using Godot;

public abstract partial class Fox : PathFollow2D
{
	private const float PATH_END = 0.985f;
	private const float PATH_START = 0.05f;

	protected float NormalSpeed;
	protected float BlindSpeed;
	protected float FleeSpeed;

	protected int BlindLevel;
	protected double ElapsedBlindness;
	protected double MaxElapsedBlindness;
	protected double ElapsedRecovery;
	protected double MaxElapsedRecovery;
	protected FoxState State;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ProgressRatio = 0.0f;
		this.ElapsedBlindness = 0.0;
		this.ElapsedRecovery = 0.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		switch (State)
		{
			case FoxState.NORMAL:
				this.Normal(delta);
				break;
			case FoxState.BLIND:
				this.Blind(delta);
				break;
			case FoxState.FLEE:
				this.Flee(delta);
				break;
		}
	}

	private void Normal(double delta)
	{
		this.ProgressRatio += NormalSpeed * (float)delta;
		if (this.ProgressRatio > PATH_END)
		{
			this.Free();
        }
		if (BlindLevel >= 100)
        {
			this.State = FoxState.BLIND;
        }
	}

	private void Blind(double delta)
	{
		this.ProgressRatio += BlindSpeed * (float)delta;
	}

	private void Flee(double delta)
	{
		this.ProgressRatio += FleeSpeed * (float)delta;
		if (this.ProgressRatio < PATH_END)
		{
			this.Free();
		}
	}
	
	public abstract void ShineOn(double strength);
}
