using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Extensions
{
    class Particle
    {
        public Vector2 Position { get; set;  }
        public Vector2 Scale { get; set; }
        public ParticleProperties properties { get; set; }

        public bool Active { get; set; }
    }

    struct ParticleProperties
    {
        Vector3 color;
    }
}
