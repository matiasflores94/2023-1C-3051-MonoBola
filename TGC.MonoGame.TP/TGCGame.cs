using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Extras;

namespace TGC.MonoGame.TP
{
    /// <summary>
    ///     Esta es la clase principal del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer mas ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);
            // Para que el juego sea pantalla completa se puede usar Graphics IsFullScreen.
            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = true;
        }

        private const float SPEED = 50f;
        private const float DIAMETER = 10f;
        private const float CameraFollowRadius = 100f;
        private const float CameraUpDistance = 30f;
        private const float SphereRotatingVelocity = 0.05f;
        private const float MouseSensitivity = 5f;

        private GraphicsDeviceManager Graphics { get; set; }
        private TargetCamera Camera { get; set; }
        private Effect Effect { get; set; }
        private Effect BallEffect { get; set; }
        private Model SphereModel { get; set; }

        private Model BodyModel { get; set; }
        private Matrix BodyWorld { get; set; }
        private Model BallModel { get; set; }
        private Matrix BallWorld { get; set; }
        private Model BoyModel { get; set; }
        private Matrix BoyWorld { get; set; }
        private Model TreeModel { get; set; }
        private Matrix TreeWorld { get; set; }
        private Model PathModel { get; set; }
        private Matrix PathWorld { get; set; }
        private Model MonumentModel { get; set; }
        private Matrix MonumentWorld { get; set; }
        private Model BenchModel { get; set; }
        private Matrix BenchWorld { get; set; }
        private Model Tree2Model { get; set; }
        private Matrix Tree2World { get; set; }
        private Model Tree1Model { get; set; }
        private Matrix Tree1World { get; set; }
        private Model BridgeModel { get; set; }
        private Matrix BridgeWorld { get; set; }
        private Model DogModel { get; set; }
        private Matrix DogWorld { get; set; }
        private Model GrassModel { get; set; }
        private Matrix GrassWorld { get; set; }
        private Model LampModel { get; set; }
        private Matrix LampWorld { get; set; }

        private Model StarModel { get; set; }
        private Matrix StarWorld { get; set; }
        private Matrix FloorWorld { get; set; }
        private Model SlideModel { get; set; }
        private Matrix SlideWorld { get; set; }
        private Model BikeModel { get; set; }
        private Matrix BikeWorld { get; set; }
        private QuadPrimitive Quad { get; set; }
        private Matrix WallWorld { get; set; }
        private QuadPrimitive QuadWall { get; set; }
        private SpherePrimitive Sphere { get; set; }
        private Matrix SphereRotation { get; set; }
        private Vector3 SpherePosition { get; set; }
        private Vector3 SphereFrontDirection { get; set; }
        private float Yaw { get; set; }
        private float Pitch { get; set; }
        private float Roll { get; set; }
        private float elapsedTime { get; set; }
        private float pastMousePositionY;

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void Initialize()
        {

            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            Camera = new TargetCamera(GraphicsDevice.Viewport.AspectRatio, Vector3.One * 100f, SpherePosition);
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.
            //Creacion de Esfera
            Sphere = new SpherePrimitive(GraphicsDevice, DIAMETER, 16, Color.Gold);
            //Creacion de piso
            Quad = new QuadPrimitive(GraphicsDevice);
            QuadWall = new QuadPrimitive(GraphicsDevice);
            // Configuramos nuestras matrices de la escena.
            SpherePosition = new Vector3(400, 15, 200);
            SphereRotation = Matrix.Identity;
            SphereFrontDirection = Vector3.Forward;
            BodyWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(20f, 20f, 20f);
            BoyWorld = Matrix.Identity;
            BallWorld = Matrix.Identity;
            GrassWorld = Matrix.Identity;
            FloorWorld = Matrix.CreateScale(200f);
            WallWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(100f, 0f, 0f);
            TreeWorld = Matrix.Identity;
            MonumentWorld = Matrix.Identity;
            DogWorld = Matrix.Identity;
            BridgeWorld = Matrix.Identity;
            Tree2World = Matrix.Identity;
            Tree1World = Matrix.Identity;
            StarWorld = Matrix.Identity;
            BenchWorld = Matrix.Identity;
            PathWorld = Matrix.Identity;
            SlideWorld = Matrix.Identity;
            LampWorld = Matrix.Identity;

            BikeWorld = Matrix.Identity;
            pastMousePositionY = Mouse.GetState().Position.ToVector2().Y;
            IsMouseVisible = false;
            Mouse.SetPosition(GraphicsDevice.Viewport.Bounds.Height/2,GraphicsDevice.Viewport.Bounds.Width/2);

            base.Initialize();
        }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void LoadContent()
        {
            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            BodyModel = Content.Load<Model>(ContentFolder3D + "body/First");
            BoyModel = Content.Load<Model>(ContentFolder3D + "boy/uploads_files_2017656_body_1");
            BallModel = Content.Load<Model>(ContentFolder3D + "ball/uploads_files_910532_soccer-ball");
            GrassModel = Content.Load<Model>(ContentFolder3D + "grass/grass");
            TreeModel = Content.Load<Model>(ContentFolder3D + "tree/Tree");
            Tree1Model = Content.Load<Model>(ContentFolder3D + "tree2/Tree");
            DogModel = Content.Load<Model>(ContentFolder3D + "dog/GermanShephardLowPoly");
            Tree2Model = Content.Load<Model>(ContentFolder3D + "tree3/Lowpoly_tree_sample");
            BridgeModel = Content.Load<Model>(ContentFolder3D + "bridge/uploads_files_4132388_WoodBridge");
            MonumentModel = Content.Load<Model>(ContentFolder3D + "monument/uploads_files_2609414_Temple_F");
            StarModel = Content.Load<Model>(ContentFolder3D + "star/Gold_Star");
            BenchModel = Content.Load<Model>(ContentFolder3D + "bench/uploads_files_3982311_Bench");
            PathModel = Content.Load<Model>(ContentFolder3D + "path/cobblestone lowpoly");
            SlideModel = Content.Load<Model>(ContentFolder3D + "slide/Hill");
            BikeModel = Content.Load<Model>(ContentFolder3D + "bike/Bicicle");
            LampModel = Content.Load<Model>(ContentFolder3D + "lamp/streetlamp");

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            BallEffect = Content.Load<Effect>(ContentFolderEffects + "BallShader");

            /*  EffectBrown = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
              EffectPink = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
              EffectPink = Content.Load<Effect>(ContentFolderEffects + "BasicShader");*/
            FuncionesGenerales.loadEffectOnMesh(GrassModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BodyModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(LampModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(MonumentModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BoyModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BallModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(TreeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(Tree1Model, Effect);
            FuncionesGenerales.loadEffectOnMesh(Tree2Model, Effect);
            FuncionesGenerales.loadEffectOnMesh(DogModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(StarModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BridgeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BikeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BallModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(PathModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(SlideModel, Effect);





            base.LoadContent();
        }

        private void UpdateCamera() //Sacado de Samples.ThirdPersonPlatformer
        {
            // Create a position that orbits the Robot by its direction (Rotation)

            // Create a normalized vector that points to the back of the Robot
            var sphereBackDirection = Vector3.Transform(Vector3.Backward, SphereRotation);
            // Then scale the vector by a radius, to set an horizontal distance between the Camera and the Robot
            var orbitalPosition = sphereBackDirection * CameraFollowRadius;


            // We will move the Camera in the Y axis by a given distance, relative to the Robot
            var upDistance = Vector3.Up * CameraUpDistance;

            // Calculate the new Camera Position by using the Robot Position, then adding the vector orbitalPosition that sends 
            // the camera further in the back of the Robot, and then we move it up by a given distance
            Camera.Position = SpherePosition + orbitalPosition + upDistance;

            // Set the Target as the Robot, the Camera needs to be always pointing to it
            Camera.TargetPosition = SpherePosition;

            // Build the View matrix from the Position and TargetPosition
            Camera.BuildView();
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la logica de computo del modelo, asi como tambien verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logica de actualizacion del juego.
            elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //Salgo del juego.
                Exit();
            }

            var currentSpeed = SPEED;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                currentSpeed *= 5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                SpherePosition += SphereFrontDirection * currentSpeed * elapsedTime;
                Pitch += elapsedTime * currentSpeed / 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                SpherePosition -= SphereFrontDirection * currentSpeed * elapsedTime;
                Pitch -= elapsedTime * currentSpeed / 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SpherePosition -= Vector3.Cross(SphereFrontDirection, Vector3.Up) * currentSpeed * elapsedTime;
                Roll += elapsedTime * currentSpeed / 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SpherePosition += Vector3.Cross(SphereFrontDirection, Vector3.Up) * currentSpeed * elapsedTime;
                Roll -= elapsedTime * currentSpeed / 2;
            }
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                SpherePosition += Vector3.Up * currentSpeed * elapsedTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                SpherePosition += Vector3.Down * currentSpeed * elapsedTime;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                SpherePosition = new Vector3(SpherePosition.X, 15, SpherePosition.Z);
            }

            moverCamaraMouse();
            UpdateCamera();

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        private Matrix SphereWorld = Matrix.Identity;
        
        protected override void Draw(GameTime gameTime)
        {

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var Alto = GraphicsDevice.Viewport.Bounds.Height;
            var Ancho = GraphicsDevice.Viewport.Bounds.Width;

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.

            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            BallEffect.Parameters["View"].SetValue(Camera.View);
            BallEffect.Parameters["Projection"].SetValue(Camera.Projection);
            DrawGeometry(Sphere, SpherePosition, 0f, Pitch, Roll);

            generateLevel();
          //  FuncionesGenerales.drawMesh(SphereModel,SphereWorld*Matrix.CreateTranslation(SpherePosition),BallEffect);
    

        }

        /// <summary>
        ///     Libero los recursos que se cargaron en el juego.
        /// </summary>
        protected override void UnloadContent()
        {
            // Libero los recursos.
            Content.Unload();

            base.UnloadContent();
        }

        private void DrawGeometry(GeometricPrimitive geometry, Vector3 position, float yaw, float pitch, float roll)
        {
            var effect = BallEffect;

            effect.Parameters["World"].SetValue(Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateTranslation(position));
           /* effect.View = Camera.View;
            effect.Projection = Camera.Projection;
*/
            geometry.Draw(effect);
        }

    private void generateLevel()
    {
        Effect.Parameters["DiffuseColor"].SetValue(Color.DarkGreen.ToVector3());

        for (float i = 0; i < 20; i++)
        {
            for (float j = 0; j < 20; j++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.4f) *
                                                        Matrix.CreateTranslation(i * 200f - 2000f, 0, j * -200f));
                    mesh.Draw();
                }
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.DarkGray.ToVector3());

        foreach (var mesh in BikeModel.Meshes)
        {
            BikeWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(BikeWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                Matrix.CreateScale(30f) * Matrix.CreateTranslation(700f, 50f, 0f));
            mesh.Draw();
        }

        for (float i = 0; i < 5; i++)
        {
            foreach (var mesh in PathModel.Meshes)
            {
                PathWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(400f, 5f, i * -200f));
                mesh.Draw();

            }
        }

        for (float i = 0; i < 5; i++)
        {
            foreach (var mesh in PathModel.Meshes)
            {
                PathWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(-i * 200f + 400f, 5f, -2000));
                mesh.Draw();

            }
        }

        for (float i = 0; i < 3; i++)
        {
            foreach (var mesh in PathModel.Meshes)
            {
                PathWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(400f, 5f, i * -200f - 1750f));
                mesh.Draw();

            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.Silver.ToVector3());
        foreach (var mesh in MonumentModel.Meshes)
        {
            MonumentWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(MonumentWorld * Matrix.CreateScale(8f) * Matrix.CreateTranslation(400f, 6f, -2500f));
            mesh.Draw();
        }


        foreach (var mesh in SlideModel.Meshes)
        {
            SlideWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(SlideWorld * Matrix.CreateScale(0.8f) *
                                                Matrix.CreateRotationY(-1 * MathHelper.PiOver2) *
                                                Matrix.CreateTranslation(-700f, 6f, -2000f));
            mesh.Draw();
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector3());
        for (float j = 0; j < 10; j++)
        {
            foreach (var mesh in BallModel.Meshes)
            {
                BallWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BallWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(400f, 12f, 0f + j * -100f));
                mesh.Draw();
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.LightPink.ToVector3());

        foreach (var mesh in BodyModel.Meshes)
        {
            BodyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(BodyWorld * Matrix.CreateScale(0.6f) *
                                                Matrix.CreateRotationY(MathHelper.Pi) *
                                                Matrix.CreateTranslation(200f, 150f, 150f));
            mesh.Draw();
        }

        foreach (var mesh in BodyModel.Meshes)
        {
            BodyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(BodyWorld * Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(200f, 150f, -100f));
            mesh.Draw();
        }

        foreach (var mesh in BoyModel.Meshes)
        {
            BoyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(BoyWorld * Matrix.CreateScale(10f) * Matrix.CreateTranslation(200f, 8f, -500f));
            mesh.Draw();
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.LimeGreen.ToVector3());

        for (float i = 0; i < 25; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.008f) *
                                                    Matrix.CreateTranslation(300f, 8f, i * -40f + 100f));
                mesh.Draw();
            }
        }

        for (float i = 0; i < 25; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                Tree1World = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(Tree1World * Matrix.CreateScale(0.008f) *
                                                    Matrix.CreateTranslation(510f, 8f, i * -40f + 100f));
                mesh.Draw();
            }
        }

        for (float i = 0; i < 7; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.1f) *
                                                    Matrix.CreateTranslation(-300f, 8f, i * -300f + 100f));
                mesh.Draw();
            }
        }

        for (float i = 0; i < 25; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.1f) *
                                                    Matrix.CreateTranslation(1000, 8f, i * -200f + 100f));
                mesh.Draw();
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.Peru.ToVector3());

        foreach (var mesh in DogModel.Meshes)
        {
            DogWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(DogWorld * Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(700f, 6f, -450f));
            mesh.Draw();
        }

        foreach (var mesh in BridgeModel.Meshes)
        {
            BridgeWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(BridgeWorld * Matrix.CreateScale(100f) *
                                                Matrix.CreateRotationX(-1f * MathHelper.PiOver2) *
                                                Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                Matrix.CreateTranslation(400f, 0f, -1300f));
            mesh.Draw();
        }

        for (float i = 0; i < 3; i++)
        {
            foreach (var mesh in BenchModel.Meshes)
            {
                BenchWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BenchWorld * Matrix.CreateScale(2f) *
                                                    Matrix.CreateRotationY(-1f * MathHelper.Pi) *
                                                    Matrix.CreateTranslation(-100f, -100f, i * -320 - 320f));
                mesh.Draw();
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.Yellow.ToVector3());

        foreach (var mesh in StarModel.Meshes)
        {
            StarWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(StarWorld * Matrix.CreateScale(1f) *
                                                Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                Matrix.CreateTranslation(400f, 20f, -100f));
            mesh.Draw();
        }

        for (float i = 0; i < 10; i++)
        {
            for (float j = 0; j < 4; j++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.4f) *
                                                        Matrix.CreateTranslation(-i * 200f - 700f, 2f,
                                                            j * -200f - 1750f));
                    mesh.Draw();
                }
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.Blue.ToVector3());

        for (float i = 0; i < 5; i++)
        {
            for (float j = 0; j < 3; j++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.4f) *
                                                        Matrix.CreateTranslation(i * 200f, 0, j * -200f - 1000f));
                    mesh.Draw();
                }
            }
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.Black.ToVector3());

        foreach (var mesh in LampModel.Meshes)
        {
            LampWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(LampWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(700f, 6f, -1900f));
            mesh.Draw();
        }
    }

    private void moverCamaraMouse()
    {
        SphereRotation = Matrix.CreateRotationY(Mouse.GetState().Position.ToVector2().X*-0.1f);
        SphereFrontDirection = Vector3.Transform(Vector3.Forward, SphereRotation);
    }
    }

}
