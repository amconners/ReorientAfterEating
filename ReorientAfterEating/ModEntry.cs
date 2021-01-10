using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ReorientAfterEating
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Fields
        *********/
        /// <summary>
        /// If the farmer was eating since direction was last saved.
        /// </summary>
        private bool wasEating = false;

        /// <summary>
        /// Remembers the direction the farmer was facing before eating.
        /// </summary>
        private int oldDirection;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the game state is updated</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, EventArgs e)
        {
            if (Game1.player.isEating && !this.wasEating)
            {
                this.wasEating = true;
            }
            else if (!Game1.player.isEating && this.wasEating)
            {
                Game1.player.faceDirection(oldDirection);
                this.wasEating = false;
            }
        }

        /// <summary>Raised after the player pressed a keyboard, mouse, or controller button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (Game1.activeClickableMenu == null)
            {
                oldDirection = Game1.player.FacingDirection;
            }
        }
    }
}