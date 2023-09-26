using Nez.Samples;
using Nez.Sprites;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nez.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame.Scenes
{
    [SampleScene("Basic Scene", 9999, "Scene with a single Entity. The minimum to have something to show")]
    public class BasicScene : SampleScene
    {
        public override void Initialize()
        {
            base.Initialize();

            // default to 1280x720 with no SceneResolutionPolicy
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);

            var moonTex = Content.LoadTexture(@"../Content/chess-queen-white");
            var playerEntity = CreateEntity("player", new Vector2(Screen.Width / 2, Screen.Height / 2));
            playerEntity.AddComponent(new SpriteRenderer(moonTex));
        }
    }
}
