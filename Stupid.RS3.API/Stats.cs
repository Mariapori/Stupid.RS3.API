
namespace Stupid.RS3.API
{
    public class Stats
    {
        public Skills Skill {  get; set; }
        public int Rank { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
    }

    public enum Skills
    {
        Overall,
        Attack,
        Defence,
        Strength,
        Constitution,
        Ranged,
        Prayer,
        Magic,
        Cooking,
        Woodcutting,
        Fletching,
        Fishing,
        Firemaking,
        Crafting,
        Smithing,
        Mining,
        Herblore,
        Agility,
        Thieving,
        Slayer,
        Farming,
        Runecrafting,
        Hunter,
        Construction,
        Summoning,
        Dungeoneering,
        Divination,
        Invention,
        Archaeology,
        Necromancy
    }
}
