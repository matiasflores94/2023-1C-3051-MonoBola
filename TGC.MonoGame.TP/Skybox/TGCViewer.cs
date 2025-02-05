/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Viewer.Models;

namespace TGC.MonoGame.TP.SkyBox
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class TGCViewer : Game
    {
        /// <summary>
        ///     The folder which the game will search for content.
        /// </summary>
        public const string ContentFolder = "Content";


        /// <summary>
        ///     Initializes a new instance of the <see cref="TGCViewer" /> class.
        ///     The main game constructor is used to initialize the starting variables.
        /// </summary>
        public TGCViewer()
        {
            Graphics = new GraphicsDeviceManager(this);
            //Graphics.IsFullScreen = true;
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            // Commented because of https://github.com/MonoGame/MonoGame/issues/7914
            //Graphics.PreferMultiSampling = true;
            Content.RootDirectory = ContentFolder;
            IsMouseVisible = true;
            Gizmos = new Gizmos.Gizmos();
            Model = new TGCViewerModel(this);
            Model.LoadTreeSamples();
        }

        /// <summary>
        ///     Sample background color.
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        ///     Represents a state of keystrokes recorded by a keyboard input device.
        /// </summary>
        public KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        ///     Represents the state of the Mouse input device.
        /// </summary>
        public MouseState CurrentMouseState { get; set; }

        /// <summary>
        ///     Handles the configuration and management of the graphics device.
        /// </summary>
        public GraphicsDeviceManager Graphics { get; }

        /// <summary>
        ///     The model has the logic for the creation of the sample explorer.
        /// </summary>
        private TGCViewerModel Model { get; }

        /// <summary>
        ///     Gizmos are used to debug and visualize boundaries and vectors
        /// </summary>
        public Gizmos.Gizmos Gizmos { get; }

        /// <summary>
        ///     Enables a group of sprites to be drawn using the same settings.
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }
        
        /// <summary>
        ///     This method is called after the constructor, but before the main game loop (Update/Draw).
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where you can query any required services and load any non-graphic related content.
        ///     Calling base.Initialize will enumerate through any components and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Model.LoadImgGUI();
            Model.LoadWelcomeSample();
            Background = Color.CornflowerBlue;
            base.Initialize();
        }

        /// <summary>
        ///     This method is used to load your game content.
        ///     It is called only once per game, after Initialize method, but before the main game loop methods.
        /// </summary>
        protected override void LoadContent()
        {
            Gizmos.LoadContent(GraphicsDevice, new ContentManager(Content.ServiceProvider, ContentFolder));

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        ///     This method is called multiple times per second, and is used to update your game state (updating the world,
        ///     checking for collisions, gathering input, playing audio, etc.).
        /// </summary>
        /// <param name="gameTime">Holds the time state of a <see cref="Game" />.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        ///     Similar to the Update method, it is also called multiple times per second.
        /// </summary>
        /// <param name="gameTime">Holds the time state of a <see cref="Game" />.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Background);
            base.Draw(gameTime);
            Model.DrawSampleExplorer(gameTime);
            Gizmos.Draw();
        }

        /// <summary>
        ///     Unload the resources loaded by the game.
        /// </summary>
        protected override void UnloadContent()
        {
            Gizmos.Dispose();
            Model.Dispose();
            Content.Unload();
        }

        /// <summary>
        ///     Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            // Check for exit.
            if (CurrentKeyboardState.IsKeyDown(Keys.Escape)) Exit();
        }
    }
}*/