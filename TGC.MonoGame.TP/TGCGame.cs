using System;
using System.Diagnostics;
using BepuPhysics.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using BepuPhysics.CollisionDetection.CollisionTasks;
using BepuUtilities.Memory;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.Samples.Geometries.Textures;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Extras;
using TGC.MonoGame.TP.SkyBoxSpace;

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
        private const float MouseSensitivity = -0.005f;
        private const float CheckPointHeight = 5000;
        private const float CheckPointRadius = 80;
        private bool alive;
        private SpriteBatch spriteBatch { get; set; }

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
        
        private Model BirdModel { get; set; }
        private Matrix BirdWorld { get; set; }

        private Matrix StarWorld { get; set; }
        private Matrix FloorWorld { get; set; }
        private Model SlideModel { get; set; }
        private Matrix SlideWorld { get; set; }
        private Model BikeModel { get; set; }
        private Model ArcoModel { get; set; }

        private Matrix ArcoWorld { get; set; }
        private Boolean god  { get; set; }
        private Matrix BikeWorld { get; set; }
        private QuadPrimitive Quad { get; set; }
        private Matrix WallWorld { get; set; }
        private QuadPrimitive QuadWall { get; set; }
        private SpherePrimitive Sphere { get; set; }
        private SpherePrimitive Test { get; set; }

        private CylinderPrimitive Cylinder { get; set; }
        private Matrix SphereRotation { get; set; }
        private Matrix SphereWorld { get; set; }

        private Vector3 SpherePosition { get; set; }
        private Vector3 Checkpoint1Position { get; set; }
        private BoundingCylinder CheckPoint1Collide { get; set; }
        private BoundingCylinder CheckPoint2Collide { get; set; }
        private BoundingSphere SphereCollide { get; set; }
        private BoundingSphere StarCollide { get; set; }

        private BoundingSphere BallCollide1{ get; set; }
        private BoundingSphere BallCollide2{ get; set; }
        private BoundingSphere BallCollide3{ get; set; }

        private OrientedBoundingBox FinishCollide { get; set; }
        private OrientedBoundingBox Pared1 { get; set; }
        private OrientedBoundingBox AguaCollide { get; set; }

        private OrientedBoundingBox Pared2 { get; set; }
        private OrientedBoundingBox Pared3 { get; set; }
        private OrientedBoundingBox Pared4 { get; set; }
      
        private OrientedBoundingBox SlideCollide { get; set; }

        private Vector3 Checkpoint2Position { get; set; }
        private Vector3 FinishPosition { get; set; }
        private int Checkpoint1;
        private int Checkpoint2;
        private bool isOnGround;
        private Boolean Finsh{ get; set; }
        private Vector3 BirdPosition { get; set; }
        private BoundingSphere BridgeCollision { get; set; }
        private BoundingSphere BDCollide { get; set; }

        private Vector3 SphereFrontDirection { get; set; }
        
        private float Yaw { get; set; }
        private float Pitch { get; set; }
        private float Roll { get; set; }
        private float elapsedTime { get; set; }
        private float pastMousePositionY;
        private SpriteFont font;
        public OrientedBoundingBox ArenaCollide { get; set; }


        private Effect SphereEffect { get; set; }
        private Texture2D RockTexture { get; set; }
        private TextureCube SkyBoxTexture { get; set; }
        private Model SkyBoxModel { get; set; }
        private Effect SkyBoxEffect { get; set; }
        private SkyBox SkyBox { get; set; }

        private Texture2D MetalTexture { get; set; }
        private Texture2D RubberTexture { get; set; }
        private Texture2D BallTexture { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        private Vector3 BridgePosition;
        protected override void Initialize()
        {
          
            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            
             Graphics.PreferredBackBufferWidth =
                 GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
             Graphics.PreferredBackBufferHeight =
                 GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
               Graphics.ApplyChanges();
            Camera = new TargetCamera(GraphicsDevice.Viewport.AspectRatio, Vector3.One * 100f, SpherePosition);
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.
            //Creacion de Esfera
            Sphere = new SpherePrimitive(GraphicsDevice, DIAMETER, 16, Color.Gold);
            Cylinder = new CylinderPrimitive(GraphicsDevice, CheckPointHeight, CheckPointRadius, 32);
            

            // Configuramos nuestras matrices de la escena.
            var Alto = GraphicsDevice.Viewport.Bounds.Height;
            var Ancho = GraphicsDevice.Viewport.Bounds.Width;
            SpherePosition = new Vector3(350f, 15, 200);
            SphereRotation = Matrix.Identity;
            SphereFrontDirection = Vector3.Forward;
            BodyWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(20f, 20f, 20f);
            BoyWorld = Matrix.Identity;
            BallWorld = Matrix.Identity;
            GrassWorld = Matrix.Identity;
            FloorWorld = Matrix.CreateScale(200f);
            WallWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(Ancho/8f, 0f, 0f);
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
            BirdPosition = new Vector3(-1000f, 150f, -1500f);
            Checkpoint1 = 0;
            Checkpoint2 = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            god = false;
            Checkpoint1Position = new Vector3(400, 0, -2000);
            Checkpoint2Position = new Vector3(-1680, 0, -2000);
            CheckPoint1Collide = new BoundingCylinder(Checkpoint1Position,  CheckPointRadius,CheckPointHeight/2);
            CheckPoint2Collide = new BoundingCylinder(Checkpoint2Position,CheckPointRadius,CheckPointHeight/2);
            FinishPosition = new Vector3(-3200, 12f, -2000f);
            FinishCollide = new OrientedBoundingBox(FinishPosition, new Vector3(200,200,200));
            Pared1 = new OrientedBoundingBox(new Vector3(280,500,-750), new Vector3(1,500,1000));
            Pared2 = new OrientedBoundingBox(new Vector3(520,500,-950), new Vector3(1,500,1200));
            Pared3 = new OrientedBoundingBox(new Vector3(-1445,500,-1760), new Vector3(1740,2000,1));
            Pared4 = new OrientedBoundingBox(new Vector3(-1327.5f,500,-2240), new Vector3(1900,2000,1));
            AguaCollide = new OrientedBoundingBox(new Vector3(500,0,-1150), new Vector3(500,7,300));
            ArenaCollide = new OrientedBoundingBox(new Vector3(-940, 4, -2550), new Vector3(500, 1 ,800));
            SlideCollide = new OrientedBoundingBox(new Vector3(-450, 20, -2000), new Vector3(80, 190 ,100));
            
            SlideCollide.Rotate(Matrix.CreateRotationX(MathHelper.PiOver2));
            /*Pared7 = new BoundingBox(FinishPosition, new Vector3(200,200,200));
            Pared8 = new BoundingBox(FinishPosition, new Vector3(200,200,200));*/
            StarCollide = new BoundingSphere(new Vector3(400f, 5f, -100f), 18f);
            SphereCollide = new BoundingSphere(SpherePosition, DIAMETER/2);
            BallCollide1 = new BoundingSphere(new Vector3(400f, 12f, 0f + -200f), (DIAMETER/2)-1);
            BallCollide2 = new BoundingSphere(new Vector3(350f, 12f, 0f + -400f), (DIAMETER/2)-1);
            BallCollide3 = new BoundingSphere(new Vector3(450f, 12f, 0f + -600f), (DIAMETER/2)-1);
            BridgePosition = new Vector3(400f, -230f, -1300f);
            BridgeCollision = new BoundingSphere(BridgePosition, 320f);
            
            Test = new SpherePrimitive(GraphicsDevice, 320f*2f, 16, Color.Silver);


            BikeWorld = Matrix.Identity;
            pastMousePositionY = Mouse.GetState().Position.ToVector2().Y;
            IsMouseVisible = false;
            Mouse.SetPosition(GraphicsDevice.Viewport.Bounds.Height/2,GraphicsDevice.Viewport.Bounds.Width/2);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
          
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
            SlideModel = Content.Load<Model>(ContentFolder3D + "slide/ramp");
            BikeModel = Content.Load<Model>(ContentFolder3D + "bike/Bicicle");
            LampModel = Content.Load<Model>(ContentFolder3D + "lamp/streetlamp");
            BirdModel = Content.Load<Model>(ContentFolder3D + "bird/bird");
            ArcoModel = Content.Load<Model>(ContentFolder3D + "arco/Soccergoal");
            SphereModel = Content.Load<Model>(ContentFolder3D + "ball/ball");
            SkyBoxModel = Content.Load<Model>(ContentFolder3D + "skybox/cube");

            font = Content.Load<SpriteFont>("Font/File");
            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            BallEffect = Content.Load<Effect>(ContentFolderEffects + "BallShader");
            SkyBoxEffect = Content.Load<Effect>(ContentFolderEffects + "SkyBox");

            FuncionesGenerales.loadEffectOnMesh(GrassModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BodyModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(LampModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(MonumentModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BoyModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(TreeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(Tree1Model, Effect);
            FuncionesGenerales.loadEffectOnMesh(Tree2Model, Effect);
            FuncionesGenerales.loadEffectOnMesh(DogModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(StarModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BridgeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BikeModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(PathModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(SlideModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BirdModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(ArcoModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(BallModel, Effect);
            FuncionesGenerales.loadEffectOnMesh(SphereModel, BallEffect);
            //BridgeModel.Meshes.Get.GetVerticesAndIndicesFromModel.
            //ConvexHullHelper.CreateShape(BridgeModel.Bones., ,,);
            SkyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "skybox");

            RockTexture = Content.Load<Texture2D>(ContentFolderTextures + "esfera-piedra");
            MetalTexture = Content.Load<Texture2D>(ContentFolderTextures + "esfera-metal");
            RubberTexture = Content.Load<Texture2D>(ContentFolderTextures + "esfera-goma");
            SkyBox = new SkyBox(SkyBoxModel,SkyBoxTexture,SkyBoxEffect);
            base.LoadContent();
        }
        private float Angle { get; set; }
        private Vector3 CameraPosition { get; set; }
        private Vector3 CameraTarget { get; set; }
        private float Distance { get; set; }
        private Matrix View { get; set; }
        private Vector3 ViewVector { get; set; }
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

        float currentSpeed = SPEED;
        private Vector3 Velocity = Vector3.Zero;
        private Vector3 Acceleration = Vector3.Zero;
        private const float HORIZONTAL_ACC = 300f;
        private const float GRAVITY = -200.0f;
        private const float FRICTION = 0.99995f;
        private float JumpSpeed = 100f;
        private bool drawStar = true;
        protected override void Update(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logica de actualizacion del juego.
            chequearPropiedadesTextura(BallEffect.Parameters["ModelTexture"]?.GetValueTexture2D());

            elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //Salgo del juego.
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                god = !god;
            }

            isOnGround = MathF.Abs(SpherePosition.Y) <= 10f /*float.Epsilon*/;
            if (BridgeCollision.Intersects(SphereCollide) || SlideCollide.Intersects(SphereCollide) )
            {
                isOnGround = true;
            }


            if (isOnGround)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    /*Acceleration = Vector3.Cross(SphereFrontDirection, Vector3.Up) * HORIZONTAL_ACC;
                    Roll += elapsedTime * currentSpeed / 2;*/
                    SpherePosition -= Vector3.Cross(SphereFrontDirection, Vector3.Up) * currentSpeed * elapsedTime;
                    Roll += elapsedTime * currentSpeed / 2;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    /*Acceleration = -Vector3.Cross(SphereFrontDirection, Vector3.Up) * HORIZONTAL_ACC;
                    Roll -= elapsedTime * currentSpeed / 2;*/
                    SpherePosition += Vector3.Cross(SphereFrontDirection, Vector3.Up) * currentSpeed * elapsedTime;
                    Roll -= elapsedTime * currentSpeed / 2;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Acceleration = SphereFrontDirection * HORIZONTAL_ACC;
                    Pitch += elapsedTime * currentSpeed / 2;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    Acceleration = -SphereFrontDirection * HORIZONTAL_ACC;
                    Pitch -= elapsedTime * currentSpeed / 2;
                }
                else
                {
                    var HorizontalVelocity = -Velocity;
                    HorizontalVelocity.Y = 0f;
                    Acceleration = -Velocity * FRICTION;
                }



            }
            else
            {
                Acceleration = Vector3.Zero;
            }

            if (!BridgeCollision.Intersects(SphereCollide) || !SlideCollide.Intersects(SphereCollide)){
                Acceleration.Y = GRAVITY;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && isOnGround){
                Velocity.Y += JumpSpeed;
            }

            if (StarCollide.Intersects(SphereCollide))
            {
                Velocity = new Vector3 (Velocity.X * 1.2f, Velocity.Y, Velocity.Z * 1.2f);
                drawStar = false;
            }

            Velocity += Acceleration * elapsedTime;

            SpherePosition += Velocity * elapsedTime;

           
            if(SpherePosition.Y <= 10f){
                Velocity.Y = 0f;
                //no se por qué tira error de que no se puede asignar directamente a SpherePosition.Y
                //de momento lo soluciono con esto
                Vector3 Position = SpherePosition;
                Position.Y = 10f;
                SpherePosition = Position;
            }

            if(BirdPosition.Z <= (-1*1450f))
            {
                BirdPosition = new Vector3(BirdPosition.X,BirdPosition.Y,BirdPosition.Z+10f);
                BirdWorld = Matrix.CreateTranslation(BirdPosition);
            }
            if(BirdPosition.Z >= (-1f*1190f))
            {
                BirdPosition = new Vector3(BirdPosition.X,BirdPosition.Y,BirdPosition.Z-10f);
                BirdWorld = Matrix.CreateTranslation(BirdPosition);
            }


            if (BridgeCollision.Intersects(SphereCollide) )
            {
                SpherePosition = SpherePosition + new Vector3(0,1f,0);
            }

            if (SlideCollide.Intersects(SphereCollide))
            {
                Velocity= Velocity + new Vector3(0,30f,0);
                SpherePosition = SpherePosition + new Vector3(0,20f,0);

            }
            if (BridgeCollision.Contains(SphereCollide) == ContainmentType.Contains)
            {
                Velocity *= 0.1f;
                SpherePosition = SpherePosition + new Vector3(0,DIAMETER/2,0);
            }
            SphereCollide= new BoundingSphere(SpherePosition,DIAMETER/2);
            
            //Dejar siempre al final del update porque necesita la posicion ya calculada
            SpherePosition=collisionObstacle(SphereCollide);
            
            collisionCheckpoint(SphereCollide);
            /*alive = isOnTrack();
            if (!alive)
            {
               SpherePosition= Loss();
            }*/
            
            moverCamaraMouse();
            UpdateCamera();

            base.Update(gameTime);
        }

        private bool isOnTrack()
        {
            if (Pared1.Intersects(SphereCollide) || Pared2.Intersects(SphereCollide) || Pared3.Intersects(SphereCollide) || Pared4.Intersects(SphereCollide) || AguaCollide.Intersects(SphereCollide) || ArenaCollide.Intersects(SphereCollide)){
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
 
        
        protected override void Draw(GameTime gameTime)
        {
            

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
           
            View = Matrix.CreateLookAt(Camera.Position, Camera.TargetPosition, Vector3.UnitY);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            var originalRasterizerState = GraphicsDevice.RasterizerState;
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            Graphics.GraphicsDevice.RasterizerState = rasterizerState;
            SkyBox.Draw(View, Camera.Projection, Camera.Position);

            GraphicsDevice.RasterizerState = originalRasterizerState;
            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            generateLevel();

            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            BallEffect.Parameters["View"].SetValue(Camera.View);
            BallEffect.Parameters["Projection"].SetValue(Camera.Projection);
            if (Checkpoint1==0)
            {
                DrawCylinder(Cylinder, Checkpoint1Position);
            }

            if (Checkpoint2==0)
            {
                DrawCylinder(Cylinder, Checkpoint2Position);
            }
          //  DrawGeometry(Test,BridgePosition,0,0,0);
            DrawSphere(SpherePosition, 0f, Pitch, Roll);
            //DrawGeometry(Sphere, SpherePosition, 0f, Pitch, Roll);
   
          

           
          //  FuncionesGenerales.drawMesh(SphereModel,SphereWorld*Matrix.CreateTranslation(SpherePosition),BallEffect);
    
          /*spriteBatch.Begin();
 
          spriteBatch.DrawString(font, SpherePosition.ToString(), new Vector2(100,20), Color.Black);
          spriteBatch.End();*/
          


        }

        private void DrawCylinder(GeometricPrimitive geometry, Vector3 position)
        {
            var effect = Effect;
            Effect.Parameters["DiffuseColor"].SetValue(Color.Yellow.ToVector3());
           
            effect.Parameters["Alfa"].SetValue(0.3f);
            effect.Parameters["World"].SetValue( Matrix.CreateTranslation(position));
      
 
            geometry.Draw(effect);
            effect.Parameters["Alfa"].SetValue(1f);

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
        private void DrawSphere(Vector3 position, float yaw, float pitch, float roll)
        {
            BallEffect.Parameters["ModelTexture"]?.SetValue(RubberTexture);
            foreach (var mesh in SphereModel.Meshes)
            {
                BallWorld = mesh.ParentBone.Transform;
                BallEffect.Parameters["World"].SetValue(Matrix.CreateScale(0.2f)*Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateTranslation(position));
                mesh.Draw();
            }
        }

        private void DrawGeometry(GeometricPrimitive geometry, Vector3 position, float yaw, float pitch, float roll)
        {
            var effect = geometry.Effect;

            effect.World = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateTranslation(position);
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;

            geometry.Draw(effect);
        }
    
        private void collisionCheckpoint(BoundingSphere esfera)
        {

            if (CheckPoint1Collide.Intersects(esfera))
            {
                Checkpoint1 = 1;

            }

            if (Checkpoint1==1 && CheckPoint2Collide.Intersects(esfera))
            {
                Checkpoint2 = 1;
            }




            if (Checkpoint1==1 && Checkpoint2==1 && FinishCollide.Intersects(esfera))
            {
                SpherePosition = new Vector3(2, 2, 2);
            }
        }

        private Vector3 Loss()
        {
            
                if (Checkpoint1 == 1)
                {
                    return Checkpoint1Position;
                }

                if (Checkpoint1 == 1 && Checkpoint2 == 1)
                {
                    return Checkpoint2Position;
                }

                if (Checkpoint1 == 0 && Checkpoint1 == 0)
                {
                    Exit();
                    return SpherePosition;
                }
            

            return  SpherePosition;
        }
        private Vector3 collisionObstacle(BoundingSphere esfera)
        {
            if (BallCollide1.Intersects(esfera) || BallCollide2.Intersects(esfera) || BallCollide3.Intersects(esfera))
            {
              //no se porque no anda

              return Loss();

            }
            return  SpherePosition;

        }

        private void generateLevel()
        
        {
            /*
            var Alto = GraphicsDevice.Viewport.Bounds.Height;
            var Ancho = GraphicsDevice.Viewport.Bounds.Width;
            Debug.Write(Alto);
            var a100 = (1920f / 800f) * 100f;
            Debug.Write(Ancho);
           // var Alto100=*/
        Effect.Parameters["DiffuseColor"].SetValue(Color.DarkGreen.ToVector3());

        for (float i = 0; i < 40; i++)
        {
            for (float j = 0; j < 20; j++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.4f) *
                                                        Matrix.CreateTranslation(i * 200f - 6000f, 0, j * -200f));
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

        for (float i = 0; i < 10; i++)
        {
            foreach (var mesh in PathModel.Meshes)
            {
                PathWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(-i*200f-1500, 5f, - 2000f));
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
        for (float i = 0; i < 4; i++)
        {
            foreach (var mesh in PathModel.Meshes)
            {
                PathWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(PathWorld * Matrix.CreateScale(0.5f) *
                                                    Matrix.CreateTranslation(400f, 5f, i * -200f ));
                mesh.Draw();

            }
        }
        for (float i = 0; i < 2; i++)
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
            Effect.Parameters["World"].SetValue(SlideWorld * Matrix.CreateScale(140f) *
                                                Matrix.CreateRotationY(-MathHelper.PiOver2) *
                                                Matrix.CreateTranslation(-520f, 6f, -2000f));
            mesh.Draw();
        }

        Effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector3());
    
            foreach (var mesh in BallModel.Meshes)
            {
                BallWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BallWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateScale(0.8f) *
                                                    Matrix.CreateTranslation(400f, 12f, 0f + -200f));
                mesh.Draw();
            }

   
            foreach (var mesh in BallModel.Meshes)
            {
                BallWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BallWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateScale(1f) *
                                                    Matrix.CreateTranslation(350f, 12f, 0f +  -400f));
                mesh.Draw();
            }
            foreach (var mesh in BallModel.Meshes)
            {
                BallWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BallWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateScale(1f) *
                                                    Matrix.CreateTranslation(450f, 12f, 0f +  -600f));
                mesh.Draw();
            }
            foreach (var mesh in BirdModel.Meshes)
            {
                BirdWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(BirdWorld * 
                                                    Matrix.CreateScale(3f) *
                                                    Matrix.CreateTranslation(BirdPosition));
                mesh.Draw();
            }
            foreach (var mesh in ArcoModel.Meshes)
            {
                ArcoWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(ArcoWorld * Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateScale(2f) *
                                                    Matrix.CreateTranslation(-3200, 12f, -2000f));
                mesh.Draw();
            }

        Effect.Parameters["DiffuseColor"].SetValue(Color.LightPink.ToVector3());

        foreach (var mesh in BodyModel.Meshes)
        {
            BodyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"].SetValue(BodyWorld * Matrix.CreateScale(1f) *
                                                Matrix.CreateRotationY(MathHelper.Pi) *
                                                Matrix.CreateTranslation(200f, 150f, 150f));
            mesh.Draw();
        }

        foreach (var mesh in BodyModel.Meshes)
        {
            BodyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(BodyWorld * Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(200f, 150f, 0f));
            mesh.Draw();
        }

        foreach (var mesh in BoyModel.Meshes)
        {
            BoyWorld = mesh.ParentBone.Transform;
            Effect.Parameters["World"]
                .SetValue(BoyWorld *Matrix.CreateRotationY(MathHelper.PiOver4) *Matrix.CreateScale(10f) * Matrix.CreateTranslation(200f, 8f, -500f)) ;
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

        for (float i = 0; i < 6; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.1f) *
                                                    Matrix.CreateTranslation(-300f, 8f, i * -300f + 100f));
                mesh.Draw();
            }
        }
        for (float i = 0; i < 9; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.1f) *
                                                    Matrix.CreateTranslation(-i*300f-600f, 8f, -1600f));

                mesh.Draw();
            }
        }
        for (float i = 0; i < 11; i++)
        {
            foreach (var mesh in Tree1Model.Meshes)
            {
                TreeWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(TreeWorld * Matrix.CreateScale(0.1f) *
                                                    Matrix.CreateTranslation(-i*300f+50f, 8f, -2300f));

                mesh.Draw();
            }
        }

        for (float i = 0; i < 13; i++)
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
        if (drawStar)
        {
            foreach (var mesh in StarModel.Meshes)
            {
                StarWorld = mesh.ParentBone.Transform;
                Effect.Parameters["World"].SetValue(StarWorld * Matrix.CreateScale(1f) *
                                                    Matrix.CreateRotationY(MathHelper.PiOver2) *
                                                    Matrix.CreateTranslation(400f, 5f, -100f));
                mesh.Draw();
            }
        }

        for (float i = 0; i < 5; i++)
        {
            for (float j = 0; j < 5; j++)
            {
                foreach (var mesh in GrassModel.Meshes)
                {
                    GrassWorld = mesh.ParentBone.Transform;
                    Effect.Parameters["World"].SetValue(GrassWorld * Matrix.CreateScale(0.4f) *
                                                        Matrix.CreateTranslation(-i * 200f - 600f, 6f,
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
        SphereRotation = Matrix.CreateRotationY(Mouse.GetState().Position.ToVector2().X*MouseSensitivity);

        SphereFrontDirection = Vector3.Transform(Vector3.Forward, SphereRotation);
    }

    private void chequearPropiedadesTextura(Texture2D texture){
        if (texture == RubberTexture){
            JumpSpeed = 200f;
        }else if (texture == MetalTexture){
            currentSpeed = 75f;
        }else if (texture == RockTexture){
            currentSpeed = 20f;
        }
    }
    }

}
