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
        private Matrix FloorWorld { get; set; }
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

            FloorWorld = Matrix.CreateScale(200f);
            WallWorld = Matrix.CreateScale(100f) * Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(100f, 0f, 0f);

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

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            //Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

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
            // Aca deberiamos poner toda la logia de renderizado del juego.
            GraphicsDevice.Clear(Color.LightBlue);

            DrawGeometry(Sphere, SpherePosition, 0f, Pitch, Roll);
            Quad.Draw(FloorWorld, Camera.View, Camera.Projection);
            QuadWall.Draw(WallWorld, Camera.View, Camera.Projection);

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            /*Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.DarkBlue.ToVector3());*/
            //var rotationMatrix = Matrix.CreateRotationY(Rotation);

/*            foreach (var mesh in Model.Meshes)
            {
                World = mesh.ParentBone.Transform * rotationMatrix;
                Effect.Parameters["World"].SetValue(World);
                mesh.Draw();
            }
*/        }

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