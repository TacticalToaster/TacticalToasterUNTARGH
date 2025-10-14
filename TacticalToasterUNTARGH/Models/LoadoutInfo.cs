using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TacticalToasterUNTARGH.Models
{
    public class LoadoutCategory
    {
        // Add properties as needed in the future
    }

    public class LoadoutItem
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Display name for the category.
        /// </summary>
        [JsonPropertyName("chance")]
        public int? Chance { get; set; }

        /// <summary>
        /// Parent category ID, if applicable.
        /// </summary>
        [JsonPropertyName("children")]
        public Dictionary<string, LoadoutItem>? Children { get; set; }

        /// <summary>
        /// Array of child category IDs.
        /// </summary>
        [JsonPropertyName("slots")]
        public Dictionary<string, List<string>>? Slots { get; set; }
    }

    public class LoadoutInfo
    {
        /// <summary>
        /// Equipment loadout information.
        /// </summary>
        [JsonPropertyName("equipment")]
        public Dictionary<string, Dictionary<string, LoadoutItem>>? Equipment { get; set; }

        /// <summary>
        /// Weapon loadout information.
        /// </summary>
        [JsonPropertyName("weapons")]
        public Dictionary<string, Dictionary<string, List<string>>>? Weapons { get; set; }

        /// <summary>
        /// Categories of loadouts.
        /// </summary>
        [JsonPropertyName("categories")]
        public Dictionary<string, Dictionary<string, LoadoutItem>>? Categories { get; set; }
    }
}