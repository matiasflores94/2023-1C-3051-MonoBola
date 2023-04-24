using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;
using TGC.MonoGame.TP.Geometries;

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
       
        private GraphicsDeviceManager Graphics { get; set; }
        private TargetCamera Camera { get; set; }
        private Effect Effect { get; set; }
        private Model BodyModel { get; set; }
        private Matrix BodyWorld { get; set; }
        private Model  TreeModel{ get; set; }
        private Matrix TreeWorld { get; set; }
        private Model  PathModel{ get; set; }
        private Matrix PathWorld { get; set; }
        private Model  BenchModel{ get; set; }
        private Matrix BenchWorld { get;set;}
        private Model  Tree2Model{ get; set; }
        private Matrix Tree2World { get; set; }
        private Model  Tree1Model{ get; set; }
        private Matrix Tree1World { get; set; }
        private Model  BridgeModel{ get; set; }
        private Matrix BridgeWorld { get; set; }
        private Model  DogModel{ get; set; }
        private Matrix DogWorld { get; set; }
        private Model GrassModel { get; set; }
        private Matrix GrassWorld { get; set; }
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
       // private float Yaw { get; set; }
        private float Pitch { get; set; }
        private float Roll { get; set; }
        private float elapsedTime { get; set; }

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
            Camera = new TargetCamera(GraphicsDevice.Viewport.AspectRatio, Vector3.One * 100f, Vector3.Zero);
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.
            //Creacion de Esfera
            Sphere = new SpherePrimitive(GraphicsDevice, DIAMETER, 16, Color.Gold);
            //Creacion de piso
            Quad = new QuadPrimitive(GraphicsDevice);
            QuadWall = new QuadPrimitive(GraphicsDevice);
            // Configuramos nuestras matrices de la escena.
            SpherePosition = new Vector3(0, 5, 0);
            SphereRotation = Matrix.Identity;

            BodyWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(20f,20f,20f);
            GrassWorld = Matrix.Identity;
            FloorWorld = Matrix.CreateScale(200f);
            WallWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(100f, 0f, 0f);
            TreeWorld = Matrix.Identity;
            DogWorld = Matrix.Identity;
            BridgeWorld = Matrix.Identity;
            Tree2World = Matrix.Identity;
            Tree1World = Matrix.Identity;
            StarWorld = Matrix.Identity;
            BenchWorld = Matrix.Identity;
            PathWorld = Matrix.Identity;
            SlideWorld = Matrix.Identity;
            BikeWorld = Matrix.Identity;

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
            GrassModel = Content.Load<Model>(ContentFolder3D + "grass/grass");
            TreeModel = Content.Load<Model>(ContentFolder3D + "tree/Tree");
            Tree1Model = Content.Load<Model>(ContentFolder3D + "tree2/Tree");
            DogModel = Content.Load<Model>(ContentFolder3D + "dog/GermanShephardLowPoly");
            Tree2Model = Content.Load<Model>(ContentFolder3D + "tree3/Lowpoly_tree_sample");
            BridgeModel = Content.Load<Model>(ContentFolder3D + "bridge/uploads_files_4132388_WoodBridge");
            StarModel = Content.Load<Model>(ContentFolder3D + "star/Gold_Star");
            BenchModel = Content.Load<Model>(ContentFolder3D + "bench/uploads_files_3982311_Bench");
            PathModel = Content.Load<Model>(ContentFolder3D + "path/cobblestone lowpoly");
            SlideModel = Content.Load<Model>(ContentFolder3D + "slide/Hill");
            BikeModel = Content.Load<Model>(ContentFolder3D + "bike/Bicicle");

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            foreach (var mesh in GrassModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            foreach (var mesh in BodyModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            foreach (var mesh in TreeModel.Meshes)
                {
                    foreach (var meshPart in mesh.MeshParts)
                    {
                        meshPart.Effect = Effect;
                    }
            }
            foreach (var mesh in Tree1Model.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            foreach (var mesh in DogModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            foreach (var mesh in Tree2Model.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
            foreach (var mesh in StarModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
           foreach (var mesh in BridgeModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }
           foreach (var mesh in PathModel.Meshes)
           {
               foreach (var meshPart in mesh.MeshParts)
               {
                   meshPart.Effect = Effect;
               }
           }
           foreach (var mesh in BenchModel.Meshes)
           {
               foreach (var meshPart in mesh.MeshParts)
               {
                   meshPart.Effect = Effect;
               }
           }
           foreach (var mesh in SlideModel.Meshes)
           {
               foreach (var meshPart in mesh.MeshParts)
               {
                   meshPart.Effect = Effect;
               }
           }
           foreach (var mesh in BikeModel.Meshes)
           {
               foreach (var meshPart in mesh.MeshParts)
               {
                   meshPart.Effect = Effect;
               }
           }
           
            base.LoadContent();
        }

        private void UpdateCamera()//Sacado de Samples.ThirdPersonPlatformer
        {
            // Create a position that orbits the Robot by its direction (Rotation)

            // Create a normalized vector that points to the back of the Robot
            var robotBackDirection = Vector3.Transform(Vector3.Backward, SphereRotation);
            // Then scale the vector by a radius, to set an horizontal distance between the Camera and the Robot
            var orbitalPosition = robotBackDirection * CameraFollowRadius;


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
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                SpherePosition += Vector3.Forward * SPEED * elapsedTime;
                Pitch += elapsedTime * SPEED / 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                SpherePosition += Vector3.Backward * SPEED * elapsedTime;
                Pitch -= elapsedTime * SPEED / 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SpherePosition += Vector3.Left * SPEED * elapsedTime;
                Roll += elapsedTime * SPEED / 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SpherePosition += Vector3.Right * SPEED * elapsedTime;
                Roll -= elapsedTime * SPEED / 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                SpherePosition += Vector3.Up * SPEED * elapsedTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                SpherePosition += Vector3.Down * SPEED * elapsedTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                SpherePosition = new Vector3(SpherePosition.X, DIAMETER / 2, SpherePosition.Z);
            }


            UpdateCamera();
            
            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            GraphicsDevice.Clear(Color.Black);

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.DarkGreen.ToVector3());
            
            for (float i = 0; i < 10;i++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.06f) * Matrix.CreateTranslation(i*-16f,0,0));
                    mesh.Draw();
                }
            }
            for (float i = 0; i < 10;i++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.06f) * Matrix.CreateTranslation(0,0,i*16f));
                    mesh.Draw();
                }
            }

            for (float i=0; i<5; i++){
                foreach (var mesh in BodyModel.Meshes)
                {
                    BodyWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(BodyWorld * Matrix.CreateScale(0.03f) * Matrix.CreateTranslation(0,6f,i*-60f));
                    mesh.Draw();
                }
            }
            
            for (float i=0; i<10; i++){
                foreach (var mesh in TreeModel.Meshes)
                {
                    TreeWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.08f) * Matrix.CreateTranslation(0f,0f,i*10f+40f));
                    mesh.Draw();
                }
            }

            for (float i=0; i<10; i++){
                foreach (var mesh in Tree1Model.Meshes)
                {
                    Tree1World = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(Tree1World * Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(25f,0f,i*10f+40f));
                    mesh.Draw();
                }
            }
            
            for (float i=0; i<5; i++){
                foreach (var mesh in DogModel.Meshes)
                {
                    DogWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(DogWorld * Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(20f,6f,i*-60+15f));
                    mesh.Draw();
                }
            }

            foreach (var mesh in Tree2Model.Meshes)
            {
                Tree2World = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(Tree2World * Matrix.CreateScale(20f) * Matrix.CreateTranslation(-50f,6f,60f));
                mesh.Draw();
            }

            for (float i=0; i<5; i++){
                foreach (var mesh in StarModel.Meshes)
                {
                    StarWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(StarWorld * Matrix.CreateScale(0.08f) *Matrix.CreateRotationY(MathHelper.PiOver2)* Matrix.CreateTranslation(40f,6f,i*10f+60f));
                    mesh.Draw();
                }
            }

            foreach (var mesh in BridgeModel.Meshes)
            {
                BridgeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BridgeWorld * Matrix.CreateScale(5f) * Matrix.CreateRotationX(-1f*MathHelper.PiOver2) * Matrix.CreateTranslation(35f,0f,20f));
                mesh.Draw();
            }

            for (float i=0; i<10; i++){
                foreach (var mesh in BenchModel.Meshes)
                {
                    BenchWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(BenchWorld * Matrix.CreateScale(0.25f) * Matrix.CreateRotationY(0.85f*MathHelper.PiOver4) * Matrix.CreateTranslation(110f,-12f,i*-60f+0f));
                    mesh.Draw();
                }
            }

            for (float i=0; i<10; i++){
                foreach (var mesh in PathModel.Meshes)
                {
                    PathWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.08f)  * Matrix.CreateTranslation(60f,0f,i*-70f));
                    mesh.Draw();
                }
            }

            foreach (var mesh in SlideModel.Meshes)
            {
                SlideWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(SlideWorld * Matrix.CreateScale(0.10f)  * Matrix.CreateTranslation(50f,0f,50f));
                mesh.Draw();
            }

            foreach (var mesh in BikeModel.Meshes)
            {
                BikeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BikeWorld * Matrix.CreateScale(3f)  * Matrix.CreateTranslation(-50f,5f,0f));
                mesh.Draw();
            }

            DrawGeometry(Sphere, SpherePosition, 0f, Pitch, Roll);
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
            var effect = geometry.Effect;

            effect.World = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateTranslation(position);
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;

            geometry.Draw(effect);
        }

    }
}