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
        }

        public void GetOneUp()
        {
            oneUp.Play();
        }

        public void PlayBowserFall()
        {
            bowserFall.Play();
        }

        public void PlayBowserFire()
        {
            bowserFire.Play();
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
            fireworks.Play();
        }

        public void PlayFlagpole()
        {
            flagpole.Play();
        }

        public void PlayGameOver()
        {
            gameover.Play();
        }

        public void PlayJumpMarioBig()
        {
            jumpMarioBig.Play();
        }

        public void PlayJumpMarioSmall()
        {
            jumpMarioSmall.Play();
        }

        public void PlayKick()
        {
            kick.Play();
        }

        public void PlayMarioDie()
        {
            marioDie.Play();
        }

        public void PlayPauseSound()
        {
            pause.Play();
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
            stageClear.Play();
        }

        public void PlayStomp()
        {
            stomp.Play();
        }

        public void PlayVine()
        {
            vine.Play();
        }

        public void PlayWarning()
        {
            warning.Play();
        }

        public void PlayWorldClear()
        {
            worldClear.Play();
        }
    }
}
