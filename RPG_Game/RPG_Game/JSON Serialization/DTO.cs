using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RPG_Game.JSON_Serialization
{
    public class GameStateDTO
    {
        [JsonPropertyName("playersList")] public List<PlayerDTO> Players { get; set; } = new();
        [JsonPropertyName("room")] public RoomDTO Room { get; set; }
        [JsonPropertyName("version")] public int Version { get; set; }
        [JsonPropertyName("stepCounter")] public int StepCounter { get; set; }
    }

    public class PlayerDTO
    {
        [JsonPropertyName("x")] public int X { get; set; }
        [JsonPropertyName("y")] public int Y { get; set; }

        [JsonPropertyName("strength")] public int Strength { get; set; }
        [JsonPropertyName("dexterity")] public int Dexterity { get; set; }
        [JsonPropertyName("health")] public int Health { get; set; }
        [JsonPropertyName("luck")] public int Luck { get; set; }
        [JsonPropertyName("aggression")] public int Aggression { get; set; }
        [JsonPropertyName("wisdom")] public int Wisdom { get; set; }

        [JsonPropertyName("inventoryList")] public List<ItemDTO> Inventory { get; set; } = new();
        [JsonPropertyName("leftHand")] public WeaponDTO? LeftHand { get; set; }
        [JsonPropertyName("rightHand")] public WeaponDTO? RightHand { get; set; }
        [JsonPropertyName("gold")] public int Gold { get; set; }
        [JsonPropertyName("coins")] public int Coins { get; set; }

        [JsonPropertyName("nearbyEnemiesDictionary")] public Dictionary<string, EnemyDTO?> NearbyEnemies { get; set; } = new();
        [JsonPropertyName("activeEffectsList")] public List<PotionEffectDTO> ActivePlayerEffects { get; set; } = new();
    }

    public class RoomDTO
    {
        [JsonPropertyName("width")] public int Width { get; set; }
        [JsonPropertyName("height")] public int Height { get; set; }
        [JsonPropertyName("gridCellsList")] public List<CellDTO> GridCells { get; set; } = new();
    }
    
    public class CellDTO
    {
        [JsonPropertyName("x")] public int X { get; set; }
        [JsonPropertyName("y")] public int Y { get; set; }
        [JsonPropertyName("isWall")] public bool IsWall { get; set; }
        [JsonPropertyName("itemsList")] public List<ItemDTO> Items { get; set; } = new();
        [JsonPropertyName("enemy")] public EnemyDTO? Enemy { get; set; }
        [JsonPropertyName("containsPlayer")] public bool ContainsPlayer { get; set; }
    }

    public class ItemDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("symbol")] public char Symbol { get; set; }
        [JsonPropertyName("consoleColor")] public ConsoleColor ConsoleColor { get; set; }
        [JsonPropertyName("isEquippable")] public bool IsEquippable { get; set; }
        [JsonPropertyName("isDrinkable")] public bool IsDrinkable { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("properties")] public Dictionary<string, object> Properties { get; set; } = new();
    }

    public class EnemyDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("symbol")] public char Symbol { get; set; }
        [JsonPropertyName("color")] public ConsoleColor Color { get; set; }
        [JsonPropertyName("damage")] public int Damage { get; set; }
        [JsonPropertyName("health")] public int Health { get; set; }
        [JsonPropertyName("armor")] public int Armor { get; set; }
    }

    public class PotionEffectDTO
    {
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("properties")] public Dictionary<string, object> Properties { get; set; } = new();
    }

    public class WeaponDTO : ItemDTO
    {      
        [JsonPropertyName("damage")] public int Damage { get; set; }
        [JsonPropertyName("twoHanded")] public bool IsTwoHanded { get; set; } 
    }
}
