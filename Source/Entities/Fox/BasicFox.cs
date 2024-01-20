using System;
using COOP.Source.Entities.Fox;
using Godot;

public partial class BasicFox : Fox
{
    private bool HasFlower;
    private Sprite2D Sprite2D;
    public override void _Ready()
    {
        base._Ready();
        base.NormalSpeed = 0.05f;
        base.BlindSpeed = -0.02f;
        base.FleeSpeed = -0.3f;
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

        // Random Chance Of Flower
        Game game = (Game)this.GetTree().CurrentScene;
        HasFlower = game.GetRandom() < 0.3;
        if (HasFlower)
        {
            this.Sprite2D.Texture = GD.Load<Texture2D>("res://Assets/Images/Enemies/FoxWithFlower.png");
        }
    }
    public override void ShineOn(double strength)
    {
        base.BlindLevel += strength;
    }
}