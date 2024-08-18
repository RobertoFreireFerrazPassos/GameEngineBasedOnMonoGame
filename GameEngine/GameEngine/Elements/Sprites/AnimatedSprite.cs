using GameEngine.Elements.Managers;
using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements.Sprites;

public class AnimatedSprite
{
    public Dictionary<AnimationEnum, Animation> Animations = new Dictionary<AnimationEnum, Animation>();

    public static Texture Texture { get; set; }

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

    public Color Color;

    public Visibility Visibility  = new Visibility();

    public Order Ordering = new Order();

    public AnimatedSprite(
        Color color,
        Dictionary<AnimationEnum, Animation> animations,
        AnimationEnum state,
        Texture texture)
    {
        Color = color;
        Animations = animations;
        State = state;
        ElapsedTime = TimeSpan.Zero;
        Texture = texture;
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

    public Rectangle GetSourceRectangle()
    {
        var animation = Animations.GetValueOrDefault(State);
        var spriteNumber = animation.Frames[CurrentFrameIndex];
        var pixels = Texture.Pixels;
        (int x, int y) = Texture.ConvertNumberToXY(spriteNumber);
        return new Rectangle(x * pixels, y * pixels, pixels, pixels);
    }
}