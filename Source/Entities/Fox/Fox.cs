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

	protected double BlindLevel;
	protected double ElapsedBlindness;
	protected double MaxElapsedBlindness;
	
	protected double RecoveryRate;
	protected double RecoverBlindLevel;
	
	protected double ElapsedStunned;
	protected double MaxStunned;
	
	protected FoxState State;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ProgressRatio = 0.0f;
		this.ElapsedBlindness = 0.0;
		this.BlindLevel = 0.0;
		this.ElapsedStunned = 0.0;
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
			case FoxState.STUNNED:
				this.Stunned(delta);
				break;
		}
	}

	protected virtual void Normal(double delta)
	{
		this.ProgressRatio += NormalSpeed * (float)delta;
		
		if (this.ProgressRatio > PATH_END)
		{
			((Game)this.GetTree().CurrentScene).RemoveHealth(5);
			this.Free();
		}
		
		// Blind the fox at full blindness
		if (BlindLevel >= 100)
		{
			this.ElapsedBlindness = 0.0;
			this.State = FoxState.BLIND;
		}

		// Recover blindness
		if (BlindLevel <= 0) return;
		this.BlindLevel -= this.RecoveryRate * delta;
		if (this.BlindLevel < 0.0)
		{
			this.BlindLevel = 0.0;
		}
	}

	protected virtual void Blind(double delta)
	{
		this.ElapsedBlindness += delta;
		this.BlindLevel -= this.RecoveryRate * delta;
		float r = this.ProgressRatio + BlindSpeed * (float)delta;
		this.ProgressRatio = r < 0 ? 0 : r;
		
		// Fox recovers at RecoverBlindLevel
		if (this.BlindLevel < RecoverBlindLevel)
		{
			this.State = FoxState.NORMAL;
		}
		
		// Fox flees if blind for too long
		if (this.ElapsedBlindness >= this.MaxElapsedBlindness)
		{
			this.State = FoxState.FLEE;
			Sprite2D sprite = this.GetChild<Sprite2D>(0);
			sprite.Scale = new Vector2(0.025f, -0.025f);

			float prevLength = this.GetParent<Path2D>().Curve.GetBakedLength();
			this.Reparent(this.GetTree().CurrentScene.FindChild("Pathing").FindChild("FleePath2D"));
			this.ProgressRatio =
				this.ProgressRatio * (this.GetParent<Path2D>().Curve.GetBakedLength() / prevLength);

			// AnimationPlayer ap = (AnimationPlayer)this.FindChild("AnimationPlayer");
			// ap.AnimationFinished += FixAnimation;
			// ap.Play("Switch Lanes");
		}
	}

	protected virtual void Flee(double delta)
	{
		float r = this.ProgressRatio + FleeSpeed * (float)delta;
		this.ProgressRatio = r < 0 ? 0 : r;
		if (this.ProgressRatio < PATH_START)
		{
			this.Free();
		}
	}
	
	protected virtual void Stunned(double delta){
		this.ElapsedStunned += delta;
		if (ElapsedStunned >= MaxStunned){
			ElapsedStunned = 0.0;
			this.State = FoxState.NORMAL;
		}
	}
	
	//
	// public void FixAnimation(StringName animName)
	// {
	// 	if (animName != "Switch Lanes") return;
	// 	
	// 	AnimationPlayer ap = (AnimationPlayer)this.FindChild("AnimationPlayer");
	// 	ap.AnimationFinished -= FixAnimation;
	// 	ap.Play("Blind Walk Cycle");
	// }
	
	public abstract void ShineOn(double strength);
	public abstract void StunTargets();
}
