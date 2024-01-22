using System;
using COOP.Source.Entities.Fox;
using Godot;

public partial class HeavyFox: Fox
{
	private double SniffAlapsed;
	
	private Sprite2D Sprite2D;
	private AnimationPlayer AnimationPlayer;
	private Node2D Sounds;
	public override void _Ready()
	{
		base._Ready();
		base.NormalSpeed = 0.03f;
		base.BlindSpeed = 0.03f;
		base.FleeSpeed = -0.03f;
		base.State = FoxState.NORMAL;
		base.MaxElapsedBlindness = 10.0;
		base.RecoveryRate = 20.0;
		base.RecoverBlindLevel = 80.0;
		base.MaxStunned = 4.0;
		try
		{
			this.Sprite2D = (Sprite2D)this.FindChild("Sprite2D");
		}
		catch (Exception e)
		{
			GD.Print("No Sprite 2D on Fox");
		}

		try
		{
			this.AnimationPlayer = (AnimationPlayer)this.FindChild("AnimationPlayer");
		}
		catch (Exception e)
		{
			GD.Print("No Animation Player Found");
		}

		try
		{
			this.Sounds = (Node2D)this.FindChild("Sounds");
		}
		catch (Exception e)
		{
			GD.Print("Cannot find sound node");
		}


		// Random Chance Of Flower
		Game game = (Game)this.GetTree().CurrentScene;
		this.AnimationPlayer.Play("walk cycle");
	}
	public override void ShineOn(double strength)
	{
		base.BlindLevel += strength;
	}
	public override void StunTargets(){
		//TODO Check state maybe ?
		base.State = FoxState.STUNNED;
		this.AnimationPlayer.Stop();
	}

	protected override void Normal(double delta)
	{
		SniffAlapsed += delta;
		Game game = (Game)this.GetTree().CurrentScene;
		if (SniffAlapsed > 2)
		{
			switch (game.GetRandom())
			{
				case < .05:
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff1")).Stop();
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff2")).Stop();
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff1")).Play();
					break;
				case < .1:
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff1")).Stop();
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff2")).Stop();
					((AudioStreamPlayer2D)this.Sounds.FindChild("Sniff2")).Play();
					break;
			}

			SniffAlapsed = 0.0;
		}

		// Random Chance of Sniffing
		if (BlindLevel >= 100)
		{
			// Change Sounds
			AudioStreamPlayer2D aspWalk = (AudioStreamPlayer2D)this.Sounds.FindChild("Walking");
			aspWalk.Stop();
			AudioStreamPlayer2D aspBlind = (AudioStreamPlayer2D)this.Sounds.FindChild("Blinded");
			aspBlind.Play();
			
			// Random Chance of Squawk
			switch (game.GetRandom())
			{
				case < .3:
					((AudioStreamPlayer2D)this.Sounds.FindChild("Squawk1")).Play();
					break;
				case < 0.6:
					((AudioStreamPlayer2D)this.Sounds.FindChild("Squawk2")).Play();
					break;
			}
			// Change Animation
			double curFrame = this.AnimationPlayer.CurrentAnimationPosition;
			this.AnimationPlayer.Play("Blind Walk Cycle");
			this.AnimationPlayer.Seek(curFrame);
		}
		base.Normal(delta);
	}

	protected override void Blind(double delta)
	{
		if (this.BlindLevel < RecoverBlindLevel)
		{
			// Change Sounds
			AudioStreamPlayer2D aspBlind = (AudioStreamPlayer2D)this.Sounds.FindChild("Blinded");
			aspBlind.Stop();
			AudioStreamPlayer2D aspWalk = (AudioStreamPlayer2D)this.Sounds.FindChild("Walking");
			aspWalk.Play();
			double curFrame = this.AnimationPlayer.CurrentAnimationPosition;
			this.AnimationPlayer.Play("walk cycle");
			this.AnimationPlayer.Seek(curFrame);
		}
		
		if (this.ElapsedBlindness >= this.MaxElapsedBlindness)
		{
			AudioStreamPlayer2D aspBlind = (AudioStreamPlayer2D)this.Sounds.FindChild("Blinded");
			aspBlind.Stop();
			AudioStreamPlayer2D aspRun = (AudioStreamPlayer2D)this.Sounds.FindChild("Running");
			aspRun.Play();
		}
		base.Blind(delta);
	}
}