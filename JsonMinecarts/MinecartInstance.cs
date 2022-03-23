﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonMinecarts
{
    public class MinecartInstance
    {
        /// <summary>
        /// A unique id for this warp. Required for internal functionality.
        /// Suggestion: modname.cartlocation
        /// Example: mysupermart.walkinfreezer1
        /// </summary>
        public string UniqueId { get; set; } = string.Empty;

        /// <summary>
        /// The internal map Name, used to locate warp target.
        /// </summary>
        public string LocationName { get; set; } = "???";

        /// <summary>
        /// The display name that will appear in the destination menu.
        /// </summary>
        public string DisplayName { get; set; } = null;

        /// <summary>
        /// The X location the player will land at. Needs to be within 5 tiles of the cart (so that it can be identified as an origin).
        /// </summary>
        public int LandingPointX { get; set; } = 0;

        /// <summary>
        /// The Y location the player will land at. Needs to be within 5 tiles of the cart (so that it can be identified as an origin).
        /// </summary>
        public int LandingPointY { get; set; } = 0;

        /// <summary>
        /// The direction the player will face when they arrive here.
        /// </summary>
        public int LandingPointDirection { get; set; } = 2;

        /// <summary>
        /// This option mimics the vanilla caves behavior, which silences music if it's playing springtown.
        /// </summary>
        public bool IsUnderground { get; set; } = false;

        /// <summary>
        /// This is optional - if not null, it should contain a string - the option to choose this minecart will only fire if the
        /// named mail flag has been set.
        /// </summary>
        public string MailCondition { get; set; } = null;

        /// <summary>
        /// This is really intended for internal use - it contains a vanilla dialogue response that triggers a vanilla warp handler.
        /// Might be useful if the game has updated or someone has extended the vanilla minecart stuff.
        /// </summary>
        public string VanillaPassthrough { get; set; } = null;
    }
}
