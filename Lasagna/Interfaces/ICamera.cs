using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Lasagna
{
    public interface ICamera
    {
        Matrix Transform { get; }

        void Update(List<IPlayer> players, float screenWidth, float screenHeight);
        void Reset(object sender, EventArgs e);
    }
}
