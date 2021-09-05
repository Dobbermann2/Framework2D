using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Extensions
{
    //TODO: Move to engine
    class ParticleSystem
    {

        private Particle[] particlePool;

        EmitterProperties emitterProperties;


        public ParticleSystem(EmitterProperties emitterProperties, ParticleProperties particleProperties)
        {
            this.emitterProperties = emitterProperties;

            particlePool = new Particle[emitterProperties.ParticleLimit];
            
            for(int i = 0; i < particlePool.Length; i++)
            {
                particlePool[i] = new Particle();
            }
        }

        public void Draw(BatchRenderer renderer)
        {

        }


    }

    public struct EmitterProperties {
        public int ParticleLimit { get; set; }
        public int EmitRate { get; set; }
    }
}
