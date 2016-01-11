using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Controller
{
    public class GameController : Collection.MapCollection
    {
        private View.Camera camera;
        private Model.Player player;
        private PlayerController playerController;
        private Model.Level level;

        private const int MAX_LEVEL = 3;
        private int currentLevel;
        private bool gameOver = false;
        private bool gameWon = false;

        public GameController(View.Camera camera)
        {
            this.camera = camera;
        }

        public void StartNewGame(ContentManager content)
        {
            this.gameOver = false;
            this.gameWon = false;
            this.currentLevel = 1;
            this.player = new Model.Player(content, this.camera);
            this.playerController = new PlayerController(this.camera, this.player);
        }

        /*
         *  This method is once called when initializing the game
         *  and then during each GameState.PostLevel
         */
        public void LoadLevel(ContentManager content)
        {
            switch (this.currentLevel)
            {
                case 1:
                    this.level = new Model.Levels.Level1(content, this.camera, this.player, MAP_1, this.currentLevel);
                    break;
                case 2:
                    this.level = new Model.Levels.Level2(content, this.camera, this.player, MAP_2, this.currentLevel);
                    break;
                case 3:
                    this.level = new Model.Levels.Level3(content, this.camera, this.player, MAP_3, this.currentLevel);
                    break;
                default:
                    // TODO: Winning the game
                    break;
            }
        }

        /*
         *  This method is called during each GameState.PreLevel
         *  should unload all content only used for a certain level
         */
        public void UnloadContent()
        {

        }

        public void Update(float elapsedTime)
        {
            if (this.player.Lives <= 0)
                this.gameOver = true;

            if (!this.gameOver)
            {
                if (this.level != null && this.level.LevelState == Model.LevelState.Playing)
                {
                    if (this.player.CurrentPlayerState != Model.PlayerState.Dead)
                        this.playerController.Update(elapsedTime);

                    this.camera.FocusOnPlayer(elapsedTime, this.player.Position, this.player.FaceForward, this.camera.MapWidth, this.camera.MapHeight);

                    if (this.level.IsFinished())
                        this.level.LevelState = Model.LevelState.Finished;
                }

                else if (this.level.LevelState == Model.LevelState.Created && this.level.LoadTimer < 0)
                    this.level.LevelState = Model.LevelState.Playing;

                else if (this.level.LevelState == Model.LevelState.Finished && Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    this.currentLevel += 1;
                    if (this.currentLevel > MAX_LEVEL)
                    {
                        this.gameWon = true;
                        this.gameOver = true;
                    }
                }
                    
                if (this.level != null)
                    this.level.Update(elapsedTime);
            }
        }
        
        public void Draw(SpriteBatch sb, GameState state)
        {
            if (this.level != null && !this.gameOver)
                this.level.Draw(sb);
        }

        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        public bool GameWon { get { return this.gameWon; } }

        public Model.Player Player { get { return this.player; } }
        public Model.Level Level { get { return this.level; } }
        public int CurrentLevel { get { return this.currentLevel; } }
    }
}
