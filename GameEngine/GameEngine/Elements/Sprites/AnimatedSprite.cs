using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements.Sprites;

public class AnimatedSprite
{
    public Dictionary<AnimationEnum, Animation> Animations = new Dictionary<AnimationEnum, Animation>();

    public SpriteEffects FlipHorizontally; 

    public AnimationEnum State {  get; private set; }

    public void SetState(AnimationEnum state)
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

    public int Pixels;

    public Visibility Visibility  = new Visibility();

    public Order Ordering = new Order();

    public AnimatedSprite(
        Color color,
        Dictionary<AnimationEnum, Animation> animations,
        AnimationEnum state,
        int pixels)
    {
        Color = color;
        Animations = animations;
        State = state;
        ElapsedTime = TimeSpan.Zero;
        Pixels = pixels;
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