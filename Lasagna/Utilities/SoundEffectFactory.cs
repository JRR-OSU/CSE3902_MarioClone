using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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

        public void PlayBowserFall()
        {
            if (soundEffects != null)
            {
                soundEffects[1].Play();
            }
        }

        public void PlayBowserFire()
        {
            if (soundEffects != null)
            {
                soundEffects[2].Play();
            }
        }

        public void PlayBrickBlock()
        {
            breakBlock.Play();
        }

        public void PlayBump()
        {
            bump.Play();
        }

        public void PlayCoin()
        {
            coin.Play();
        }

        public void PlayFireball()
        {
            fireball.Play();
        }

        public void PlayFireworks()
        {
            if (soundEffects != null)
            {
                soundEffects[7].Play();
            }
        }

        public void PlayFlagpole()
        {
            if (soundEffects != null)
            {
                soundEffects[8].Play();
            }
        }

        public void PlayGameOver()
        {
            if (soundEffects != null)
            {
                soundEffects[9].Play();
            }
        }

        public void PlayJumpMarioBig()
        {
            if (soundEffects != null)
            {
                soundEffects[10].Play();
            }
        }

        public void PlayJumpMarioSmall()
        {
            if (soundEffects != null)
            {
                soundEffects[11].Play();
            }
        }

        public void PlayKick()
        {
            if (soundEffects != null)
            {
                soundEffects[12].Play();
            }
        }

        public void PlayMarioDie()
        {
            if (soundEffects != null)
            {
                soundEffects[13].Play();
            }
        }

        public void PlayPauseSound()
        {
            if (soundEffects != null)
            {
                soundEffects[14].Play();
            }
        }

        public void PlayPipeSound()
        {
            pipe.Play();
        }

        public void PlayPowerUp()
        {
            powerUp.Play();
        }

        public void PlayPowerUpAppearsSound()
        {
            powerUpAppears.Play();
        }

        public void PlayStageClear()
        {
            if (soundEffects != null)
            {
                soundEffects[18].Play();
            }
        }

        public void PlayStomp()
        {
            stomp.Play();
        }

        public void PlayVine()
        {
            if (soundEffects != null)
            {
                soundEffects[20].Play();
            }
        }

        public void PlayWarning()
        {
            if (soundEffects != null)
            {
                soundEffects[21].Play();
            }
        }

        public void PlayWorldClear()
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
