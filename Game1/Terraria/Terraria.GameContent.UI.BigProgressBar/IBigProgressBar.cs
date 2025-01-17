using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	internal interface IBigProgressBar
	{
		bool ValidateAndCollectNecessaryInfo(BigProgressBarInfo info);

		void Draw(BigProgressBarInfo info, SpriteBatch spriteBatch);
	}
}
