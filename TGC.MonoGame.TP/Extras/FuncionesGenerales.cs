using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;
using TGC.MonoGame.TP.Geometries;
namespace TGC.MonoGame.TP.Extras
{
    public abstract class FuncionesGenerales
    {
        public static void loadEffectOnMesh(Model modelo,Effect efecto)
        {
            foreach (var mesh in modelo.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = efecto;
                }
            }
        }

        public static void drawMesh(Model modelo, Matrix mundo, Effect efecto)
        {
            foreach (var mesh in modelo.Meshes)
            {
                mundo = mesh.ParentBone.Transform;
                efecto.Parameters["World"].SetValue(mundo);
                mesh.Draw();
            }
        }
    }
}

