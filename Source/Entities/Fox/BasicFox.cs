﻿using System;
using COOP.Source.Entities.Fox;
using Godot;

public partial class BasicFox : Fox
{
    private bool HasFlower;
    private Sprite2D Sprite2D;
    private AnimationPlayer AnimationPlayer;
    private Node2D Sounds;
    public override void _Ready()
    {
        base._Ready();
        base.NormalSpeed = 0.05f;
        base.BlindSpeed = -0.02f;
        base.FleeSpeed = -0.05f;
        base.State = FoxState.NORMAL;
        base.MaxElapsedBlindness = 3.0;
        base.RecoveryRate = 20.0;
        base.RecoverBlindLevel = 80.0;
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
        HasFlower = game.GetRandom() < 0.3;
        this.AnimationPlayer.Play(HasFlower ? "Flower Walk Cycle" : "walk cycle");
    }
    public override void ShineOn(double strength)
    {
        base.BlindLevel += strength;
    }

    protected override void Normal(double delta)
    {
        if (BlindLevel >= 100)
        {
            // Change Sounds
            AudioStreamPlayer2D aspWalk = (AudioStreamPlayer2D)this.Sounds.FindChild("Walking");
            aspWalk.Stop();
            AudioStreamPlayer2D aspBlind = (AudioStreamPlayer2D)this.Sounds.FindChild("Blinded");
            aspBlind.Play();
            // Change Animation
            double curFrame = this.AnimationPlayer.CurrentAnimationPosition;
            this.AnimationPlayer.Play(HasFlower ? "Blind Flower Walk Cycle" : "Blind Walk Cycle");
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
            this.AnimationPlayer.Play(HasFlower ? "Flower Walk Cycle" : "walk cycle");
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
    protected override void Flee(double delta)
    {
        base.Flee(delta);
        if (HasFlower)
        {
            PackedScene flowerScene = GD.Load<PackedScene>("res://Source/Entities/Items/flower.tscn");
            Node flower = flowerScene.Instantiate();
            Sprite2D flowerSprite = flower.GetChild<Sprite2D>(0);

            Game game = (Game)this.GetTree().CurrentScene;
            flowerSprite.Position = this.GlobalPosition;
            flowerSprite.RotationDegrees = 360 * (float)game.GetRandom();
                                           
            this.GetTree().CurrentScene.AddChild(flower);

            double curFrame = this.AnimationPlayer.CurrentAnimationPosition;
            this.AnimationPlayer.Play("walk cycle");
            this.AnimationPlayer.Seek(curFrame);
            HasFlower = false;
        }
    }
}