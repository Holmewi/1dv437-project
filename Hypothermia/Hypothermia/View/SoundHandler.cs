using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class SoundHandler
    {
        private Song ambient;
        private bool ambientPlaying = false;

        public SoundHandler()
        {

        }

        public void AddAmbient(Song ambient)
        {
            this.ambient = ambient;
            MediaPlayer.IsRepeating = true;
        }

        public void Update(Controller.GameState state)
        {
            switch (state)
            {
                case Controller.GameState.Playing:
                    if (!this.ambientPlaying)
                    {
                        MediaPlayer.Play(this.ambient);
                        this.ambientPlaying = true;
                    }
                    MediaPlayer.Resume();
                    break;
                case Controller.GameState.Paused:
                    MediaPlayer.Pause();
                    break;
                case Controller.GameState.GameOver:
                    MediaPlayer.Stop();
                    this.ambientPlaying = false;
                    break;
            }
        }
    }
}
