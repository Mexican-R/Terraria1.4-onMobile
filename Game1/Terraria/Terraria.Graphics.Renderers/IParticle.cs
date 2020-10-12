using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	public interface IParticle
	{
		bool ShouldBeRemovedFromRenderer
		{
			get;
		}

		void Update(ParticleRendererSettings settings);

		void Draw(ParticleRendererSettings settings, SpriteBatch spritebatch);
	}
}
