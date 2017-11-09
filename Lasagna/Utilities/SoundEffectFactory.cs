using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Lasagna
{
    public class SoundEffectFactory
    {
        public SoundEffect oneUp;
        public SoundEffect bowserFall;
        public SoundEffect bowserFire;
        public SoundEffect breakBlock;
        public SoundEffect bump;
        public SoundEffect coin;
        public SoundEffect fireball;
        public SoundEffect fireworks;
        public SoundEffect flagpole;
        public SoundEffect gameover;
        public SoundEffect jumpMarioBig;
        public SoundEffect jumpMarioSmall;
        public SoundEffect kick;
        public SoundEffect marioDie;
        public SoundEffect pause;
        public SoundEffect pipe;
        public SoundEffect powerUp;
        public SoundEffect powerUpAppears;
        public SoundEffect stageClear;
        public SoundEffect stomp;
        public SoundEffect vine;
        public SoundEffect warning;
        public SoundEffect worldClear;
        public List<SoundEffect> soundEffects = new List<SoundEffect>();
        private static SoundEffectFactory instance;

        public static SoundEffectFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundEffectFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            oneUp = content.Load<SoundEffect>("SFX/1-UP");
            bowserFall = content.Load<SoundEffect>("SFX/BowserFall");
            bowserFire = content.Load<SoundEffect>("SFX/BowserFire");
            breakBlock = content.Load<SoundEffect>("SFX/BreakBlock");
            bump = content.Load<SoundEffect>("SFX/Bump");
            coin = content.Load<SoundEffect>("SFX/Coin");
            fireball = content.Load<SoundEffect>("SFX/Fireball");
            fireworks = content.Load<SoundEffect>("SFX/Fireworks");
            flagpole = content.Load<SoundEffect>("SFX/Flagpole");
            gameover = content.Load<SoundEffect>("SFX/Gameover");
            jumpMarioBig = content.Load<SoundEffect>("SFX/JumpMarioBig");
            jumpMarioSmall = content.Load<SoundEffect>("SFX/JumpMarioSmall");
            kick = content.Load<SoundEffect>("SFX/Kick");
            marioDie = content.Load<SoundEffect>("SFX/MarioDie");
            pause = content.Load<SoundEffect>("SFX/Pause");
            pipe = content.Load<SoundEffect>("SFX/Pipe");
            powerUp = content.Load<SoundEffect>("SFX/PowerUp");
            powerUpAppears = content.Load<SoundEffect>("SFX/PowerUpAppears");
            stageClear = content.Load<SoundEffect>("SFX/StageClear");
            stomp = content.Load<SoundEffect>("SFX/Stomp");
            vine = content.Load<SoundEffect>("SFX/Vine");
            warning = content.Load<SoundEffect>("SFX/Warning");
            worldClear = content.Load<SoundEffect>("SFX/WorldClear");
            soundEffects.Add(oneUp);
            soundEffects.Add(bowserFall);
            soundEffects.Add(bowserFire);
            soundEffects.Add(breakBlock);
            soundEffects.Add(bump);
            soundEffects.Add(coin);
            soundEffects.Add(fireball);
            soundEffects.Add(fireworks);
            soundEffects.Add(flagpole);
            soundEffects.Add(gameover);
            soundEffects.Add(jumpMarioBig);
            soundEffects.Add(jumpMarioSmall);
            soundEffects.Add(kick);
            soundEffects.Add(marioDie);
            soundEffects.Add(pipe);
            soundEffects.Add(powerUp);
            soundEffects.Add(powerUpAppears);
            soundEffects.Add(stageClear);
            soundEffects.Add(stomp);
            soundEffects.Add(vine);
            soundEffects.Add(warning);
            soundEffects.Add(worldClear);
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
            soundEffects = null;
        }
    }
}
