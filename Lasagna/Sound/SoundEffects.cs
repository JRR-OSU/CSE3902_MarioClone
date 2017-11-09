using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Lasagna
{
    public class SoundEffects
    {
        private List<SoundEffect> soundEffects = new List<SoundEffect>();

        public SoundEffects()
        {
            
        }

        public void GetOneUp()
        {
            if (soundEffects != null)
            {
                soundEffects[0].Play();
            }
        }

        public void BowserFall()
        {
            if (soundEffects != null)
            {
                soundEffects[1].Play();
            }
        }

        public void BowserFire()
        {
            if (soundEffects != null)
            {
                soundEffects[2].Play();
            }
        }

        public void BreakBlock()
        {
            if (soundEffects != null)
            {
                soundEffects[3].Play();
            }
        }

        public void Bump()
        {
            if (soundEffects != null)
            {
                soundEffects[4].Play();
            }
        }

        public void Coin()
        {
            if (soundEffects != null)
            {
                soundEffects[5].Play();
            }
        }

        public void Fireball()
        {
            if (soundEffects != null)
            {
                soundEffects[6].Play();
            }
        }

        public void Fireworks()
        {
            if (soundEffects != null)
            {
                soundEffects[7].Play();
            }
        }

        public void Flagpole()
        {
            if (soundEffects != null)
            {
                soundEffects[8].Play();
            }
        }

        public void Gameover()
        {
            if (soundEffects != null)
            {
                soundEffects[9].Play();
            }
        }

        public void JumpMarioBig()
        {
            if (soundEffects != null)
            {
                soundEffects[10].Play();
            }
        }

        public void JumpMarioSmall()
        {
            if (soundEffects != null)
            {
                soundEffects[11].Play();
            }
        }

        public void Kick()
        {
            if (soundEffects != null)
            {
                soundEffects[12].Play();
            }
        }

        public void MarioDie()
        {
            if (soundEffects != null)
            {
                soundEffects[13].Play();
            }
        }

        public void Pause()
        {
            if (soundEffects != null)
            {
                soundEffects[14].Play();
            }
        }

        public void Pipe()
        {
            if (soundEffects != null)
            {
                soundEffects[15].Play();
            }
        }

        public void PowerUp()
        {
            if (soundEffects != null)
            {
                soundEffects[16].Play();
            }
        }

        public void PowerUpAppears()
        {
            if (soundEffects != null)
            {
                soundEffects[17].Play();
            }
        }

        public void StageClear()
        {
            if (soundEffects != null)
            {
                soundEffects[18].Play();
            }
        }

        public void Stomp()
        {
            if (soundEffects != null)
            {
                soundEffects[19].Play();
            }
        }

        public void Vine()
        {
            if (soundEffects != null)
            {
                soundEffects[20].Play();
            }
        }

        public void Warning()
        {
            if (soundEffects != null)
            {
                soundEffects[21].Play();
            }
        }

        public void WorldClear()
        {
            if (soundEffects != null)
            {
                soundEffects[22].Play();
            }
        }

        public void Clear()
        {
            soundEffects = new List<SoundEffect>();
        }
    }
}
