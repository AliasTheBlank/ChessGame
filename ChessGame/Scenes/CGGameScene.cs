using ChessGame.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ChessGame.Scenes;

public class CGGameScene : Scene, IFinalRenderDelegate
{
    public const int ScreenSpaceRenderLayer = 999;
    
    ScreenSpaceRenderer _screenSpaceRenderer;
    static bool _needsFullRenderSizeForUi;
    
    public UICanvas Canvas;

    
    public CGGameScene(bool addExcludeRenderer = true, bool needsFullRenderSizeForUi = false)
    {
        _needsFullRenderSizeForUi = needsFullRenderSizeForUi;
        

        // setup one renderer in screen space for the UI and then (optionally) another renderer to render everything else
        if (needsFullRenderSizeForUi)
        {
            // dont actually add the renderer since we will manually call it later
            _screenSpaceRenderer = new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer);
            _screenSpaceRenderer.ShouldDebugRender = false;
            FinalRenderDelegate = this;
        }
        else
        {
            AddRenderer(new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer));
        }

        if (addExcludeRenderer)
            AddRenderer(new RenderLayerExcludeRenderer(0, ScreenSpaceRenderLayer));

        // create our canvas and put it on the screen space render layer
        Canvas = CreateEntity("ui").AddComponent(new UICanvas());
        Canvas.IsFullScreen = true;
        Canvas.RenderLayer = ScreenSpaceRenderLayer;
    }
    
    private Scene _scene;

    public void OnAddedToScene(Scene scene) => _scene = scene;

    public void OnSceneBackBufferSizeChanged(int newWidth, int newHeight) => _screenSpaceRenderer.OnSceneBackBufferSizeChanged(newWidth, newHeight);

    public void HandleFinalRender(RenderTarget2D finalRenderTarget, Color letterboxColor, RenderTarget2D source,
        Rectangle finalRenderDestinationRect, SamplerState samplerState)
    {
        Core.GraphicsDevice.SetRenderTarget(null);
        Core.GraphicsDevice.Clear(letterboxColor);
        Graphics.Instance.Batcher.Begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
        Graphics.Instance.Batcher.Draw(source, finalRenderDestinationRect, Color.White);
        Graphics.Instance.Batcher.End();

        _screenSpaceRenderer.Render(_scene);
    }
}