using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements.Sprites;

internal class AnimatedSprite
{
    public Texture Texture;

    public Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();

    public SpriteEffects FlipHorizontally; 

    public string State {  get; private set; }

    public void SetState(string state)
    {
        if (State == state)
        {
            return;
        }

        State = state;
        CurrentFrameIndex = 0;
    }

    public int CurrentFrameIndex;

    public TimeSpan ElapsedTime;

    public int Sprite
    {
        get
        {
            var animation = Animations.GetValueOrDefault(State);
            if (animation is null) return 0;

            return animation.Frames[CurrentFrameIndex];
        }
    }

    public Color Color;

    public Visibility Visibility  = new Visibility();

    public Order Ordering = new Order();

    public AnimatedSprite(
        Texture texture, 
        Color color,
        Dictionary<string, Animation> animations,
        string state)
    {
        Texture = texture;
        Color = color;
        Animations = animations;
        State = state;
        ElapsedTime = TimeSpan.Zero;
    }

    public void Update(GameTime gameTime)
    {
        var animation = Animations.GetValueOrDefault(State);

        if (animation is null) return;

        ElapsedTime += gameTime.ElapsedGameTime;

        if (ElapsedTime >= animation.FrameDuration)
        {
            ElapsedTime -= animation.FrameDuration;
            CurrentFrameIndex++;

            if (CurrentFrameIndex >= animation.Frames.Length)
            {
                if (animation.Loop)
                {
                    CurrentFrameIndex = 0;
                }
                else
                {
                    CurrentFrameIndex = animation.Frames.Length - 1;
                }
            }
        }
    }
}