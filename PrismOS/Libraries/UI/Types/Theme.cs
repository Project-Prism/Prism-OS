using PrismOS.Libraries.Graphics;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.UI.Types
{
    public class Theme : IDisposable
    {
        public static Theme Default = new()
        {
            Background = Color.StackOverflowBlack,
            Foreground = Color.StackOverflowWhite,
            Accent = Color.StackOverflowOrange,
            Text = Color.White,

            BackgroundHover = Color.LighterBlack,
            ForegroundHover = Color.White,
            AccentHover = Color.StackOverflowOrange,
            TextHover = Color.White,

            BackgroundClick = Color.StackOverflowWhite,
            ForegroundClick = Color.StackOverflowBlack,
            AccentClick = Color.StackOverflowOrange,
            TextClick = Color.Black,

            Radius = 0,
        };

        // Normal Colors
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Color Accent { get; set; }
        public Color Text { get; set; }

        // Hover Colors
        public Color BackgroundHover { get; set; }
        public Color ForegroundHover { get; set; }
        public Color AccentHover { get; set; }
        public Color TextHover { get; set; }

        // Click Colors
        public Color BackgroundClick { get; set; }
        public Color ForegroundClick { get; set; }
        public Color AccentClick { get; set; }
        public Color TextClick { get; set; }

        // Extra Properties
        public uint Radius { get; set; }

        public Color GetBackground(bool Click, bool Hover)
        {
            if (Hover)
            {
                if (Click)
                {
                    return BackgroundClick;
                }
                else
                {
                    return BackgroundHover;
                }
            }
            return Background;
        }
        public Color GetForeground(bool Click, bool Hover)
        {
            if (Hover)
            {
                if (Click)
                {
                    return ForegroundClick;
                }
                else
                {
                    return ForegroundHover;
                }
            }
            return Foreground;
        }
        public Color GetAccent(bool Click, bool Hover)
        {
            if (Hover)
            {
                if (Click)
                {
                    return AccentClick;
                }
                else
                {
                    return AccentHover;
                }
            }
            return Accent;
        }
        public Color GetText(bool Click, bool Hover)
        {
            if (Hover)
            {
                if (Click)
                {
                    return TextClick;
                }
                else
                {
                    return TextHover;
                }
            }
            return Text;
        }

        public void Dispose()
		{
            Background.Dispose();
            Foreground.Dispose();
            Accent.Dispose();
            Text.Dispose();

            BackgroundHover.Dispose();
            ForegroundHover.Dispose();
            AccentHover.Dispose();
            TextHover.Dispose();

            BackgroundClick.Dispose();
            ForegroundClick.Dispose();
            AccentClick.Dispose();
            TextClick.Dispose();

            GCImplementation.Free(Radius);
            GC.SuppressFinalize(this);
		}
    }
}