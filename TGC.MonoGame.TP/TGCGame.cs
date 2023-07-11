using System;
using System.Diagnostics;
using BepuPhysics.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using System.IO;
using Microsoft.Xna.Framework.Media;

using System.Linq;
using System.Net.Http;
using BepuPhysics;
using BepuPhysics.CollisionDetection.CollisionTasks;
using BepuUtilities.Memory;
using Microsoft.Xna.Framework.Audio;
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
        private const int ShadowmapSize = 2048;

        private const float SPEED = 50f;
        private const float DIAMETER = 10f;
        private const float CameraFollowRadius = 100f;
        private const float CameraUpDistance = 30f;
        private const float SphereRotatingVelocity = 0.05f;
        private const float MouseSensitivity = -0.005f;
        private const float CheckPointHeight = 5000;
        private const float CheckPointRadius = 80;
        private bool alive;
        private bool inMenu = true;
        private StaticCamera CubeMapCamera { get; set; }
        public Effect ShadowMap { get; set; }

        private SoundEffect Music{ get; set; }
        private SoundEffect jumpSoundEffect { get; set; }
        public float tiempoStar = 0f;
        public RenderTargetCube EnvironmentMapRenderTarget { get; set; }

        private SoundEffect loseSoundEffect { get; set; }
        private SoundEffect BoostSoundEffect { get; set; }
        private SoundEffect ChangetSoundEffect { get; set; }
        private BoundingFrustum BoundingFrustum { get; set; }
        public Matrix FootWorld { get; set; }

        private SpriteBatch spriteBatch { get; set; }
        public Model SunModel { get; set; }


        private GraphicsDeviceManager Graphics { get; set; }
        private TargetCamera Camera { get; set; }
        private Effect Effect { get; set; }
        private Effect BallEffect { get; set; }
        private Effect RockEffect { get; set; }
        private RenderTarget2D RenderTarget { get; set; }
        private Effect RubberEffect { get; set; }
        private FullScreenQuad FullScreenQuad { get; set; }
        private Effect PostProcessEffect { get; set; }

        private Effect MetalEffect { get; set; }
        public Model FootModel { get; set; }
        public Matrix RatWorld { get; set; }

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
        private Model InstruccionModel { get; set; }

        private Matrix Tree1World { get; set; }
        private Model BridgeModel { get; set; }
        private Matrix BridgeWorld { get; set; }
        private Model DogModel { get; set; }
        private Matrix DogWorld { get; set; }
        private Model GrassModel { get; set; }
        private Matrix GrassWorld { get; set; }
        private Model LampModel { get; set; }
        private Matrix LampWorld { get; set; }
        public Model pathTextModel { get; set; }
        public Model checkpointTextModel { get; set; }
        public Model moveTextModel { get; set; }
        public Model RatModel { get; set; }

        private Model StarModel { get; set; }
        
        private Model BirdModel { get; set; }
        private Model PlayModel { get; set; }

        private Matrix BirdWorld { get; set; }
        private Vector3 RatPosition { get; set; }

        private Matrix StarWorld { get; set; }
        private Matrix FloorWorld { get; set; }
        private Model SlideModel { get; set; }
        private Matrix SlideWorld { get; set; }
        private Model BikeModel { get; set; }
        private Model ArcoModel { get; set; }
        private Effect BlinnEffect { get; set; }
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
        private Matrix PlayMatrix { get; set; }
        private Matrix TextMatrix { get; set; }

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
        private OrientedBoundingBox FootOBB { get; set; }
        private OrientedBoundingBox RatOBB { get; set; }


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
        private BoundingSphere BirdCollide { get; set; }

        private Vector3 SphereFrontDirection { get; set; }
        Matrix mundoCalculado{ get; set; }
        private float Yaw { get; set; }
        private float Pitch { get; set; }
        private float Roll { get; set; }
        private float elapsedTime { get; set; }
        private float pastMousePositionY;
        private SpriteFont font;
        public OrientedBoundingBox ArenaCollide { get; set; }


        private Effect MenuEffect { get; set; }
        private Texture2D RockTexture { get; set; }
        private Texture2D TexturaActual { get; set; }
        private Texture2D MenuTexture2D { get; set; }
        private Texture2D MetalRoughness { get; set; }
        private Texture2D MetalNormal { get; set; }
        private Texture2D RockRoughness { get; set; }
        private Texture2D RockNormal { get; set; }
        private Texture2D GomaNormal { get; set; }
        private Texture2D GomaRoughness { get; set; }
        private Texture2D TexturaNormal { get; set; }
        private Texture2D TexturaMetal { get; set; }
        private Texture2D TexturaAO { get; set; }

        private Texture2D TexturaRoughness { get; set; }
        private BoundingSphere RangoRaton { get; set; }

        private Texture2D RockAO { get; set; }
        private Texture2D RockMetal { get; set; }
        private Texture2D MetalMetal { get; set; }
        private Texture2D MetalAO { get; set; }
        private Texture2D GomaAO { get; set; }
        private Texture2D GomaMetal { get; set; }

        private Matrix InstruccionMatrix { get; set; }
        private Matrix MoveMatrix { get; set; }
        private Matrix PathMatrix { get; set; }
        private Matrix CheckpointMatrix { get; set; }

        private TextureCube SkyBoxTexture { get; set; }
        private Model SkyBoxModel { get; set; }
        private Model TextModel { get; set; }

        private Effect SkyBoxEffect { get; set; }
        private Effect PBR { get; set; }
        private Effect PBRMalo { get; set; }
        private Effect ActualPBR { get; set; }

        private SkyBox SkyBox { get; set; }
        public TargetCamera TargetLightCamera { get; set; }

        private Texture2D MetalTexture { get; set; }
        private Texture2D RubberTexture { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        private Vector3 BridgePosition;

        private Vector3 lightPosition = new Vector3(100f,400f,-1600f);
        protected override void Initialize()
        {
          
          

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
            RatPosition = new Vector3(-5000f, 0f, -2000f);
            RatWorld = Matrix.CreateTranslation(RatPosition);
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
            PlayMatrix = Matrix.Identity;
            InstruccionMatrix = Matrix.Identity;
            PathMatrix = Matrix.Identity;
            MoveMatrix = Matrix.Identity;
            CheckpointMatrix = Matrix.Identity;

            FootWorld=Matrix.CreateScale(30f)* Matrix.CreateTranslation(new Vector3(-3000,350,-2000));
            BirdPosition = new Vector3(-1000f, 150f, -1500f);
            Checkpoint1 = 0;
            Checkpoint2 = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            god = false;
            Checkpoint1Position = new Vector3(400, 0, -2000);
            Checkpoint2Position = new Vector3(-1680, 0, -2000);
            CheckPoint1Collide = new BoundingCylinder(Checkpoint1Position,  CheckPointRadius,CheckPointHeight/2);
            CheckPoint2Collide = new BoundingCylinder(Checkpoint2Position,CheckPointRadius,CheckPointHeight/2);
            FinishPosition = new Vector3(-5200, 12f, -2000f);
            FinishCollide = new OrientedBoundingBox(FinishPosition, new Vector3(12,50000,5000));
            Pared1 = new OrientedBoundingBox(new Vector3(280,500,-750), new Vector3(1,500,1000));
            Pared2 = new OrientedBoundingBox(new Vector3(520,500,-950), new Vector3(1,500,1200));
            Pared3 = new OrientedBoundingBox(new Vector3(-2045,500,-1760), new Vector3(2340,2000,1));
            Pared4 = new OrientedBoundingBox(new Vector3(-1827.5f,500,-2240), new Vector3(2400,2000,1));
            Pared5 = new OrientedBoundingBox(new Vector3(-21321321200, 12f, -2000f), new Vector3(400,500,1));
            RatOBB = new OrientedBoundingBox(new Vector3(-2000, 0f, -2000f), new Vector3(50,20,80));

            AguaCollide = new OrientedBoundingBox(new Vector3(500,0,-1150), new Vector3(500,7,300));

            ArenaCollide = new OrientedBoundingBox(new Vector3(-940, 4, -2550), new Vector3(500, 1 ,800));
            SlideCollide = new OrientedBoundingBox(new Vector3(-450, 20, -2000), new Vector3(200, 50 ,200));
            
            SlideCollide.Rotate(Matrix.CreateRotationZ(MathHelper.PiOver4));
            StarCollide = new BoundingSphere(new Vector3(400f, 5f, -100f), 18f);
            SphereCollide = new BoundingSphere(SpherePosition, DIAMETER/2);
            BallCollide1 = new BoundingSphere(new Vector3(400f, 12f, 0f + -200f), (DIAMETER/2)-1);
            BallCollide2 = new BoundingSphere(new Vector3(350f, 12f, 0f + -400f), (DIAMETER/2)-1);
            BallCollide3 = new BoundingSphere(new Vector3(450f, 12f, 0f + -600f), (DIAMETER/2)-1);
            BridgePosition = new Vector3(400f, -230f, -1300f);
            BridgeCollision = new BoundingSphere(BridgePosition, 320f);
            RangoRaton = new BoundingSphere(new Vector3(-4500, 0f, -2000f), 1000f);

            BirdCollide = new BoundingSphere(BirdPosition, 50f);
            var size = GraphicsDevice.Viewport.Bounds.Size;
            size.X /= 2;
            size.Y /= 2;


            Test = new SpherePrimitive(GraphicsDevice, 320f*2f, 16, Color.Silver);


            BikeWorld = Matrix.Identity;
            pastMousePositionY = Mouse.GetState().Position.ToVector2().Y;
            IsMouseVisible = true;
            Mouse.SetPosition(GraphicsDevice.Viewport.Bounds.Height/2,GraphicsDevice.Viewport.Bounds.Width/2);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            BoundingFrustum = new BoundingFrustum(Camera.View * Camera.Projection);

            // ENVIROMENT MAP
            CubeMapCamera = new StaticCamera(1f, SpherePosition, Vector3.UnitX, Vector3.Up);
            CubeMapCamera.BuildProjection(1f, 0.1f,6000f , MathHelper.PiOver4);
            // Camera Light

           TargetLightCamera = new TargetCamera(1f,lightPosition , Vector3.Zero);
            TargetLightCamera.BuildProjection(1f, 0.1f, 6000f,
                MathHelper.PiOver2);
            base.Initialize();
        }

        public OrientedBoundingBox Pared5 { get; set; }


        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void LoadContent()
        {
            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            FullScreenQuad = new FullScreenQuad(GraphicsDevice);
            RenderTarget = new RenderTarget2D(GraphicsDevice, ShadowmapSize, ShadowmapSize, false,
                SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.PlatformContents);
            EnvironmentMapRenderTarget = new RenderTargetCube(GraphicsDevice, 2048, false,
                SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);
            GraphicsDevice.BlendState = BlendState.Opaque;
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
            PlayModel = Content.Load<Model>(ContentFolder3D + "menu/arcade");
            TextModel = Content.Load<Model>(ContentFolder3D + "play/text3d");
            SunModel = Content.Load<Model>(ContentFolder3D + "sun/sun");
            FootModel = Content.Load<Model>(ContentFolder3D + "foot/foot");
            pathTextModel = Content.Load<Model>(ContentFolder3D + "pathtext/pathtext");
            moveTextModel = Content.Load<Model>(ContentFolder3D + "movertext/movertext");
            checkpointTextModel = Content.Load<Model>(ContentFolder3D + "checkpointtext/checkpointtext");
            InstruccionModel = Content.Load<Model>(ContentFolder3D + "instrucciones/instrucciones");
            VolverModel = Content.Load<Model>(ContentFolder3D + "volver/volver");
            RatModel = Content.Load<Model>(ContentFolder3D + "rat/rat");
            AltoModel = Content.Load<Model>(ContentFolder3D + "quality/alto");
            BajoModel = Content.Load<Model>(ContentFolder3D + "quality/bajo");

            SkyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "skybox");

            MenuTexture2D = Content.Load<Texture2D>(ContentFolder3D + "menu/tex");
            MetalRoughness = Content.Load<Texture2D>(ContentFolderTextures + "metal_rough");
            MetalNormal = Content.Load<Texture2D>(ContentFolderTextures + "metal_normal");
            RockNormal = Content.Load<Texture2D>(ContentFolderTextures + "rock_normal");
            RockRoughness = Content.Load<Texture2D>(ContentFolderTextures + "rock_rough");
            GomaNormal = Content.Load<Texture2D>(ContentFolderTextures + "goma_normal");
            GomaRoughness = Content.Load<Texture2D>(ContentFolderTextures + "goma_rough");
            RockTexture = Content.Load<Texture2D>(ContentFolderTextures + "rock_diffuse");
            RubberTexture = Content.Load<Texture2D>(ContentFolderTextures + "goma_diffuse");
            MetalTexture =  Content.Load<Texture2D>(ContentFolderTextures + "metal_diffuse");
            MetalAO = Content.Load<Texture2D>(ContentFolderTextures + "metal_ao");
            MetalMetal = Content.Load<Texture2D>(ContentFolderTextures + "metal_metal");
            RockAO = Content.Load<Texture2D>(ContentFolderTextures + "rock_ao");
            RockMetal = Content.Load<Texture2D>(ContentFolderTextures + "rock_metal");
            GomaAO = Content.Load<Texture2D>(ContentFolderTextures + "goma_ao");
            GomaMetal = Content.Load<Texture2D>(ContentFolderTextures + "goma_metal");

           // PathTexture = Content.Load<Texture2D>(ContentFolder3D + "path/cobblestone lowpoly_diffuse");
           // GrassTexture = Content.Load<Texture2D>(ContentFolder3D + "grass/grass2");

            TexturaActual = RockTexture;
            TexturaNormal = RockNormal;
            TexturaRoughness = RockRoughness;
            TexturaAO = RockAO;
            TexturaMetal = RockMetal;
                // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            BlinnEffect = Content.Load<Effect>(ContentFolderEffects + "BlinnPhong");
            PBR = Content.Load<Effect>(ContentFolderEffects + "PBR");
            
            ShadowMap = Content.Load<Effect>(ContentFolderEffects + "ShadowMap");

           
            PostProcessEffect = Content.Load<Effect>(ContentFolderEffects + "PostProcess");

            MenuEffect = Content.Load<Effect>(ContentFolderEffects + "MenuShader");

            SkyBoxEffect = Content.Load<Effect>(ContentFolderEffects + "SkyBox");

            FuncionesGenerales.loadEffectOnMesh(TextModel,BlinnEffect);

            FuncionesGenerales.loadEffectOnMesh(PlayModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(GrassModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BodyModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(LampModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(MonumentModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BoyModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(TreeModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(Tree1Model, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(Tree2Model, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(SunModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(moveTextModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(checkpointTextModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(pathTextModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(RatModel, BlinnEffect);

            FuncionesGenerales.loadEffectOnMesh(DogModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(StarModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BridgeModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BikeModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(PathModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BenchModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(SlideModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BirdModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(ArcoModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BallModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(SphereModel,PBR);
            FuncionesGenerales.loadEffectOnMesh(FootModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(InstruccionModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(AltoModel, BlinnEffect);
            FuncionesGenerales.loadEffectOnMesh(BajoModel, BlinnEffect);

            FuncionesGenerales.loadEffectOnMesh(VolverModel, BlinnEffect);


            //BridgeModel.Meshes.Get.GetVerticesAndIndicesFromModel.

            //ConvexHullHelper.CreateShape(BridgeModel.Bones., ,);
            //TexturaActual = RockTexture;
            BlinnEffect.Parameters["baseTexture"].SetValue(RockTexture);
            Music = Content.Load<SoundEffect>(ContentFolderMusic + "ost");
            jumpSoundEffect = Content.Load<SoundEffect>(ContentFolderMusic + "jump");
            loseSoundEffect = Content.Load<SoundEffect>(ContentFolderMusic + "fail");
            BoostSoundEffect = Content.Load<SoundEffect>(ContentFolderMusic + "powerup");
            SoundEffect.Initialize();
            SoundEffect.MasterVolume = 0.1f;
            Music.Play();
           // Music.Duration.Add(TimeSpan.MaxValue);
           SkyBox = new SkyBox(SkyBoxModel,SkyBoxTexture,SkyBoxEffect);
           PBR.CurrentTechnique = PBR.Techniques["PBR"];

           var positions = PBR.Parameters["lightPositions"].Elements;
           var colors = PBR.Parameters["lightColors"].Elements;
           List<Vector3> lightpositions = new List<Vector3>();
           lightpositions.Add(lightPosition);
           lightpositions.Add(lightPosition);
           
           for (var index = 0; index < lightpositions.Count; index++)
           {

               positions[index].SetValue(lightpositions[index]);
               colors[index].SetValue(new Vector3(1400f, 1400f, 1400f));
           }
            base.LoadContent();
        }

        public Model BajoModel { get; set; }

        public Model AltoModel { get; set; }

        public Texture2D GrassTexture { get; set; }

        public Texture2D PathTexture { get; set; }


        private Matrix View { get; set; }
        private bool inInstrucciones;
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
                if (inMenu)
                {
                    Camera.Position = new Vector3(2000f, 650f, 1000f);
                    Camera.TargetPosition = new Vector3(2000f, 400f, 1500f);
                    if (inInstrucciones)
                    {
                        Camera.Position = new Vector3(2000f, 1650f, 1000f);
                        Camera.TargetPosition = new Vector3(2000f, 1400f, 1500f);

                    }
                }
                else
                {
                    Camera.Position = SpherePosition + orbitalPosition + upDistance;
                    Camera.TargetPosition = SpherePosition;


                }
                

                // Set the Target as the Robot, the Camera needs to be always pointing to it
           

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
        private bool Dir = true ;
        private bool DirAnterior;
        private bool Bajando= false;
        private float materialPeso = 1f;
        private float footCeiling = 340f;
        private float velocidadPeso = 1f;
        public bool calidad=true;
        protected override void Update(GameTime gameTime)
        {
            
            if (TexturaActual == RockTexture)
            {
                materialPeso = 2.3f;
                velocidadPeso = 0.5f;
            }
            if (TexturaActual == MetalTexture)
            {
                materialPeso = 1.7f;
                velocidadPeso = 1.9f;

            }

            if (TexturaActual == RubberTexture)
            {
                velocidadPeso = 1.3f;
                materialPeso = 1f;
            }
            
            elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            if (tiempoStar > 0f)
            {
                tiempoStar -= elapsedTime;    
            }
            
            UpdateCamera();
            if (inMenu==false)
            {

                moverCamaraMouse();
               // TestCamera.Update(gameTime);

                BoundingFrustum.Matrix = Camera.View * Camera.Projection;

                // Aca deberiamos poner toda la logica de actualizacion del juego.
                //chequearPropiedadesTextura(BlinnEffect.Parameters["ModelTexture"]?.GetValueTexture2D());
                if (TexturaActual == null)
                {
                    TexturaActual = RockTexture;
                    TexturaNormal = RockNormal;
                    TexturaRoughness = RockRoughness;
                    TexturaMetal = RockMetal;
                    TexturaAO = RockAO;

                }
                if (Keyboard.GetState().IsKeyDown(Keys.G))
                {
                    TexturaActual = RubberTexture;
                    TexturaNormal = GomaNormal;
                    TexturaRoughness = GomaRoughness;
                    TexturaMetal = GomaMetal;
                    TexturaAO = GomaAO;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    TexturaActual = RockTexture;
                    TexturaNormal = RockNormal;
                    TexturaRoughness = RockRoughness;
                    TexturaMetal = RockMetal;
                    TexturaAO = RockAO;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    TexturaActual = MetalTexture;
                    TexturaNormal = MetalNormal;
                    TexturaRoughness = MetalRoughness;
                    TexturaMetal =MetalMetal;
                    TexturaAO = MetalAO;

                }
                
                //Movimiento del Pie
                var FootPosition = FootWorld.Translation;
                if (FootPosition.Y > footCeiling)
                {
                    Bajando = true;
                }

                if (FootPosition.Y < -80f)
                {
                    Bajando = false;
                }
                if (Bajando)
                {
                    FootWorld *= Matrix.CreateTranslation(0f, -350f * elapsedTime, 0f);
                }
                else
                {
                    FootWorld *= Matrix.CreateTranslation(0f, 190f * elapsedTime, 0f);
                }

                FootOBB = new OrientedBoundingBox(FootWorld.Translation, new Vector3(50, 30, 50));



                isOnGround = MathF.Abs(SpherePosition.Y) <= 10f;
                if (BridgeCollision.Intersects(SphereCollide) || SlideCollide.Intersects(SphereCollide))
                {
                    isOnGround = true;
                }


                if (isOnGround)
                {
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
                        Acceleration = -Velocity * velocidadPeso* FRICTION;
                    }



                }
                else
                {
                    Acceleration = Vector3.Zero;
                }

                if (!BridgeCollision.Intersects(SphereCollide) || !SlideCollide.Intersects(SphereCollide))
                {
                    Acceleration.Y = GRAVITY*materialPeso;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && isOnGround)
                {
                    Velocity.Y += JumpSpeed;
                    jumpSoundEffect.Play();
                }

                if (StarCollide.Intersects(SphereCollide))
                {
                    Velocity = new Vector3(Velocity.X * 1.2f, Velocity.Y, Velocity.Z * 1.2f);
                    drawStar = false;
                    BoostSoundEffect.Play();
                    tiempoStar=elapsedTime*10;
                }

                Velocity += Acceleration * elapsedTime;

                SpherePosition += Velocity * elapsedTime;

                
                if (SpherePosition.Y <= 10f)
                {
                    Velocity.Y = 0f;
                    //no se por qué tira error de que no se puede asignar directamente a SpherePosition.Y
                    //de momento lo soluciono con esto
                    Vector3 Position = SpherePosition;
                    Position.Y = 10f;
                    SpherePosition = Position;
                }

                var ZBird = BirdPosition.Z;
                if (ZBird> -1490f)
                {
                    Dir = true;
                }
                if (ZBird < -2500f)
                {
                    Dir = false;
                }

               


                if (Dir)
                {
              
                    BirdPosition += new Vector3(0f,0f ,-250*elapsedTime);
                }
                else
                {
                    BirdPosition += new Vector3(0f, 0f,250f*elapsedTime);

                }

                DirAnterior = Dir;
                if (DirAnterior != Dir)
                {
                    BirdWorld = Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(BirdPosition);
                }
                else
                {
                    BirdWorld = Matrix.CreateTranslation(BirdPosition);
 
                }

                if (BridgeCollision.Intersects(SphereCollide))
                {
                    SpherePosition = SpherePosition + new Vector3(0, 1f, 0);
                }

                if (SlideCollide.Intersects(SphereCollide))
                {
                    Velocity = Velocity + new Vector3(0, 30f, 0);
                    SpherePosition = SpherePosition + new Vector3(0, 20f, 0);

                }

                if (BridgeCollision.Contains(SphereCollide) == ContainmentType.Contains)
                {
                    Velocity *= 0.1f;
                    SpherePosition = SpherePosition + new Vector3(0, DIAMETER / 2, 0);
                }

                if (RangoRaton.Contains(SphereCollide).Equals(ContainmentType.Contains))
                {
                   
                    var RatTranslation = SpherePosition - RatPosition;
                    var producto =Vector3.Dot(SpherePosition, RatPosition);
                    var angulo = MathF.Acos((producto) / (SpherePosition.Length() * RatPosition.Length()));
                   // angulo = angulo * 360f / (MathF.PI*2f);
                    if (SpherePosition.X < RatPosition.X) angulo = 360 - angulo;
                    RatTranslation.Y = 0f;
                    RatTranslation.Normalize();
                    RatTranslation *= 200f * elapsedTime;
                    RatPosition += RatTranslation;
                    RatWorld = Matrix.CreateRotationY(angulo)*Matrix.CreateTranslation(RatPosition);
                    RatOBB = new OrientedBoundingBox(RatPosition, new Vector3(50, 30, 50));
                }

                collisionCheckpoint(SphereCollide);
                SphereCollide = new BoundingSphere(SpherePosition, DIAMETER / 2);

                //Dejar siempre al final del update porque necesita la posicion ya calculada
                SpherePosition = collisionObstacle(SphereCollide);
                
                alive = isOnTrack();
                if (!alive)
                {
                    SpherePosition = Loss();
                }
            }else
            {

                var ancho = GraphicsDevice.Viewport.Bounds.Width;
                var largo = GraphicsDevice.Viewport.Bounds.Height;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && (Mouse.GetState().Position.X>ancho/2.2 && Mouse.GetState().Position.X<ancho/1.6 ) && (Mouse.GetState().Position.Y>largo/2.6f && Mouse.GetState().Position.Y<largo/2.3f ))
                {
                    inInstrucciones = true;
                }
                if (inInstrucciones==false &&Mouse.GetState().LeftButton == ButtonState.Pressed && (Mouse.GetState().Position.X>ancho/2.2 && Mouse.GetState().Position.X<ancho/1.6 ) && (Mouse.GetState().Position.Y>largo/4.12f && Mouse.GetState().Position.Y<largo/3.4f ))
                {
                    inMenu = false;
                    
                }
                if (inInstrucciones==false &&Mouse.GetState().LeftButton == ButtonState.Pressed && (Mouse.GetState().Position.X>ancho/2.2 && Mouse.GetState().Position.X<ancho/1.6 ) && (Mouse.GetState().Position.Y>largo/3.2f && Mouse.GetState().Position.Y<largo/2.8f ))
                {
                    calidad = !calidad;

                }
                if (inInstrucciones && Mouse.GetState().LeftButton == ButtonState.Pressed && (Mouse.GetState().Position.X > ancho/2.2 && Mouse.GetState().Position.X<ancho/1.6) && (Mouse.GetState().Position.Y > largo/1.66f && Mouse.GetState().Position.Y < largo/1.44f))
                {
                    inInstrucciones = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

                //Salgo del juego.
                Exit();
            }

            mundoCalculado = Matrix.CreateScale(0.2f)*Matrix.CreateFromYawPitchRoll(0f, Pitch, Roll) * Matrix.CreateTranslation(SpherePosition);

            
            base.Update(gameTime);
        }

        

        private bool isOnTrack()
        {
            if (Pared1.Intersects(SphereCollide) || RatOBB.Intersects(SphereCollide) ||BirdCollide.Intersects(SphereCollide) || Pared2.Intersects(SphereCollide) || Pared3.Intersects(SphereCollide) ||  Pared4.Intersects(SphereCollide) || AguaCollide.Intersects(SphereCollide) || ArenaCollide.Intersects(SphereCollide)){
                
                return false;
            }
           /* if (Pared5.Intersects(SphereCollide)&&Checkpoint2==0)
            {
                return false;

            }*/
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

            BlinnEffect.Parameters["KAmbient"].SetValue(0.5f);
            BlinnEffect.Parameters["KDiffuse"].SetValue(0.5f);
            BlinnEffect.Parameters["KSpecular"].SetValue(0.4f);
            BlinnEffect.Parameters["shininess"].SetValue(0.4f);
            BlinnEffect.Parameters["lightPosition"].SetValue(lightPosition);
           

            BlinnEffect.Parameters["eyePosition"].SetValue(Camera.Position);
            BlinnEffect.Parameters["baseTexture"]?.SetValue(MetalTexture);

            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            MenuEffect.Parameters["View"].SetValue(Camera.View);
            MenuEffect.Parameters["Projection"].SetValue(Camera.Projection);
            if (TexturaActual == RockTexture)
            {
                PBR.Parameters["cantidadEnviroment"].SetValue(0.1f);

            }
            if (TexturaActual == RubberTexture)
            {
                PBR.Parameters["cantidadEnviroment"].SetValue(0.2f);

            }
            if (TexturaActual == MetalTexture)
            {
                PBR.Parameters["cantidadEnviroment"].SetValue(0.7f);

            }
            CubeMapCamera.Position = SpherePosition;
                
            
            // CALCULAR SOMBRAS
            GraphicsDevice.SetRenderTarget(RenderTarget);
              
            generateLevelDepth();
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1f, 0);
            // CALCULAR SOMBRAS
            
            
            DrawWithEnviromentalMap();
           
                
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
 
        }

        private void DrawWithEnviromentalMap()
        {            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Draw to our cubemap from the robot position
            for (var face = CubeMapFace.PositiveX; face <= CubeMapFace.NegativeZ; face++)
            {
                // Set the render target as our cubemap face, we are drawing the scene in this texture
                GraphicsDevice.SetRenderTarget(EnvironmentMapRenderTarget, face);
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1f, 0);

                
                SetCubemapCameraForOrientation(face);

                CubeMapCamera.BuildView();
                BlinnEffect.Parameters["shadowMap"].SetValue(RenderTarget);
                BlinnEffect.Parameters["lightPosition"].SetValue(lightPosition);
                BlinnEffect.Parameters["shadowMapSize"].SetValue(Vector2.One * ShadowmapSize);
                BlinnEffect.Parameters["LightViewProjection"].SetValue(TargetLightCamera.View * TargetLightCamera.Projection);

                DrawAllExceptSphere(CubeMapCamera.View, CubeMapCamera.Projection,face);
            
             
            }
         
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1f, 0);
            BlinnEffect.Parameters["shadowMap"].SetValue(RenderTarget);
          
            DrawAllExceptSphere(Camera.View, Camera.Projection);
            
            PBR.Parameters["environmentMap"].SetValue(EnvironmentMapRenderTarget);
            PBR.Parameters["eyePosition"].SetValue(Camera.Position);
            PBR.Parameters["shadowMap"].SetValue(RenderTarget);
            PBR.Parameters["lightPosition"].SetValue(lightPosition);
            PBR.Parameters["shadowMapSize"].SetValue(Vector2.One * ShadowmapSize);
            PBR.Parameters["LightViewProjection"].SetValue(TargetLightCamera.View * TargetLightCamera.Projection);

            if (!inMenu)
            {
                
                if (BoundingFrustum.Intersects(SphereCollide))
                {
                    DrawSphere(SpherePosition, 0f, Pitch, Roll);
                }
            }
           
        }
        private void SetCubemapCameraForOrientation(CubeMapFace face)
        {
            switch (face)
            {
                default:
                case CubeMapFace.PositiveX:
                    CubeMapCamera.FrontDirection = -Vector3.UnitX;
                    CubeMapCamera.UpDirection = Vector3.Down;
                    break;

                case CubeMapFace.NegativeX:
                    CubeMapCamera.FrontDirection = Vector3.UnitX;
                    CubeMapCamera.UpDirection = Vector3.Down;
                    break;

                case CubeMapFace.PositiveY:
                    CubeMapCamera.FrontDirection = Vector3.Down;
                    CubeMapCamera.UpDirection = Vector3.UnitZ;
                    break;

                case CubeMapFace.NegativeY:
                    CubeMapCamera.FrontDirection = Vector3.Up;
                    CubeMapCamera.UpDirection = -Vector3.UnitZ;
                    break;

                case CubeMapFace.PositiveZ:
                    CubeMapCamera.FrontDirection = -Vector3.UnitZ;
                    CubeMapCamera.UpDirection = Vector3.Down;
                    break;

                case CubeMapFace.NegativeZ:
                    CubeMapCamera.FrontDirection = Vector3.UnitZ;
                    CubeMapCamera.UpDirection = Vector3.Down;
                    break;
            }
        }
        private void DrawAllExceptSphere(Matrix view,Matrix projection,CubeMapFace face)
        {
            if (!inMenu)
            {
                GraphicsDevice.SetRenderTarget(EnvironmentMapRenderTarget,face);
                var originalRasterizerState = GraphicsDevice.RasterizerState;
               var  rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                Graphics.GraphicsDevice.RasterizerState = rasterizerState;
                SkyBox.Draw(Camera.View,Camera.View, Camera.Position);
                GraphicsDevice.RasterizerState = originalRasterizerState;

                generateLevel(view,projection);
                if (Checkpoint1==0)
                {
                    DrawCylinder(Cylinder, Checkpoint1Position);
                }

                if (Checkpoint2==0)
                {
                    DrawCylinder(Cylinder, Checkpoint2Position);
                }
               
               
            }
            else
            {
            
                var originalRasterizerState = GraphicsDevice.RasterizerState;
                var rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                Graphics.GraphicsDevice.RasterizerState = rasterizerState;
                
                PlayMatrix = Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateScale(3f) * Matrix.CreateTranslation(new Vector3(2000f, 150f, 1500f));
                DibujarConBlinnPhongYTextura(PlayMatrix,PlayModel,Color.LightBlue,MenuTexture2D,view,projection);
                
                
               
                TextMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.2f) * Matrix.CreateTranslation(new Vector3(1975f, 450f, 1500f));
                DibujarConBlinnPhong(TextMatrix,TextModel,Color.Green,view,projection);
               

                GraphicsDevice.RasterizerState = originalRasterizerState;

            }
            
        }


        private void DrawAllExceptSphere(Matrix view,Matrix projection)
        {
            if (!inMenu)
            {
                

                
                var originalRasterizerState = GraphicsDevice.RasterizerState;
                var rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                Graphics.GraphicsDevice.RasterizerState = rasterizerState;

                SkyBox.Draw(view,projection, Camera.Position);
                GraphicsDevice.RasterizerState = originalRasterizerState;
                generateLevel(view,projection);
                if (Checkpoint1==0)
                {
                    DrawCylinder(Cylinder, Checkpoint1Position);
                }

                if (Checkpoint2==0)
                {
                    DrawCylinder(Cylinder, Checkpoint2Position);
                }

               
            }
            else
            {
            
                var originalRasterizerState = GraphicsDevice.RasterizerState;
                var rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                Graphics.GraphicsDevice.RasterizerState = rasterizerState;
                
               
                PlayMatrix = Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateScale(3f) * Matrix.CreateTranslation(new Vector3(2000f, 150f, 1500f));
                DibujarConBlinnPhongYTextura(PlayMatrix,PlayModel,Color.LightBlue,MenuTexture2D,view,projection);
                   
                //aaaa
               
                TextMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.2f) * Matrix.CreateTranslation(new Vector3(1975f, 500f, 1500f));
                DibujarConBlinnPhong(TextMatrix,TextModel,Color.Green,view,projection);
                InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(1982, 450f, 1450f));
                DibujarConBlinnPhong(InstruccionMatrix,InstruccionModel,Color.Green,view,projection);
                InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.2f) * Matrix.CreateTranslation(new Vector3(1982, 460, 1500f));
                if (calidad)
                {
                    DibujarConBlinnPhong(InstruccionMatrix,AltoModel,Color.Green,view,projection);

                }
                else
                {
                    DibujarConBlinnPhong(InstruccionMatrix,BajoModel,Color.Green,view,projection);

                }

                if (inInstrucciones)
                {
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(1982, 1550f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,moveTextModel,Color.Green,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(1982, 1500f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,pathTextModel,Color.Green,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(1982, 1450f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,checkpointTextModel,Color.Green,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(1792, 1450f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,ArcoModel,Color.White,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(new Vector3(1772, 1500f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,BallModel,Color.White,view,projection);
                    InstruccionMatrix =  Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(1.3f) * Matrix.CreateTranslation(new Vector3(1752, 1500f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,FootModel,Color.Pink,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(1.7f) * Matrix.CreateTranslation(new Vector3(1762, 1520f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,BirdModel,Color.Gray,view,projection);
                    InstruccionMatrix = Matrix.CreateRotationY(MathHelper.Pi)* Matrix.CreateRotationX(MathHelper.PiOver4) * Matrix.CreateScale(0.2f) * Matrix.CreateTranslation(new Vector3(1982, 1350f, 1450f));
                    DibujarConBlinnPhong(InstruccionMatrix,VolverModel,Color.Green,view,projection);


                }
                GraphicsDevice.RasterizerState = originalRasterizerState;

            }
            
        }

        public Model VolverModel { get; set; }

        private void generateLevelDepth()
        {
            for (float i = 0; i < 30; i++)
            {
             for (float j = 0; j < 15; j++)
             {
                
                    GrassWorld=Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(-i * 200f + 1000f, 0, j * -200f);
                    dibujarDepth(GrassWorld,GrassModel,BlinnEffect);
                
             }
            }
            
            dibujarDepth(RatWorld,RatModel,BlinnEffect);
            
            BikeWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(30f) * Matrix.CreateTranslation(700f, 50f, 0f);
            dibujarDepth(BikeWorld,BikeModel,BlinnEffect);
             
            dibujarDepth(FootWorld,FootModel,BlinnEffect);

            for (float i = 0; i < 10; i++)
            {
                
                    PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-i*200f-1500, 5f, - 2000f);
                    dibujarDepth(PathWorld,PathModel,BlinnEffect);

            }
     
            for (float i = 0; i < 5; i++)
            {
                
                    PathWorld =  Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-i * 200f + 400f, 5f, -2000);
                    dibujarDepth(PathWorld,PathModel,BlinnEffect);

                
            }
            for (float i = 0; i < 4; i++)
            {
                PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(400f, 5f, i * -200f );
                    dibujarDepth(PathWorld,PathModel,BlinnEffect);

                
            }
            for (float i = 0; i < 2; i++)
            {
               
                    PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(400f, 5f, i * -200f - 1750f);
                    dibujarDepth(PathWorld,PathModel,BlinnEffect);

            }
            for (float i = 0; i < 5; i++)
            {
                
                PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-i*200f-2000, 5f, - 2000f);
                dibujarDepth(PathWorld,PathModel,BlinnEffect);

            }
            MonumentWorld =  Matrix.CreateScale(8f) * Matrix.CreateTranslation(400f, 6f, -2500f);
            dibujarDepth(MonumentWorld,MonumentModel,BlinnEffect);

            SlideWorld =  Matrix.CreateScale(140f) * Matrix.CreateRotationY(-MathHelper.PiOver2) * Matrix.CreateTranslation(-520f, 6f, -2000f);
            dibujarDepth(SlideWorld,SlideModel,BlinnEffect);

            if (BoundingFrustum.Intersects(BallCollide1))
            {
                BallWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(0.8f) * Matrix.CreateTranslation(400f, 12f, 0f + -200f);
                dibujarDepth(BallWorld,BallModel,BlinnEffect);

            }

            if (BoundingFrustum.Intersects(BallCollide2))
            {
                BallWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(350f, 12f, 0f + -400f);
                dibujarDepth(BallWorld,BallModel,BlinnEffect);
            }
            if (BoundingFrustum.Intersects(BallCollide3)){

                
                    BallWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(450f, 12f, 0f +  -600f);
                    dibujarDepth(BallWorld,BallModel,BlinnEffect);
                
            }
            
                BirdWorld = Matrix.CreateScale(4f) * Matrix.CreateTranslation(BirdPosition);
                dibujarDepth(BirdWorld,BirdModel,BlinnEffect);
            
            
                ArcoWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(2f) * Matrix.CreateTranslation(FinishPosition);
                dibujarDepth(ArcoWorld,ArcoModel,BlinnEffect);
            

            BodyWorld =Matrix.CreateScale(1f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(200f, 150f, 150f);
            dibujarDepth(BodyWorld,BodyModel,BlinnEffect);

            

        
            BodyWorld =Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(200f, 150f, 0f);
            dibujarDepth(BodyWorld,BodyModel,BlinnEffect);
            

            BoyWorld = Matrix.CreateRotationY(MathHelper.PiOver4) *Matrix.CreateScale(10f) * Matrix.CreateTranslation(200f, 8f, -500f);
            dibujarDepth(BodyWorld,BodyModel,BlinnEffect);
            

            for (float i = 0; i < 25; i++)
            {
              
                    TreeWorld = Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(300f, 8f, i * -40f + 100f);
                    dibujarDepth(TreeWorld,Tree1Model,BlinnEffect);

            }

            for (float i = 0; i < 25; i++)
            {
             
                    Tree1World =Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(510f, 8f, i * -40f + 100f);
                    dibujarDepth(Tree1World,Tree1Model,BlinnEffect);
                
            }

            for (float i = 0; i < 6; i++)
            {
             
                    TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-300f, 8f, i * -300f + 100f);
                    dibujarDepth(TreeWorld,Tree1Model,BlinnEffect);
                
            }
            for (float i = 0; i < 30; i++)
            {
                TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-i*300f-600f, 8f, -1600f);
                    dibujarDepth(TreeWorld,Tree1Model,BlinnEffect);
            }
            for (float i = 0; i < 30; i++)
            {
                TreeWorld =Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-i*300f+50f, 8f, -2300f);

                    dibujarDepth(TreeWorld,Tree1Model,BlinnEffect);
                
            }

            for (float i = 0; i < 13; i++)
            {
               TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(1000, 8f, i * -200f + 100f);
               dibujarDepth(TreeWorld,Tree1Model,BlinnEffect);

            }


            DogWorld =  Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(700f, 6f, -450f);
            dibujarDepth(DogWorld,DogModel,BlinnEffect);
                            

            if (BoundingFrustum.Intersects(BridgeCollision))
            {
                BridgeWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationX(-1f * MathHelper.PiOver2) * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(400f, 0f, -1300f); 
                dibujarDepth(BridgeWorld,BridgeModel,BlinnEffect);
                
            }

            for (float i = 0; i < 3; i++)
            {
                
                    BenchWorld = Matrix.CreateScale(2f) * Matrix.CreateRotationY(-1f * MathHelper.Pi) * Matrix.CreateTranslation(-100f, -100f, i * -320 - 320f);
                    dibujarDepth(BridgeWorld,BridgeModel,BlinnEffect);
                
            }

           
            if (drawStar)
            {
               
                    StarWorld = Matrix.CreateScale(1f) * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(400f, 5f, -100f);
                    dibujarDepth(StarWorld,StarModel,BlinnEffect);
                
            }

            for (float i = 0; i < 5; i++)
            {
                for (float j = 0; j < 5; j++)
                {
                    GrassWorld = Matrix.CreateScale(0.4f) *
                                 Matrix.CreateTranslation(-i * 200f - 600f, 6f, j * -200f - 1750f);
                    dibujarDepth(GrassWorld,GrassModel,BlinnEffect);

                }
            }

            for (float i = 0; i < 5; i++)
            {
                for (float j = 0; j < 3; j++)
                {
                   
                        GrassWorld =Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(i * 200f, 0, j * -200f - 1000f);
                        dibujarDepth(GrassWorld,GrassModel,BlinnEffect);

                }
                
            }

            LampWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(700f, 6f, -1900f);
            dibujarDepth(LampWorld,LampModel,BlinnEffect);
            dibujarDepth(mundoCalculado,SphereModel,PBR);
    
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
        public Random _random;
        private const int SEED = 0;
        private Color RandomColor(Random random)
        {
            // Construye un color aleatorio en base a un entero de 32 bits
            return new Color((uint)random.Next());
        }
        private void DrawSphere(Vector3 position, float yaw, float pitch, float roll)
        {

            Color colorcito = Color.DarkGray;
           if (tiempoStar > 0f)
            {
                _random = new Random(SEED);
                colorcito = RandomColor(_random);
            }

           DibujarPBR(mundoCalculado,SphereModel);

        }

        private void dibujarDepth(Matrix mundo,Model model,Effect effect)
        {
      
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                // Set the render target as our shadow map, we are drawing the depth into this texture
                
                effect.CurrentTechnique = effect.Techniques["DepthPass"];
                if (effect == PBR)
                {
                    foreach (var mesh in model.Meshes)
                    {            
                        Matrix meshMatrix = mesh.ParentBone.Transform;
                        effect.Parameters["matWorldViewProj"].SetValue(meshMatrix*mundo * TargetLightCamera.View * TargetLightCamera.Projection);
                        mesh.Draw();

                    }   
                }
                else
                {
                    foreach (var mesh in model.Meshes)
                    {            
                        Matrix meshMatrix = mesh.ParentBone.Transform;
                        effect.Parameters["WorldViewProjection"].SetValue(meshMatrix*mundo * TargetLightCamera.View * TargetLightCamera.Projection);
                        mesh.Draw();

                    }    
                }
                
        }

        private void DibujarPBR(Matrix mundo, Model model)
        {
            if (calidad)
            {
                PBR.Parameters["metallicTexture"]?.SetValue(TexturaMetal);
                PBR.Parameters["aoTexture"]?.SetValue(TexturaAO);
                PBR.CurrentTechnique = PBR.Techniques["PBR"];
            }
            else
            {
                PBR.CurrentTechnique = PBR.Techniques["PBRMalo"];

            }

            PBR.Parameters["albedoTexture"]?.SetValue(TexturaActual);
            PBR.Parameters["normalTexture"]?.SetValue(TexturaNormal);
            PBR.Parameters["roughnessTexture"]?.SetValue(TexturaRoughness);

            PBR.Parameters["matWorldViewProj"].SetValue(mundo *Camera.View *Camera.Projection);
            PBR.Parameters["matInverseTransposeWorld"].SetValue(Matrix.Transpose(Matrix.Invert(mundo)));
            
            foreach (var mesh in model.Meshes)
            {
                Matrix meshMatrix = mesh.ParentBone.Transform;
                PBR.Parameters["matWorld"].SetValue(mundo*meshMatrix);
              
                mesh.Draw();
            }

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
                inMenu = true;
                alive = true;
                Checkpoint1 = 0;
                Checkpoint2 = 0;
                drawStar = true;
                Velocity = new Vector3(0f, 0f, 0f);
                SpherePosition = new Vector3(350f, 15, 200);
            }
        }

        private Vector3 Loss()
        {
            
              

                if (Checkpoint1 == 1 && Checkpoint2 == 1)
                {
                    Velocity = new Vector3(0f, 0f, 0f);
                    return Checkpoint2Position;
                }else if (Checkpoint1 == 1)
                {
                    Velocity = new Vector3(0f, 0f, 0f);
                    return Checkpoint1Position;
                }

                if (Checkpoint1 == 0 && Checkpoint2 == 0)
                {
                    loseSoundEffect.Play();
                    inMenu = true;
                    alive = true;
                    Checkpoint1 = 0;
                    Checkpoint2 = 0;
                    drawStar = true;
                    Velocity = new Vector3(0f, 0f, 0f);
                    return new Vector3(350f, 15, 200);
                    
                }
            

            return  SpherePosition;
        }
        private Vector3 collisionObstacle(BoundingSphere esfera)
        {
            if (BallCollide1.Intersects(esfera) || BallCollide2.Intersects(esfera) || BallCollide3.Intersects(esfera) || FootOBB.Intersects(esfera))
            {

              return Loss();

            }
            return  SpherePosition;

        }
        private void DibujarConBlinnPhongYTextura(Matrix MundoCalculado,Model Modelo,Color color,Texture2D textura,Matrix view,Matrix projection)
        {
            BlinnEffect.Parameters["baseTexture"].SetValue(textura);
            BlinnEffect.Parameters["ambientColor"].SetValue(color.ToVector3());
            BlinnEffect.Parameters["diffuseColor"].SetValue(color.ToVector3());
            BlinnEffect.Parameters["specularColor"].SetValue(color.ToVector3());
            BlinnEffect.Parameters["shadowMap"].SetValue(RenderTarget);
            BlinnEffect.Parameters["lightPosition"].SetValue(lightPosition);
            BlinnEffect.Parameters["shadowMapSize"].SetValue(Vector2.One * ShadowmapSize);
            BlinnEffect.Parameters["LightViewProjection"].SetValue(TargetLightCamera.View * TargetLightCamera.Projection);

            foreach (var mesh in Modelo.Meshes)
            {
                Matrix meshMatrix = mesh.ParentBone.Transform;
                BlinnEffect.Parameters["World"].SetValue(meshMatrix*MundoCalculado);
                BlinnEffect.Parameters["WorldViewProjection"].SetValue(meshMatrix*MundoCalculado*view*projection);
                BlinnEffect.Parameters["InverseTransposeWorld"].SetValue(Matrix.Transpose(Matrix.Invert(MundoCalculado)));
                mesh.Draw();
            }

        }

        private void DibujarConBlinnPhong(Matrix MundoCalculado,Model Modelo,Color color, Matrix view, Matrix projection)
        {
                BlinnEffect.CurrentTechnique = BlinnEffect.Techniques["BasicPlainDrawing"];
                BlinnEffect.Parameters["ambientColor"].SetValue(color.ToVector3());
                BlinnEffect.Parameters["diffuseColor"].SetValue(color.ToVector3());
                BlinnEffect.Parameters["specularColor"].SetValue(color.ToVector3());
              
                foreach (var mesh in Modelo.Meshes)
                {

                    Matrix meshMatrix = mesh.ParentBone.Transform;
                    BlinnEffect.Parameters["World"].SetValue(meshMatrix * MundoCalculado);
                    BlinnEffect.Parameters["WorldViewProjection"]
                        .SetValue(meshMatrix * MundoCalculado * view* projection);

                    BlinnEffect.Parameters["InverseTransposeWorld"]
                        .SetValue(Matrix.Invert(Matrix.Transpose(MundoCalculado)));
                    mesh.Draw();

                }
            
                BlinnEffect.CurrentTechnique = BlinnEffect.Techniques["BasicColorDrawing"];
        }
        private void generateLevel(Matrix view,Matrix projection)
        {
            Color ActualColor;
            ActualColor = Color.DarkGreen;
            for (float i = 0; i < 40; i++)
            {
             for (float j = 0; j < 20; j++)
             {
                
                    GrassWorld=Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(i * 200f - 6000f, 0, j * -200f);
                    DibujarConBlinnPhong(GrassWorld,GrassModel,ActualColor,view,projection);
                
             }
            }

            ActualColor = Color.DarkGray;

            
            BikeWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(30f) * Matrix.CreateTranslation(700f, 50f, 0f);
            DibujarConBlinnPhong(BikeWorld,BikeModel,ActualColor,view,projection);
            DibujarConBlinnPhong(RatWorld,RatModel,ActualColor,view,projection);


            for (float i = 0; i < 20; i++)
            {
                
                    PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-i*200f-1500, 5f, - 2000f);
                    DibujarConBlinnPhong(PathWorld,PathModel,ActualColor,view,projection);

            }
     
            for (float i = 0; i < 5; i++)
            {
                
                    PathWorld =  Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-i * 200f + 400f, 5f, -2000);
                    DibujarConBlinnPhong(PathWorld,PathModel,ActualColor,view,projection);

                
            }
            for (float i = 0; i < 4; i++)
            {
                PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(400f, 5f, i * -200f );
                    DibujarConBlinnPhong(PathWorld,PathModel,ActualColor,view,projection);

                
            }
            for (float i = 0; i < 2; i++)
            {
               
                    PathWorld = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(400f, 5f, i * -200f - 1750f);
                    DibujarConBlinnPhong(PathWorld,PathModel,ActualColor,view,projection);

            }

            ActualColor = Color.DarkGray;
            MonumentWorld =  Matrix.CreateScale(8f) * Matrix.CreateTranslation(400f, 6f, -2500f);
            DibujarConBlinnPhong(MonumentWorld,MonumentModel,ActualColor,view,projection);

            SlideWorld =  Matrix.CreateScale(140f) * Matrix.CreateRotationY(-MathHelper.PiOver2) * Matrix.CreateTranslation(-520f, 6f, -2000f);
            DibujarConBlinnPhong(SlideWorld,SlideModel,ActualColor,view,projection);

            ActualColor = Color.White;
            if (BoundingFrustum.Intersects(BallCollide1))
            {
                BallWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(0.8f) * Matrix.CreateTranslation(400f, 12f, 0f + -200f);
                DibujarConBlinnPhong(BallWorld,BallModel,ActualColor,view,projection);

            }

            if (BoundingFrustum.Intersects(BallCollide2))
            {
                BallWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(350f, 12f, 0f + -400f);
                DibujarConBlinnPhong(BallWorld,BallModel,ActualColor,view,projection);
            }
            if (BoundingFrustum.Intersects(BallCollide3)){

                
                    BallWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(450f, 12f, 0f +  -600f);
                    DibujarConBlinnPhong(BallWorld,BallModel,ActualColor,view,projection);
                
            }
            
                BirdWorld = Matrix.CreateScale(4f) * Matrix.CreateTranslation(BirdPosition);
                DibujarConBlinnPhong(BirdWorld,BirdModel,ActualColor,view,projection);
            
            
                ArcoWorld =Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(2f) * Matrix.CreateTranslation(FinishPosition);
                DibujarConBlinnPhong(ArcoWorld,ArcoModel,ActualColor,view,projection);
            

            ActualColor = Color.LightPink;
            BodyWorld =Matrix.CreateScale(1f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(200f, 150f, 150f);
            DibujarConBlinnPhong(BodyWorld,BodyModel,ActualColor,view,projection);

            

        
            BodyWorld =Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(200f, 150f, 0f);
            DibujarConBlinnPhong(BodyWorld,BodyModel,ActualColor,view,projection);
            

            BoyWorld = Matrix.CreateRotationY(MathHelper.PiOver4) *Matrix.CreateScale(10f) * Matrix.CreateTranslation(200f, 8f, -500f);
            DibujarConBlinnPhong(BodyWorld,BodyModel,ActualColor,view,projection);
            DibujarConBlinnPhong(FootWorld,FootModel,ActualColor,view,projection);

            ActualColor = Color.LimeGreen;

            for (float i = 0; i < 25; i++)
            {
              
                    TreeWorld = Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(300f, 8f, i * -40f + 100f);
                    DibujarConBlinnPhong(TreeWorld,Tree1Model,ActualColor,view,projection);

            }

            for (float i = 0; i < 25; i++)
            {
             
                    Tree1World =Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(510f, 8f, i * -40f + 100f);
                    DibujarConBlinnPhong(Tree1World,Tree1Model,ActualColor,view,projection);
                
            }

            for (float i = 0; i < 6; i++)
            {
             
                    TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-300f, 8f, i * -300f + 100f);
                    DibujarConBlinnPhong(TreeWorld,Tree1Model,ActualColor,view,projection);
                
            }
            for (float i = 0; i < 9; i++)
            {
                TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-i*300f-600f, 8f, -1600f);
                    DibujarConBlinnPhong(TreeWorld,Tree1Model,ActualColor,view,projection);
            }
            for (float i = 0; i < 11; i++)
            {
                TreeWorld =Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(-i*300f+50f, 8f, -2300f);

                    DibujarConBlinnPhong(TreeWorld,Tree1Model,ActualColor,view,projection);
                
            }

            for (float i = 0; i < 13; i++)
            {
               TreeWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(1000, 8f, i * -200f + 100f);
               DibujarConBlinnPhong(TreeWorld,Tree1Model,ActualColor,view,projection);

            }

            ActualColor =Color.Peru;

            DogWorld =  Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(700f, 6f, -450f);
            DibujarConBlinnPhong(DogWorld,DogModel,ActualColor,view,projection);
                            

            if (BoundingFrustum.Intersects(BridgeCollision))
            {
                BridgeWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationX(-1f * MathHelper.PiOver2) * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(400f, 0f, -1300f); 
                DibujarConBlinnPhong(BridgeWorld,BridgeModel,ActualColor,view,projection);
                
            }

            for (float i = 0; i < 3; i++)
            {
                
                    BenchWorld = Matrix.CreateScale(2f) * Matrix.CreateRotationY(-1f * MathHelper.Pi) * Matrix.CreateTranslation(-100f, -100f, i * -320 - 320f);
                    DibujarConBlinnPhong(BridgeWorld,BridgeModel,ActualColor,view,projection);
                
            }
            ActualColor =Color.Yellow;

           
            if (drawStar)
            {
               
                    StarWorld = Matrix.CreateScale(1f) * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(400f, 5f, -100f);
                    DibujarConBlinnPhong(StarWorld,StarModel,ActualColor,view,projection);
                
            }

            for (float i = 0; i < 5; i++)
            {
                for (float j = 0; j < 5; j++)
                {
                    GrassWorld = Matrix.CreateScale(0.4f) *
                                 Matrix.CreateTranslation(-i * 200f - 600f, 6f, j * -200f - 1750f);
                    DibujarConBlinnPhong(GrassWorld,GrassModel,ActualColor,view,projection);

                }
            }

            ActualColor= Color.Blue;
            for (float i = 0; i < 5; i++)
            {
                for (float j = 0; j < 3; j++)
                {
                   
                        GrassWorld =Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(i * 200f, 0, j * -200f - 1000f);
                        DibujarConBlinnPhong(GrassWorld,GrassModel,ActualColor,view,projection);

                }
                
            }

            ActualColor = Color.Black;
            LampWorld = Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(700f, 6f, -1900f);
            DibujarConBlinnPhong(LampWorld,LampModel,ActualColor,view,projection);
            DibujarConBlinnPhong(Matrix.CreateScale(0.5f)*Matrix.CreateTranslation(lightPosition),SunModel,Color.Yellow,view,projection);

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
