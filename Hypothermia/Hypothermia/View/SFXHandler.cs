using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{

    public class SFXHandler
    {
        private SoundEffect walkSFX;
        private SoundEffectInstance walkInstance;
        private SoundEffect sprintSFX;
        private SoundEffectInstance sprintInstance;

        private SoundEffect jumpSFX;
        private SoundEffect landSFX;
        
        protected bool isWalking = false;

        public SFXHandler(ContentManager content)
        {
            this.walkSFX = content.Load<SoundEffect>("Sounds/Effects/player_walk_snow_01");
            this.walkInstance = this.walkSFX.CreateInstance();
            this.walkInstance.IsLooped = true;

            this.sprintSFX = content.Load<SoundEffect>("Sounds/Effects/player_sprint_snow_01");
            this.sprintInstance = this.sprintSFX.CreateInstance();
            this.sprintInstance.IsLooped = true;

            this.jumpSFX = content.Load<SoundEffect>("Sounds/Effects/player_jump_snow_01");
            this.landSFX = content.Load<SoundEffect>("Sounds/Effects/player_land_snow_01");
        }

        public void Update()
        {
            if (this.isWalking) 
                this.walkInstance.Play();
            else
                this.walkInstance.Stop();
        }

        public void HandleMovementSFX(bool onGround, bool isSprinting)
        {
            if(onGround) {
                if(isSprinting) {
                    this.walkInstance.Stop();
                    this.sprintInstance.Play();
                }
                else
                {
                    this.sprintInstance.Stop();
                    this.walkInstance.Play();
                }
            }
            else
            {
                this.walkInstance.Stop();
                this.sprintInstance.Stop();
            }
        }

        public void HandleDieSFX()
        {
            this.walkInstance.Stop();
            this.sprintInstance.Stop();
        }

        public void HandleJumpSFX()
        {
            this.jumpSFX.Play();
        }

        public void HandleLandSFX()
        {
            this.landSFX.Play();
        }

        public void HandleIdleSFX()
        {
            this.walkInstance.Stop();
            this.sprintInstance.Stop();
        }

        public SoundEffect Walking { set { this.walkSFX = value; } }
        public bool IsWalking { set { this.isWalking = value; } }
    }
}
